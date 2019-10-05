using System;
using System.Collections.Generic;
using System.Text;

namespace MultithreadedFileOperations
{
	public abstract class FileOperationException : Exception
	{
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
