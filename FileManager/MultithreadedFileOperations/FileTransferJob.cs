using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Security;

namespace MultithreadedFileOperations
{
	//TODO: *Refactor* order usings and delete not used
	class FileTransferJob : Job
	{

		FileOperationException _firstException;

		public FileTransferJob(FileTransferArguments args, CancellationToken ct)
		{
			Args = args;
			this.ct = ct;
			Type = JobType.FileTransfer;

			_firstException = null;
		}
		public FileTransferJob(FileTransferArguments args, CancellationToken ct, OnExceptionRaiseDelegate onExceptionRaise, OnProgressChangeDelegate onProgressChange)
			:this (args, ct)
		{
			ExceptionRaise += onExceptionRaise;
			ProgressChange += onProgressChange;
		}

		public override JobType Type { get; }
		public FileTransferArguments Args { get; private set; }

		public override void Run()
		{
			ct.ThrowIfCancellationRequested();
			bool copyFinished = false;
			bool moveFinished = false;

			try
			{
				Copy();
				copyFinished = true;

				ct.ThrowIfCancellationRequested();
				if (Args.Settings == TransferSettings.DeleteOriginal)
					Args.From.Delete();

				OnProgressChange(100f);
				moveFinished = true;
			}
			//TODO: some of these are ancestors of others
			catch (Exception e) when (e is IOException | e is SecurityException | e is UnauthorizedAccessException)
			{
				var fileE = new FileTransferException(Args, e);
				OnExceptionRaise(fileE);
				throw fileE;
			}
			finally
			{
				//Rollback
				Args.From.Refresh();
				if (copyFinished && !moveFinished && Args.From.Exists) Args.To.Delete();
			}
			
		}

		public override void Accept(IJobVisitor visitor) => visitor.Visit(this);

		private void Copy()
		{
			bool destFileDidExist = false;
			bool finishedCopying = false;
			int bufferSize = 1024 * 64;
			int bytesCopied = 0;

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
					OnProgressChange(Args.From.Length * 90.0f / bytesCopied);

					ct.ThrowIfCancellationRequested();

					bytesRead = reader.Read(buffer, 0, buffer.Length);
				}

				finishedCopying = true;
			}
			catch (Exception e) when (e is IOException || e is UnauthorizedAccessException || e is SecurityException)
			{
				var ex = new FileTransferException(Args, e);
				OnExceptionRaise(ex);
#if TEST
				throw ex;
#endif
			}
			finally
			{
				reader?.Dispose();
				writer?.Dispose();

				if (!destFileDidExist && !finishedCopying && Args.To.Exists) Args.To.Delete();
			}
		}
	}
}
