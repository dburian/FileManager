using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace MultithreadedFileOperations
{
	/// <summary>
	/// For library users provider of a view of running and queued jobs.
	/// Internally takes care of running the operations and providing callbacks for each job change.
	/// </summary>
	public static class JobsPool
	{
		internal static volatile bool jobsPoolDisposed = false;
		private static readonly MoveFreezeLockSlim jobsLock = new MoveFreezeLockSlim();
		private static int lastUsedId = 0;
		private static readonly Worker worker = new Worker();

		/// <summary>
		/// Signlas to the workers, that there is some job in JobsQueue to execute.
		/// </summary>
		private static readonly AutoResetEvent queueNonEmpty = new AutoResetEvent(false);

		private static ConcurrentQueue<IJobHandle> JobsQueue { get; set; } = new ConcurrentQueue<IJobHandle>();

		//Set from GUI, called from ThreadPool or WorkerThread
		private static event OnJobChangeDelegate JobChange;

		#region From GUI

		/// <summary>
		/// Registers new jobs pane as a listener.
		/// </summary>
		/// <param name="jobChange">Delegate to be called on each subsequent job change</param>
		/// <param name="jobsListToPopulate">Snapshot of enqueued jobs</param>
		public static void RegisterNewJobsPane(OnJobChangeDelegate jobChange, out List<IJobView> jobsListToPopulate)
		{
			jobsListToPopulate = null;
			if (jobsPoolDisposed)
			{
				return;
			}

			jobsLock.EnterFreezeLock();                         //----------Enter freeze lock

			using (var jobs = JobsQueue.GetEnumerator())
			{
				JobChange += jobChange;

				jobsLock.ExitFreezeLock();                      //----------Exit freeze lock

				jobsListToPopulate = new List<IJobView>();
				while (jobs.MoveNext())
				{
					jobsListToPopulate.Add(jobs.Current.GetView());
				}
			}
		}

		/// <summary>
		/// Sings out or unregisters old jobs pane as a listener.
		/// </summary>
		/// <param name="jobChange">Delegate to be removed from job change event</param>
		public static void SignOutJobsPane(OnJobChangeDelegate jobChange)
		{
			if (jobsPoolDisposed)
			{
				return;
			}

			JobChange -= jobChange;
		}

		/// <summary>
		/// Cancels all enqueued and running jobs and disposes whole JobsPool class.
		/// </summary>
		public static void CancelAllAndDispose()
		{
			jobsLock.EnterFreezeLock();
			jobsPoolDisposed = true;

			foreach (var job in JobsQueue)
			{
				job.Dispose();
			}

			jobsLock.ExitFreezeLock();

			worker.Cancel();

			//In the future, if there are more workers, it should be made sure to Set() the event until there are no workers running.
			queueNonEmpty.Set();

			worker.Dispose();

			jobsLock.Dispose();
		}
		#endregion

		/// <summary>
		/// Enqueues new job(operation) to be executed.
		/// </summary>
		/// <param name="job">Job to be executed</param>
		/// <param name="cts">CancellationTokenSource bound with the provided jobs</param>
		/// <returns>Unique id of the enqueued job</returns>
		internal static int EnqueueNew(Job job, CancellationTokenSource cts)
		{
			if (jobsPoolDisposed)
			{
				return 0;
			}

			IJobHandle handle = new JobWrapper(job, cts, ++lastUsedId) { LastStatus = JobStatus.Waiting };

			OnJobChange(handle.GetView(), JobChangeEvent.Enqueued);
			AddJob(handle);

			return handle.Id;
		}

		private static void AddJob(IJobHandle jobHandle)
		{
			jobsLock.EnterMoveLock();                                       //------------EnterLock

			JobsQueue.Enqueue(jobHandle);

			jobsLock.ExitMoveLock();                                        //------------ExitLock	


			jobHandle.JobChange += OnJobChange;

			queueNonEmpty.Set();
		}

		private static void OnJobChange(IJobView changedJob, JobChangeEvent changeEvent)
		{
			JobChange?.Invoke(changedJob, changeEvent);
		}

		/// <summary>
		/// Execution class. Dequeues a job from JobsQueue and executes it.
		/// </summary>
		private class Worker : IDisposable
		{
			private readonly CancellationTokenSource cts;
			private IJobHandle currentJob;
			private readonly Thread workerThread;
			private CancellationToken ct;

			public Worker()
			{
				cts = new CancellationTokenSource();
				ct = cts.Token;

				workerThread = new Thread(Start);
				workerThread.Start();
			}

			/// <summary>
			/// Cancels currently executing job, disposes it and shuts down the main execution loop.
			/// </summary>
			public void Cancel()
			{
				cts.Cancel();
				currentJob?.Cancel();
			}

			public void Dispose()
			{
				Cancel();

				if (workerThread.ThreadState != System.Threading.ThreadState.Unstarted)
				{
					workerThread.Join();
				}

				cts.Dispose();
			}

			/// <summary>
			/// Starts the main execution loop.
			/// </summary>
			public void Start()
			{
				while (!ct.IsCancellationRequested)
				{
					jobsLock.EnterMoveLock();
					while (!JobsQueue.TryDequeue(out currentJob))
					{
						jobsLock.ExitMoveLock();
						queueNonEmpty.WaitOne();
						if (ct.IsCancellationRequested)
						{
							return;
						}

						jobsLock.EnterMoveLock();
					}
					jobsLock.ExitMoveLock();


					OnJobChange(currentJob.GetView(), JobChangeEvent.BeforeRun);

					currentJob.Run();
					if (currentJob.LastStatus != JobStatus.Canceled && currentJob.LastStatus != JobStatus.Error)
					{
						OnJobChange(currentJob.GetView(), JobChangeEvent.AfterCompleted);
					}

					currentJob.Dispose();
				}
			}
		}
	}
}
