using System.IO;

namespace FileManager
{
	public interface IFilesPane : IPane
	{
		DirectoryInfo CurrentDir { get; set; }
		long FreeSpaceInDir { set; }
		int SelectedEntriesCount { get; set; }
		long SelectedEntriesSize { get; set; }
		int FileEntriesCount { set; }
		int DirectoryEntriesCount { set; }
	}
}
