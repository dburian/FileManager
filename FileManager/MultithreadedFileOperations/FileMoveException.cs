using System;
using System.Collections.Generic;
using System.Text;

namespace MultithreadedFileOperations
{
	public class FileMoveException : FileOperationException
	{
		public FileMoveException(FileTransferJobArguments jobArgs, Exception innerException)
			: base(CreateMessage(jobArgs, innerException), innerException) 
		{
			JobArgs = jobArgs;
		}

		public FileTransferJobArguments JobArgs { get; private set; }

		private static string CreateMessage(FileTransferJobArguments jobArgs, Exception innerException)
		{
			return $"A {innerException.GetType()} has been raised when trying to move file {jobArgs.From.FullName} to {jobArgs.To.FullName}";
		}
	}
}
