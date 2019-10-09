using MultithreadedFileSystemOperations;

namespace FileManager
{
	/// <summary>
	/// Specialization of IPane. View interface to SearchResultPane.
	/// </summary>
	internal interface ISearchResultPane : IPane
	{
		int Found { get; set; }
		string SearchingName { get; set; }
		string InDirectory { set; }
		JobStatus Status { get; set; }
	}
}
