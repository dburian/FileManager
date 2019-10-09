using System;

namespace MultithreadedFileOperations
{
	/// <summary>
	/// Base class for all exeptions in MultithreadedFileOperations library.
	/// </summary>
	public abstract class FileOperationException : Exception
	{
		public FileOperationException()
			: base("A FileOperationException has been thrown.")
		{ }
		public FileOperationException(string message) : base(message) { }
		public FileOperationException(string message, Exception innerException) : base(message, innerException) { }

		public override string ToString()
		{
			Exception e = this;
			string message = Message;

			while (e.InnerException != null)
			{
				message += $"\nInnerException<{e.InnerException.GetType()}>: {e.InnerException.Message}";
				e = e.InnerException;
			}

			return message;
		}


	}
}
