using System.IO;

namespace FileManager
{
	/// <summary>
	/// Specialization of IPane. View interface to FilesPane.
	/// </summary>
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
