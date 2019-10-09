namespace MultithreadedFileSystemOperations
{
	/// <summary>
	/// View (read-only reference) of job's arguments 
	/// </summary>
	public interface IJobArgumentsView
	{
		FileTransferArguments FileTransferArguments { get; }
		DirectoryTransferArguments DirectoryTransferArguments { get; }
		DeleteJobArguments DeleteArguments { get; }
	}
}
