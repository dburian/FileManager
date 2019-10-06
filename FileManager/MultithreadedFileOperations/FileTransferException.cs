using System;
using System.Collections.Generic;
using System.Text;

namespace MultithreadedFileOperations
{
	public class FileTransferException : FileOperationException
	{
		public FileTransferException(FileTransferArguments jobArgs, Exception innerException)
			: base(CreateMessage(jobArgs, innerException), innerException) 
		{
			JobArgs = jobArgs;
		}

		public FileTransferArguments JobArgs { get; private set; }

		private static string CreateMessage(FileTransferArguments jobArgs, Exception innerException)
		{
			return $"A {innerException.GetType()} has been raised when trying to transfer file {jobArgs.From.FullName} to {jobArgs.To.FullName}";
		}
	}
}
