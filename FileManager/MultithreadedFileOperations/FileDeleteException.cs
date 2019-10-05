using System;
using System.Collections.Generic;
using System.Text;

namespace MultithreadedFileOperations
{
	public class FileDeleteException : FileOperationException
	{
		public FileDeleteException(DeleteJobArguments jobArgs, Exception innerException)
			: base(CreateMessage(jobArgs, innerException), innerException)
		{
			JobArgs = jobArgs;
		}

		public DeleteJobArguments JobArgs { get; private set; }

		public static string CreateMessage(DeleteJobArguments jobArgs, Exception innerException)
		{
			return $"A {innerException.GetType()} has been raised when deleting file {jobArgs.Target.FullName}";
		}
	}
}
