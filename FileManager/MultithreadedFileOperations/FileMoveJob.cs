using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace MultithreadedFileOperations
{
	class FileMoveJob : Job
	{

		FileOperationException _firstException;

		public FileMoveJob(FileTransferJobArguments args, CancellationToken ct)
		{
			Args = args;
			this.ct = ct;
			Type = JobType.FileMove;

			_firstException = null;
		}
		public FileMoveJob(FileTransferJobArguments args, CancellationToken ct, OnExceptionRaiseDelegate onExceptionRaise, OnProgressChangeDelegate onProgressChange)
			:this (args, ct)
		{
			ExceptionRaise += onExceptionRaise;
			ProgressChange += onProgressChange;
		}

		public override JobType Type { get; }
		public FileTransferJobArguments Args { get; private set; }

		private FileOperationException FirstException
		{
			get => _firstException;
			set
			{
				if (_firstException == null) _firstException = value;
			}
		}


		public override void Run()
		{
			ct.ThrowIfCancellationRequested();

			var copyJob = new FileCopyJob(Args, ct, OnExceptionRaise, OnCopyProgressChange);
			var delJob = new DeleteJob(new DeleteJobArguments(Args.From), ct, OnExceptionRaise, OnDeleteProgressChange);
			Task tCopy = null;
			Task tDel = null;
			Task tRollback = null;
			try
			{
				// Copy
				tCopy = Task.Factory.StartNew(copyJob.Run, ct, TaskCreationOptions.PreferFairness, TaskScheduler.Default);

				//Delete the original
				tDel = tCopy.ContinueWith(
					_ => delJob.Run(),
					ct,
					TaskContinuationOptions.OnlyOnRanToCompletion, 
					TaskScheduler.Default);


				// If deleting the original fails, delete the copy, thus canceling the operation
				tRollback = tDel.ContinueWith(
					_ => 
						{
							Args.From.Refresh();
							if (tCopy.Status == TaskStatus.RanToCompletion && Args.From.Exists) //Just double-checking
									Args.To.Delete();
						},
					TaskContinuationOptions.HideScheduler | TaskContinuationOptions.NotOnRanToCompletion);

				Task.WaitAll(new Task[] { tCopy, tDel, tRollback});
			}
			catch (AggregateException ae)
			{
				//If there are some exceptions, MoveJob shouldn't handle they will throw here...
				ae.Handle(e =>
				{
					if (e == FirstException) return true;
					if (e is OperationCanceledException && tRollback?.IsCanceled == true) return true;
					if (e is OperationCanceledException && tDel?.IsCanceled == true && !ct.IsCancellationRequested) return true;

					return false;
				});

				// Only care about the first exception that happened (since tasks are running after each other, first exception is well defined)
				if (FirstException != null) throw  new FileMoveException(Args, FirstException);
			}
			
		}

		public override void Accept(IJobVisitor visitor) => visitor.Visit(this);

		protected override void OnExceptionRaise(FileOperationException e)
		{
			FirstException = e;
			base.OnExceptionRaise(new FileMoveException(Args, e));
		}

		private void OnCopyProgressChange(float percentage)
		{
			OnProgressChange(percentage * 0.9f);
		}
		private void OnDeleteProgressChange(float percentage)
		{
			OnProgressChange(90 + 0.1f * percentage);
		}
	}
}
