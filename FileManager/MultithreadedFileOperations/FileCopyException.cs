using System;
using System.Collections.Generic;
using System.Text;

namespace MultithreadedFileOperations
{
	public class FileCopyException : FileOperationException
	{

		public FileCopyException(FileTransferJobArguments jobArgs, Exception innerException)  
			: base(CreateMessage(jobArgs, innerException), innerException)
		{
			JobArgs = jobArgs;
		}

		public FileTransferJobArguments JobArgs { get; private set; }
		

		static string CreateMessage(FileTransferJobArguments jobArgs, Exception innerException)
		{
			return $"A {innerException.GetType()} has been raised when copying from {jobArgs.From.FullName} to {jobArgs.To.FullName}";
		}
	}
}
