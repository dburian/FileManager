namespace FileManager
{
	/// <summary>
	/// Command representing all unknown commands.
	/// </summary>
	internal struct UnknownCommand : ICommand
	{
		public readonly string enteredCommand;

		public UnknownCommand(string unknownCommand)
		{
			this.enteredCommand = unknownCommand;
		}
	}
}
