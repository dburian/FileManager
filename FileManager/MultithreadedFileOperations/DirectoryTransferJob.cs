using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Security;

namespace MultithreadedFileOperations
{
	class DirectoryTransferJob : Job
	{
		DirectoryTransferException firstException;

		public DirectoryTransferJob(DirectoryTransferArguments args, CancellationToken ct)
		{
			Args = args;
			this.ct = ct;
		}
		public DirectoryTransferJob(DirectoryTransferArguments args, CancellationToken ct, OnExceptionRaiseDelegate exceptionRaiseDelegate, OnProgressChangeDelegate progressChangeDelegate)
			:this(args, ct)
		{
			ExceptionRaise += exceptionRaiseDelegate;
			ProgressChange += progressChangeDelegate;
		}

		public DirectoryTransferArguments Args { get; }
		public override JobType Type { get => JobType.DirTransfer; }

		public override void Accept(IJobVisitor visitor) => visitor.Visit(this);


		public override void Run()
		{
			ct.ThrowIfCancellationRequested();

			try
			{
				var copy = new DirectoryCopy(this);
				copy.ExceptionRaise += OnInnerException;
				copy.ProgressChange += OnCopyProgress;

				copy.Run();

				ct.ThrowIfCancellationRequested();
				if(Args.Settings == TransferSettings.DeleteOriginal)
					Args.From.Delete(true);
				
				OnProgressChange(100f);
			}catch (Exception e) when (e is IOException | e is SecurityException | e is UnauthorizedAccessException)
			{
				var ex = new DirectoryTransferException(Args, e);
				OnExceptionRaise(ex);
#if TEST
				throw ex;
#endif
			}
			//No rollback -> Eventhough Delete fails, it still could have deleted some files. Also Delete could fail just temporarly.
			//Action left on user. Either he moves directories in opposite direction or he tries again to delete source...
		}

		void OnCopyProgress(float progress)
		{
			OnProgressChange(progress * 0.9f);
		}
		private void OnInnerException(DirectoryTransferException e)
		{
			if (firstException == null) firstException = e;

			OnExceptionRaise(e);
		}

		private class DirectoryCopy
		{
			DirectoryTransferJob wholeJob;

			float[] progressCache;
			volatile float totalProgress;

			public DirectoryCopy(DirectoryTransferJob job)
			{
				wholeJob = job;
			}

			public event OnProgressChangeDelegate ProgressChange;
			public event Action<DirectoryTransferException> ExceptionRaise;

			public void Run()
			{
				wholeJob.ct.ThrowIfCancellationRequested();

				try
				{
					wholeJob.Args.To.Refresh();
					wholeJob.Args.From.Refresh();
					if (wholeJob.Args.To.Exists) throw new IOException("Destination directory already exists.");
					else if (!wholeJob.Args.From.Exists) throw new IOException("Source directory does not exist.");

					var files = wholeJob.Args.From.GetFiles();
					var dirs = wholeJob.Args.From.GetDirectories();

					wholeJob.Args.To.Create();

					progressCache = new float[files.Length + dirs.Length];

					List<Task> tasks = new List<Task>();
					foreach (var file in files)
					{
						int cacheInd = tasks.Count;
						wholeJob.ct.ThrowIfCancellationRequested();
						var destFile = new FileInfo(Path.Combine(wholeJob.Args.To.FullName, file.Name));
						var job = new FileTransferJob(new FileTransferArguments(file, destFile, wholeJob.Args.Settings), wholeJob.ct, OnInnerExceptionRaise, p => RegisterProgress(p, cacheInd));
						tasks.Add(Task.Run(job.Run, wholeJob.ct));
					}

					foreach (var dir in dirs)
					{
						int cacheInd = tasks.Count;
						wholeJob.ct.ThrowIfCancellationRequested();
						var destDir = new DirectoryInfo(Path.Combine(wholeJob.Args.To.FullName, dir.Name));
						var job = new DirectoryTransferJob(new DirectoryTransferArguments(dir, destDir, wholeJob.Args.Settings), wholeJob.ct, OnInnerExceptionRaise, p => RegisterProgress(p, cacheInd));
						tasks.Add(Task.Run(job.Run, wholeJob.ct));
					}

					Task.WaitAll(tasks.ToArray());
					if (totalProgress < 100) ProgressChange?.Invoke(100);
				}
				catch (AggregateException e)
				{
					//Consistent state is taken care of by inner jobs...
					e.Handle(AggregateExceptionHandler);

					//Rollback
					wholeJob.Args.To.Refresh();
					if (wholeJob.Args.To.Exists) wholeJob.Args.To.Delete(true);

					if (wholeJob.firstException != null) throw wholeJob.firstException;
				}
				catch (Exception e) when (e is IOException | e is UnauthorizedAccessException | e is SecurityException)
				{
					var ex = new DirectoryTransferException(wholeJob.Args, e);
					ExceptionRaise?.Invoke(ex);
#if TEST
					throw ex;
#endif
				}
			}

			private bool AggregateExceptionHandler(Exception e)
			{
				if (e == wholeJob.firstException.InnerException) return true;
				return false;
			}

			private void OnInnerExceptionRaise(FileOperationException e)
			{
				var ex = new DirectoryTransferException(wholeJob.Args, e);
				ExceptionRaise?.Invoke(ex);
			}

			void RegisterProgress(float progress, int cacheIndex)
			{
				var dif = (progress - progressCache[cacheIndex]) / progressCache.Length;
				progressCache[cacheIndex] = progress;

				int counter = 10;
				float totalProgressLocal = totalProgress;
				float newTotal = totalProgressLocal + dif;
				while (Interlocked.CompareExchange(ref totalProgress, newTotal, totalProgressLocal) != totalProgressLocal && counter > 0)
				{
					totalProgressLocal = totalProgress;
					newTotal = totalProgressLocal + dif;

					counter--;
				}
				ProgressChange?.Invoke(totalProgress);
			}
		}
	}
}
