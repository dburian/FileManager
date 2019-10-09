namespace FileManager
{
	internal interface IJobsPane : IPane
	{
		int JobsInProgress { get; set; }
		int JobsQueued { get; set; }
	}
}
