using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
	struct UnknownCommand : ICommand
	{
		public readonly string enteredCommand;

		public UnknownCommand(string unknownCommand)
		{
			this.enteredCommand = unknownCommand;
		}
	}
}
