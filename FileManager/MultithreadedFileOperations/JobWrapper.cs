using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MultithreadedFileOperations
{
	class JobWrapper : IJobHandle, IJobView
	{
		int _id;
		bool idSealed = false;
		JobArgumentsVisitor visitor;

		public JobWrapper(Job job, CancellationTokenSource cts, Task task)
		{
			Job = job;
			Cts = cts;
			Task = task;

			Job.ExceptionRaise += OnJobException;
			Job.ProgressChange += OnJobProgressChange;

			visitor = new JobArgumentsVisitor();
			Job.Accept(visitor);
		}

		public Task Task { get; }
		public int Id
		{
			get => _id;
			set
			{
				if (idSealed) throw new InvalidOperationException("JobWrapper.Id: Id is already set");

				idSealed = true;
				_id = value;
			}
		}
		public FileOperationException Exception { get; private set; }
		public float Progress { get; private set; }
		public JobType Type { get => Job.Type; }

		CancellationTokenSource Cts { get; }
		Job Job { get; }


		public event OnJobChangeDelegate JobChange;


		public void Cancel() => Cts.Cancel();

		//TODO: refactor all events, start of new thread should be in event handler not event caller...
		public void Run()
		{
			if (!idSealed) throw new InvalidOperationException("JobWrapper.Run: About to start job with no Id.");
			Job.Run();
		}

		public IJobView GetView() => this;

		public IJobArgumentsView GetArgumentsView() => visitor;

		void OnJobException(FileOperationException e)
		{
			Exception = e;
			JobChange?.Invoke(this, JobChangeEvent.OnExceptionRaise);
		}
		void OnJobProgressChange(float progress)
		{
			Progress = progress;
			JobChange?.Invoke(this, JobChangeEvent.OnProgressChange);
		}
	}
}
