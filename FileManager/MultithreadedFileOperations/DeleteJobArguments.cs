using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MultithreadedFileOperations
{
	public struct DeleteJobArguments
	{
		public DeleteJobArguments(FileSystemInfo target)
		{
			Target = target;
			Recursively = false;
		}
		public DeleteJobArguments(DirectoryInfo dirTarget, bool deleteRecursively)
		{
			Target = dirTarget;
			Recursively = deleteRecursively;
		}

		public FileSystemInfo Target { get; }
		public bool Recursively { get; }
	}
}
