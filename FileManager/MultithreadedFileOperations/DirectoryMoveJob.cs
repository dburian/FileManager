using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultithreadedFileOperations
{
	class DirectoryMoveJob : Job
	{
		DirectoryMoveException firstException;

		public DirectoryMoveJob(DirectoryTransferJobArguments args, CancellationToken ct)
		{
			Args = args;
			this.ct = ct;
		}
		public DirectoryMoveJob(DirectoryTransferJobArguments args, CancellationToken ct, OnExceptionRaiseDelegate exceptionRaiseDelegate, OnProgressChangeDelegate progressChangeDelegate)
			:this(args, ct)
		{
			ExceptionRaise += exceptionRaiseDelegate;
			ProgressChange += progressChangeDelegate;
		}

		public DirectoryTransferJobArguments Args { get; }
		public override JobType Type { get => JobType.DirMove; }

		public override void Accept(IJobVisitor visitor) => visitor.Visit(this);


		public override void Run()
		{
			ct.ThrowIfCancellationRequested();

			var copyJob = new DirectoryCopyJob(Args, ct, OnInnerException, OnCopyProgress);
			var delJob = new DeleteJob(new DeleteJobArguments(Args.From, true), ct, OnInnerException, OnDeleteProgress);

			Task tDel = null;
			try
			{
				var tCopy = Task.Factory.StartNew(copyJob.Run, ct, TaskCreationOptions.PreferFairness, TaskScheduler.Default);

				tDel = tCopy.ContinueWith(_ => delJob.Run(), ct, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.Default);

				//No rollback -> Eventhough tDel fails, it still could have deleted some files. Also tDel could fail just temporarly.
				//Action left on user. Either he moves directories in opposite direction or he tries again to delete source...

				Task.WaitAll(new Task[] { tCopy, tDel });
			}catch (AggregateException ae)
			{
				ae.Handle(e =>
					{
						if (e == firstException.InnerException) return true;
						if (e is OperationCanceledException && tDel?.IsCanceled == true && !ct.IsCancellationRequested) return true;

						return false;
					});

				if (firstException != null) throw firstException;
			}
		}

		void OnInnerException(FileOperationException e)
		{
			var ex = new DirectoryMoveException(Args, e);
			if (firstException == null) firstException = ex;
			OnExceptionRaise(ex);
		}
		void OnCopyProgress(float progress)
		{
			OnProgressChange(progress * 0.9f);
		}
		void OnDeleteProgress(float progress)
		{
			OnProgressChange(90f + progress * 0.1f);
		}
	}
}
