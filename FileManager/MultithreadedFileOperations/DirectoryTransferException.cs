using System;

namespace MultithreadedFileOperations
{
	internal class DirectoryTransferException : FileOperationException
	{
		public DirectoryTransferException()
		{ }

		public DirectoryTransferException(string message) : base(message)
		{ }

		public DirectoryTransferException(string message, Exception innerException) : base(message, innerException)
		{ }
		public DirectoryTransferException(DirectoryTransferArguments args, Exception innerException)
		: base(CreateMessage(args, innerException), innerException)
		{
			Args = args;
		}

		public DirectoryTransferArguments Args { get; }

		private static string CreateMessage(DirectoryTransferArguments jobArgs, Exception innerException)
		{
			return $"A {innerException.GetType()} has been raised when trying to transfer directory {jobArgs.From.FullName} to {jobArgs.To.FullName} with settings {jobArgs.Settings}";
		}


	}
}
