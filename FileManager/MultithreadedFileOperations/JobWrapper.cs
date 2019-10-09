using System;
using System.Threading;

namespace MultithreadedFileOperations
{
	/// <summary>
	/// Wraps raw job and provides additional info, so it can be executed in JobsPool. Is thread safe.
	/// </summary>
	internal class JobWrapper : IJobHandle, IJobView
	{
		private readonly JobArgumentsVisitor visitor;
		private float lastProgressReported;
		private bool _disposed;

		public JobWrapper(Job job, CancellationTokenSource cts, int id)
		{
			Job = job;
			Cts = cts;
			Id = id;

			Job.ProgressChange += OnJobProgressChange;

			visitor = new JobArgumentsVisitor();
			Job.Accept(visitor);
		}

		public int Id { get; }
		public JobStatus LastStatus { get; set; }
		public FileOperationException Exception { get; private set; }
		public float Progress { get; private set; }
		public JobType Type => Job.Type;
		public bool Disposed => _disposed;

		private CancellationTokenSource Cts { get; }
		private Job Job { get; }


		public event OnJobChangeDelegate JobChange;


		public void Cancel()
		{
			if (_disposed)
			{
				return;
			}

			Cts.Cancel();
		}
		public void Dispose()
		{
			if (_disposed)
			{
				return;
			}

			Cancel();

			_disposed = true;
			Cts.Dispose();
		}

		public void Run()
		{
			if (_disposed)
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
				if (!_disposed && e.CancellationToken == Cts.Token)
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
