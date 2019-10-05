using System;
using System.Collections.Generic;
using System.Text;

namespace MultithreadedFileOperations
{
	class DirectoryMoveException : FileOperationException
	{
		public DirectoryMoveException(DirectoryTransferJobArguments args, Exception innerException)
		: base(CreateMessage(args, innerException), innerException)
		{
			Args = args;
		}

		public DirectoryTransferJobArguments Args { get; }

		private static string CreateMessage(DirectoryTransferJobArguments jobArgs, Exception innerException)
		{
			return $"A {innerException.GetType()} has been raised when trying to move directory {jobArgs.From.FullName} to {jobArgs.To.FullName}";
		}
	}
}
