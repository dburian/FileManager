namespace MultithreadedFileOperations
{
	/// <summary>
	/// All possible job states.
	/// </summary>
	public enum JobStatus
	{
		Waiting,
		Running,
		Done,
		Error,
		Canceled
	}
}
