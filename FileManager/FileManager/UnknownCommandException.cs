using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
	class UnknownCommandException : Exception, ICommandException
	{
		UnknownCommand unknownCommand;

		public UnknownCommandException(UnknownCommand unknownCommand)
		{
			this.unknownCommand = unknownCommand;
		}

		public string Type => "Unknown command exception";
		public override string Message => $"Command {unknownCommand.enteredCommand} is unknown";
	}
}
