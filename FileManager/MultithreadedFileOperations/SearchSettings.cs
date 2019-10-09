using System.IO;

namespace MultithreadedFileSystemOperations
{
	/// <summary>
	/// Encapsulates all the information needed to execute a FileSystemNodeSearch operation.
	/// </summary>
	public struct SearchSettings
	{
		public SearchSettings(SearchTarget fileSystemNodeName, DirectoryInfo inDir, bool searchSubdirecotries)
		{
			Target = fileSystemNodeName;
			SearchSubdirectories = searchSubdirecotries;
			InDirectory = inDir;
		}

		public SearchTarget Target { get; }
		public bool SearchSubdirectories { get; }
		public DirectoryInfo InDirectory { get; }
	}
}
