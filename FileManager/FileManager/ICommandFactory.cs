namespace FileManager
{
	/// <summary>
	/// Factory which is able to create command out of a string input.
	/// </summary>
	internal interface ICommandFactory
	{
		bool Parse(string stringInput, out ICommand parsedCmd);
	}
}
