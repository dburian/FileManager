using System;
using System.IO;
using System.Security;
using System.Threading;

namespace MultithreadedFileSystemOperations
{
	/// <summary>
	/// A cancelable file transfer operation.
	/// </summary>
	internal class FileTransferJob : Job
	{
		public FileTransferJob(FileTransferArguments args, CancellationToken ct)
		{
			Args = args;
			this.ct = ct;
			Type = JobType.FileTransfer;

		}
		public FileTransferJob(FileTransferArguments args, OnProgressChangeDelegate onProgressChange, CancellationToken ct)
			: this(args, ct)
		{
			ProgressChange += onProgressChange;
		}

		public override JobType Type { get; }
		public FileTransferArguments Args { get; }

		/// <summary>
		/// Executes the transfer.
		/// </summary>
		/// <exception cref="FileTransferException"/>
		public override void Run()
		{
			ct.ThrowIfCancellationRequested();
			bool copyFinished = false;
			bool transferFinished = false;

			try
			{
				Copy();
				copyFinished = true;

				ct.ThrowIfCancellationRequested();
				if (Args.Settings == TransferSettings.DeleteOriginal)
				{
					Args.From.Delete();
				}

				OnProgressChange(100f);
				transferFinished = true;
			}
			catch (Exception e) when (e is IOException | e is SecurityException | e is UnauthorizedAccessException)
			{
				throw new FileTransferException(Args, e);
			}
			finally
			{
				//Rollback
				Args.From.Refresh();
				if (copyFinished && !transferFinished && Args.From.Exists)
				{
					Args.To.Delete();
				}
			}

		}

		public override void Accept(IJobVisitor visitor)
		{
			visitor.Visit(this);
		}

		private void Copy()
		{
			bool destFileDidExist = false;
			bool finishedCopying = false;
			int bufferSize = 1024 * 64;
			int bytesCopied = 0;
			float lastProgress = 0;

			FileStream reader = null;
			FileStream writer = null;

			try
			{
				ct.ThrowIfCancellationRequested();

				Args.To.Refresh();
				destFileDidExist = Args.To.Exists;

				reader = new FileStream(Args.From.FullName, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize, FileOptions.SequentialScan);
				writer = new FileStream(Args.To.FullName, FileMode.CreateNew, FileAccess.Write, FileShare.Read, bufferSize, FileOptions.SequentialScan);


				byte[] buffer = new byte[bufferSize];
				int bytesRead = reader.Read(buffer, 0, buffer.Length);
				while (bytesRead > 0)
				{
					writer.Write(buffer, 0, bytesRead);

					bytesCopied += bytesRead;
					float newProgress = Args.From.Length == 0 ? 95F : bytesCopied * 95.0f / Args.From.Length;
					if (newProgress - lastProgress > 0.1)
					{
						OnProgressChange(newProgress);
					}

					ct.ThrowIfCancellationRequested();

					bytesRead = reader.Read(buffer, 0, buffer.Length);
				}

				finishedCopying = true;
			}
			catch (Exception e) when (e is IOException || e is UnauthorizedAccessException || e is SecurityException)
			{
				throw new FileTransferException(Args, e);
			}
			finally
			{
				reader?.Dispose();
				writer?.Dispose();

				if (!destFileDidExist && !finishedCopying && Args.To.Exists)
				{
					Args.To.Delete();
				}
			}
		}
	}
}
