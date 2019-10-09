namespace MultithreadedFileOperations
{
	/// <summary>
	/// View (read-only interface) of an enqueued or running job.
	/// </summary>
	public interface IJobView
	{
		int Id { get; }
		JobStatus LastStatus { get; }
		FileOperationException Exception { get; }
		float Progress { get; }

		JobType Type { get; }

		IJobArgumentsView GetArgumentsView();
	}
}
