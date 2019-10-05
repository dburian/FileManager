using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Security;
using System.Diagnostics;

//TODO: *Refactor* order usings and delete not used

namespace MultithreadedFileOperations
{
	class DirectoryCopyJob : Job
	{
		float[] progressCache;
		float totalProgress;
		DirectoryCopyException firstException;

		public DirectoryCopyJob(DirectoryTransferJobArguments args, CancellationToken ct)
		{
			Args = args;
			this.ct = ct;
		}
		public DirectoryCopyJob(DirectoryTransferJobArguments args, CancellationToken ct, OnExceptionRaiseDelegate exceptionRaiseDelegate, OnProgressChangeDelegate progressChangeDelegate)
			:this(args, ct)
		{
			ExceptionRaise += exceptionRaiseDelegate;
			ProgressChange += progressChangeDelegate;
		}
		
		public override JobType Type { get => JobType.FileCopy; }
		public DirectoryTransferJobArguments Args { get; }

		public override void Accept(IJobVisitor visitor)
		{
			visitor.Visit(this);
		}

		public override void Run()
		{
			try
			{
				ct.ThrowIfCancellationRequested();

				Args.To.Refresh();
				Args.From.Refresh();
				if (Args.To.Exists) throw new IOException("Destination directory already exists.");
				else if (!Args.From.Exists) throw new IOException("Source directory does not exist.");
			
				var files = Args.From.GetFiles();
				var dirs = Args.From.GetDirectories();

				Args.To.Create();

				progressCache = new float[files.Length + dirs.Length];

				List<Task> tasks = new List<Task>();
				foreach (var file in files)
				{
					int cacheInd = tasks.Count;
					ct.ThrowIfCancellationRequested();
					var destFile = new FileInfo(Path.Combine(Args.To.FullName, file.Name));
					var job = new FileCopyJob(new FileTransferJobArguments(file, destFile), ct, OnInnerException, p => RegisterProgress(p, cacheInd));
					tasks.Add(Task.Run(job.Run, ct));
				}

				foreach (var dir in dirs)
				{
					int cacheInd = tasks.Count;
					ct.ThrowIfCancellationRequested();
					var destDir = new DirectoryInfo(Path.Combine(Args.To.FullName, dir.Name));
					var job = new DirectoryCopyJob(new DirectoryTransferJobArguments(dir, destDir), ct, OnInnerException, p => RegisterProgress(p, cacheInd));
					tasks.Add(Task.Run(job.Run, ct));
				}

				Task.WaitAll(tasks.ToArray());
				if (totalProgress < 100) OnProgressChange(100);
			}
			//TODO: some of these are ancestors of others
			catch (AggregateException e)
			{
				//Consistent state is taken care of by inner jobs...
				e.Handle(AggregateExceptionHandler);

				Args.To.Refresh();
				if (Args.To.Exists) Args.To.Delete(true);

				if (firstException != null) throw firstException;
			}
			catch(Exception e) when (e is IOException | e is UnauthorizedAccessException | e is SecurityException)
			{
				var ex = new DirectoryCopyException(Args, e);
				OnExceptionRaise(ex);
				throw ex;
			}
		}


		bool AggregateExceptionHandler(Exception e)
		{
			if (e == firstException.InnerException) return true;
			return false;
		}

		void OnInnerException(FileOperationException e)
		{
			var ex = new DirectoryCopyException(Args, e);
			if (firstException == null) firstException = ex;

			OnExceptionRaise(ex);
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
			OnProgressChange(totalProgress);
		}
	}
}
