namespace FileManager
{
	/// <summary>
	/// Common interface for all exceptions which are raised when processing some command.
	/// </summary>
	internal interface ICommandException
	{
		string Message { get; }
		string Type { get; }
	}
}
