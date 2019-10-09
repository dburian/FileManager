using System;
using System.IO;
using System.Security;
using System.Threading;

namespace MultithreadedFileOperations
{
	/// <summary>
	/// Cancelable directory transfer operation.
	/// </summary>
	internal class DirectoryTransferJob : Job
	{
		public DirectoryTransferJob(DirectoryTransferArguments args, CancellationToken ct)
		{
			Args = args;
			this.ct = ct;
		}
		public DirectoryTransferJob(DirectoryTransferArguments args, OnProgressChangeDelegate progressChangeDelegate, CancellationToken ct)
			: this(args, ct)
		{
			ProgressChange += progressChangeDelegate;
		}

		public DirectoryTransferArguments Args { get; }
		public override JobType Type => JobType.DirTransfer;

		public override void Accept(IJobVisitor visitor)
		{
			visitor.Visit(this);
		}

		/// <summary>
		/// Executes directory transfer.
		/// </summary>
		/// <exception cref="DirectoryTransferException"/>
		public override void Run()
		{
			ct.ThrowIfCancellationRequested();

			try
			{
				var copy = new DirectoryCopy(this);
				copy.ProgressChange += OnCopyProgress;

				copy.Run();

				if (Args.Settings == TransferSettings.DeleteOriginal)
				{
					Args.From.Delete(true);
				}

				OnProgressChange(100f);
			}
			catch (Exception e) when (e is IOException | e is SecurityException | e is UnauthorizedAccessException)
			{
				throw new DirectoryTransferException(Args, e);
			}
			//Delete(true) can fail despite deleting some files. Therefore rollback could be as time consuming as the actual task.
			//Consequently I decided to leave the rollback on user. (Should be easy once TransferSettings.Overwrite is implemented.)
		}

		private void OnCopyProgress(float progress)
		{
			OnProgressChange(progress * 0.95F);
		}

		/// <summary>
		/// Represents inner part of transfering - copying - and it's execution context.
		/// </summary>
		private class DirectoryCopy
		{
			private readonly DirectoryTransferJob wholeJob;
			private float[] progressCache;
			private float totalProgress;

			public DirectoryCopy(DirectoryTransferJob job)
			{
				wholeJob = job;
			}

			public event OnProgressChangeDelegate ProgressChange;
			public event Action<DirectoryTransferException> ExceptionRaise;

			public void Run()
			{
				wholeJob.ct.ThrowIfCancellationRequested();
				wholeJob.Args.To.Refresh();
				wholeJob.Args.From.Refresh();

				bool destinationExisted = wholeJob.Args.To.Exists;

				try
				{
					if (destinationExisted)
					{
						throw new IOException("Destination directory already exists.");
					}
					else if (!wholeJob.Args.From.Exists)
					{
						throw new IOException("Source directory does not exist.");
					}

					var files = wholeJob.Args.From.GetFiles();
					var dirs = wholeJob.Args.From.GetDirectories();

					wholeJob.Args.To.Create();

					progressCache = new float[files.Length + dirs.Length];
					int cacheInd = 0;

					foreach (var file in files)
					{
						int cacheIndLocal = cacheInd++;

						wholeJob.ct.ThrowIfCancellationRequested();
						var destFile = new FileInfo(Path.Combine(wholeJob.Args.To.FullName, file.Name));
						var job = new FileTransferJob(new FileTransferArguments(file, destFile, wholeJob.Args.Settings), p => RegisterProgress(p, cacheIndLocal), wholeJob.ct);
						job.Run();
					}

					foreach (var dir in dirs)
					{
						int cacheIndLocal = cacheInd++;
						wholeJob.ct.ThrowIfCancellationRequested();
						var destDir = new DirectoryInfo(Path.Combine(wholeJob.Args.To.FullName, dir.Name));
						var job = new DirectoryTransferJob(new DirectoryTransferArguments(dir, destDir, wholeJob.Args.Settings), p => RegisterProgress(p, cacheIndLocal), wholeJob.ct);
						job.Run();
					}

					if (totalProgress < 100)
					{
						totalProgress = 100f;
						ProgressChange?.Invoke(100f);
					}
				}
				catch (Exception e) when (e is IOException | e is UnauthorizedAccessException | e is SecurityException | e is FileOperationException)
				{
					throw new DirectoryTransferException(wholeJob.Args, e);
				}
				finally
				{
					if (totalProgress < 100)
					{
						//Rollback
						wholeJob.Args.To.Refresh();
						if (wholeJob.Args.To.Exists && !destinationExisted)
						{
							wholeJob.Args.To.Delete(true);
						}
					}
				}
			}

			/// <summary>
			/// Registers progress of individual inner jobs. Is thread safe.
			/// </summary>
			/// <param name="progress">New progress to be registered.</param>
			/// <param name="cacheIndex">Id of inner job.</param>
			private void RegisterProgress(float progress, int cacheIndex)
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
