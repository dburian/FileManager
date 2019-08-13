using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
	struct UnknownCommand : ICommand
	{
		string unknownCmd;

		public UnknownCommand(string unknownCommand)
		{
			this.unknownCmd = unknownCommand;
		}
	}
}
