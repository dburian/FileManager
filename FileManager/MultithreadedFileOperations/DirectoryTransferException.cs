using System;
using System.Collections.Generic;
using System.Text;

namespace MultithreadedFileOperations
{
	class DirectoryTransferException : FileOperationException
	{
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
