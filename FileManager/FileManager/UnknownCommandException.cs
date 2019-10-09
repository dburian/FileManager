using System;

namespace FileManager
{
	internal class UnknownCommandException : Exception, ICommandException
	{
		private readonly UnknownCommand unknownCommand;

		public UnknownCommandException(UnknownCommand unknownCommand)
		{
			this.unknownCommand = unknownCommand;
		}

		public string Type => "Unknown command exception";
		public override string Message => $"Command {unknownCommand.enteredCommand} is unknown";
	}
}
