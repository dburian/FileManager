using System;
using System.Threading;

namespace MultithreadedFileOperations
{
	internal abstract class Job : IVisitableJob
	{
		protected CancellationToken ct;

		public abstract JobType Type { get; }

		public event OnProgressChangeDelegate ProgressChange;
		public event OnExceptionRaiseDelegate ExceptionRaise;

		public abstract void Accept(IJobVisitor visitor);

		abstract public void Run();

		protected virtual void OnProgressChange(float percentage)
		{
			ProgressChange?.Invoke(percentage);
		}

		protected virtual void OnExceptionRaise(FileOperationException e)
		{
			ExceptionRaise?.Invoke(e);
		}

	}
}
