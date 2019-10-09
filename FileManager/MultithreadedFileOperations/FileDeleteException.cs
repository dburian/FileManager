using System;

namespace MultithreadedFileSystemOperations
{
	public class DeleteException : FileOperationException
	{
		public DeleteException()
		{ }

		public DeleteException(string message) : base(message)
		{ }

		public DeleteException(string message, Exception innerException) : base(message, innerException)
		{ }
		public DeleteException(DeleteJobArguments jobArgs, Exception innerException)
			: base(CreateMessage(jobArgs, innerException), innerException)
		{
			JobArgs = jobArgs;
		}

		public DeleteJobArguments JobArgs { get; }

		public static string CreateMessage(DeleteJobArguments jobArgs, Exception innerException)
		{
			return $"A {innerException.GetType()} has been raised when deleting file {jobArgs.Target.FullName}";
		}


	}
}
