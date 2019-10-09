using System;
using System.Threading;

namespace MultithreadedFileSystemOperations
{
	/// <summary>
	/// Wraps raw job and provides additional info, so it can be executed in JobsPool. Is thread safe.
	/// </summary>
	internal class JobWrapper : IJobHandle, IJobView
	{
		private readonly JobArgumentsVisitor visitor;
		private float lastProgressReported;
		/// <summary>
		/// Representing the right to dispose this instance
		/// </summary>
		private readonly object disposing;

		public JobWrapper(Job job, CancellationTokenSource cts, int id)
		{
			Job = job;
			Cts = cts;
			Id = id;

			disposing = new object();

			Job.ProgressChange += OnJobProgressChange;

			visitor = new JobArgumentsVisitor();
			Job.Accept(visitor);
		}

		/// <summary>
		/// Unique job id.
		/// </summary>
		public int Id { get; }
		/// <summary>
		/// Last logged job status.
		/// </summary>
		public JobStatus LastStatus { get; set; }
		/// <summary>
		/// Last exception thrown inside of the job.
		/// </summary>
		public FileOperationException Exception { get; private set; }
		/// <summary>
		/// Last logged job progress.
		/// </summary>
		public float Progress { get; private set; }
		/// <summary>
		/// Type of the job.
		/// </summary>
		public JobType Type => Job.Type;
		/// <summary>
		/// True if job is already disposed, false otherwise.
		/// </summary>
		public bool IsDisposed { get; private set; }

		private CancellationTokenSource Cts { get; }
		private Job Job { get; }


		public event OnJobChangeDelegate JobChange;


		public void Cancel()
		{
			lock (disposing)
			{
				if (IsDisposed)
				{
					return;
				}
				Cts.Cancel();
			}
		}
		public void Dispose()
		{
			if (IsDisposed)
			{
				return;
			}

			Cancel();

			IsDisposed = true;

			lock (disposing)
			{
				Cts.Dispose();
			}
		}

		public void Run()
		{
			if (IsDisposed)
			{
				return;
			}

			LastStatus = JobStatus.Running;
			try
			{
				Job.Run();
				LastStatus = JobStatus.Done;
			}
			catch (OperationCanceledException e)
			{
				if (!IsDisposed && e.CancellationToken == Cts.Token)
				{
					LastStatus = JobStatus.Canceled;
					JobChange?.Invoke(this, JobChangeEvent.Canceled);
				}
			}
			catch (FileOperationException e)
			{
				LastStatus = JobStatus.Error;

				Exception = e;
				JobChange?.Invoke(this, JobChangeEvent.ExceptionThrown);
			}

		}

		public IJobView GetView()
		{
			return this;
		}

		public IJobArgumentsView GetArgumentsView()
		{
			return visitor;
		}

		private void OnJobProgressChange(float progress)
		{
			Progress = progress;

			if (Progress - lastProgressReported > 0.1)
			{
				JobChange?.Invoke(this, JobChangeEvent.OnProgressChange);
				lastProgressReported = Progress;
			}
		}
	}
}
