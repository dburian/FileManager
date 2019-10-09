namespace FileManager
{
	/// <summary>
	/// Specialization of IPane. View interface to a JobsPane.
	/// </summary>
	internal interface IJobsPane : IPane
	{
		int JobsInProgress { get; set; }
		int JobsQueued { get; set; }
	}
}
