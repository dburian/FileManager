using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Linq;

namespace MultithreadedFileOperations
{
	public static class JobsPool
	{
		static MoveFreezeLockSlim jobsLock = new MoveFreezeLockSlim();
		static Random UniqueGen = new Random();

		internal static volatile bool resettingProcedureStarted = false;

		static ConcurrentDictionary<int, IJobHandle> Jobs { get; set; } = new ConcurrentDictionary<int, IJobHandle>();

		//Set from UI, called from ThreadPool
		static event OnJobChangeDelegate JobChange;
		static event OnJobsPoolExceptionDelegate PoolException;

		internal static int StartNew(Job job, CancellationTokenSource cts)
		{
			if (resettingProcedureStarted) return 0;

			var task = new Task(job.Run, cts.Token);
			IJobHandle handle = new JobWrapper(job, cts, task);

			int id = AddJob(handle);

			OnJobChange(handle.GetView(), JobChangeEvent.BeforeRun);

			handle.Task.Start(TaskScheduler.Default);

			return id;
		}
		internal static int StartNew(Job job, CancellationTokenSource cts, IEnumerable<int> idOfPreviousJobs)
		{
			if (resettingProcedureStarted) return 0;

			List<Task> previousTasks = new List<Task>();
			foreach (var jobId in idOfPreviousJobs)
			{
				IJobHandle previousJob;
				if (Jobs.TryGetValue(jobId, out previousJob))
				{
					previousTasks.Add(previousJob.Task);
				}
			}

			var startNewJob = new TaskCompletionSource<bool>();
			previousTasks.Add(startNewJob.Task);

			var task = Task.Factory.ContinueWhenAll(
				previousTasks.ToArray(),
				tasks => {
					int i;
					for (i = 0; i < tasks.Length; i++)
					{
						if (tasks[i].Status != TaskStatus.RanToCompletion) break;
					}

					if(i == tasks.Length) job.Run();
				},
				cts.Token,
				TaskContinuationOptions.None,
				TaskScheduler.Default
			);
			IJobHandle newJob = new JobWrapper(job, cts, task);

			int id = AddJob(newJob);

			OnJobChange(newJob.GetView(), JobChangeEvent.BeforeRun);
			startNewJob.SetResult(true);

			return id;
		}


		static int AddJob(IJobHandle jobHandle)
		{
			int id = UniqueGen.Next();

			jobsLock.EnterMoveLock();                                       //------------EnterLock

			while (id == 0 || !Jobs.TryAdd(id, jobHandle))
				id = UniqueGen.Next();

			jobsLock.ExitMoveLock();                                        //------------ExitLock	

			jobHandle.Id = id;
			jobHandle.JobChange += OnJobChange;
			jobHandle.Task.ContinueWith(_ => RemoveJob(jobHandle), new CancellationToken(), TaskContinuationOptions.None, TaskScheduler.Default);

			return id;
		}
		static void RemoveJob(IJobHandle jobHandle)
		{
			if (jobHandle.Task.Status != TaskStatus.Canceled) OnJobChange(jobHandle.GetView(), JobChangeEvent.AfterCompleted);
			else OnJobChange(jobHandle.GetView(), JobChangeEvent.Canceled);

			jobsLock.EnterMoveLock();

			// If this fails, this jobHandle was somehow already removed
			if (!Jobs.TryRemove(jobHandle.Id, out jobHandle))
			{
				PoolException?.Invoke(new InvalidOperationException("JobsPool.RemoveJob: Somehow a job was removed twice from the pool.\nPlease wait while the JobsPool resets."));
				Task.Run(UIThreadSafe.CancelAllAndReset);
			}

			jobsLock.ExitMoveLock();
		}

		static void OnJobChange(IJobView changedJob, JobChangeEvent changeEvent)
		{
			JobChange?.Invoke(changedJob, changeEvent);
		}

		static void CancelAll()
		{
			resettingProcedureStarted = true;

			while (Jobs.Count > 0)
			{
				foreach (var jobHandle in Jobs.Values)
					if (!jobHandle.Task.IsCanceled) jobHandle.Cancel();

				//If this code is executed from thread pool, sleep so that this thread can be used to cleanup jobs
				if (TaskScheduler.Current == TaskScheduler.Default)
					Thread.Sleep(500);
			}

			resettingProcedureStarted = false;
		}

		//TODO: *Refactor* reorder the code so the classes are last
		//Called only from UI thread
		public static class UIThreadSafe
		{

			public static void RegisterForExceptions(OnJobsPoolExceptionDelegate onJobsPoolException)
			{
				PoolException += onJobsPoolException;
			}

			public static void RegisterNewJobsPane(OnJobChangeDelegate jobChange, out List<IJobView> jobsListToPopulate)
			{
				jobsLock.EnterFreezeLock();

				JobChange += jobChange;

				jobsListToPopulate = new List<IJobView>(Jobs.Select(pair => pair.Value.GetView()));

				jobsLock.ExitFreezeLock();
			}

			public static void SignOutJobsPane(OnJobChangeDelegate jobChange)
			{
				JobChange -= jobChange;
			}


			public static void CancelAllAndReset()
			{
				Task.Run(JobsPool.CancelAll);
			}
		}
	}
}
