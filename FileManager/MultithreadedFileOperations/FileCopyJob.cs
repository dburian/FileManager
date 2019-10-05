using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security;
using System.Threading;

namespace MultithreadedFileOperations
{
	internal class FileCopyJob : Job
	{
		bool finishedCopying = false;
		bool destFileDidExist = false;
		int bufferSize = 1024 * 64;
		long bytesCopied = 0;
		

		public FileCopyJob(FileTransferJobArguments args, CancellationToken ct)
		{
			Args = args;
			this.ct = ct;
			Type = JobType.FileCopy;
		}
		public FileCopyJob(FileTransferJobArguments args, CancellationToken ct, OnExceptionRaiseDelegate onExceptionRaise, OnProgressChangeDelegate onProgressChange)
			:this(args, ct)
		{
			ExceptionRaise += onExceptionRaise;
			ProgressChange += onProgressChange;
		}

		public override JobType Type { get; }
		public FileTransferJobArguments Args { get; }

		public override void Run()
		{
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
					OnProgressChange(Args.From.Length * 100.0f / bytesCopied);

					ct.ThrowIfCancellationRequested();

					bytesRead = reader.Read(buffer, 0, buffer.Length);
				}

				finishedCopying = true;
			} catch (Exception e) when (e is IOException || e is UnauthorizedAccessException || e is SecurityException || e is DirectoryNotFoundException)
			{
				var fileE = new FileCopyException(Args, e);
				OnExceptionRaise(fileE);
				throw fileE;
			}
			finally
			{
				reader?.Dispose();
				writer?.Dispose();

				if (!destFileDidExist && !finishedCopying && Args.To.Exists) Args.To.Delete();
			}
		}

		public override void Accept(IJobVisitor visitor) => visitor.Visit(this);
	}
}
