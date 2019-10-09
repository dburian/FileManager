namespace MultithreadedFileOperations
{
	/// <summary>
	/// Possible changes to a job's state.
	/// </summary>
	public enum JobChangeEvent
	{
		Enqueued,
		BeforeRun,
		AfterCompleted,
		OnProgressChange,
		ExceptionThrown,
		Canceled
	}
}
