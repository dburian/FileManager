using MultithreadedFileOperations;

namespace FileManager
{
	internal interface ISearchResultPane : IPane
	{
		int Found { get; set; }
		string SearchingName { get; set; }
		string InDirectory { set; }
		JobStatus Status { get; set; }
	}
}
