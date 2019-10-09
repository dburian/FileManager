using System;
using System.IO;

namespace MultithreadedFileSystemOperations
{
	internal class FileSystemNodeSearchException : FileOperationException
	{
		public FileSystemNodeSearchException()
		{ }

		public FileSystemNodeSearchException(string message) : base(message)
		{ }

		public FileSystemNodeSearchException(string message, Exception innerException) : base(message, innerException)
		{ }
		public FileSystemNodeSearchException(DirectoryInfo searchedDir, Exception innerException)
			: base(CreateMessage(searchedDir, innerException), innerException)
		{ }

		private static string CreateMessage(DirectoryInfo searchedDir, Exception innerException)
		{
			return $"A {innerException.GetType()} was thrown when searching {searchedDir.FullName}";
		}

	}
}
