using System;

namespace MultithreadedFileOperations
{
	public class FileTransferException : FileOperationException
	{
		public FileTransferException()
		{ }

		public FileTransferException(string message) : base(message)
		{ }

		public FileTransferException(string message, Exception innerException) : base(message, innerException)
		{ }
		public FileTransferException(FileTransferArguments jobArgs, Exception innerException)
			: base(CreateMessage(jobArgs, innerException), innerException)
		{
			JobArgs = jobArgs;
		}

		public FileTransferArguments JobArgs { get; }

		private static string CreateMessage(FileTransferArguments jobArgs, Exception innerException)
		{
			return $"A {innerException.GetType()} has been raised when trying to transfer file {jobArgs.From.FullName} to {jobArgs.To.FullName}";
		}

	}
}
