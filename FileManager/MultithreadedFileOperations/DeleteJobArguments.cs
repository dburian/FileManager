using System.IO;

namespace MultithreadedFileSystemOperations
{
	/// <summary>
	/// Encapsulates all the needed information to execute DeleteJob.
	/// </summary>
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
