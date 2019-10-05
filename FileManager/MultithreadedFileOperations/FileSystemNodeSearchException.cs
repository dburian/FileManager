using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MultithreadedFileOperations
{
	class FileSystemNodeSearchException : FileOperationException
	{
		DirectoryInfo searchedDir;
		public FileSystemNodeSearchException(DirectoryInfo searchedDir, Exception innerException) 
			: base(CreateMessage(searchedDir, innerException), innerException)
		{
			this.searchedDir = searchedDir;
		}

		static string CreateMessage(DirectoryInfo searchedDir, Exception innerException)
		{
			return $"A {innerException.GetType()} was thrown when searching {searchedDir.FullName}";
		}
	}
}
