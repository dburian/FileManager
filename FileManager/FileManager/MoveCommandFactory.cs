using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
	class MoveCommandFactory : ICommandFactory
	{
		readonly string[] names;

		public MoveCommandFactory()
		{
			names = new string[] { "move", "mv" };
		}
		public MoveCommandFactory(string[] cmdNames)
		{
			names = cmdNames;
		}

		public bool Parse(string stringInput, out ICommand parsedCmd)
		{
			parsedCmd = null;
			string[] cmd;
			if (CommandParser.ParseWithStrArgs(stringInput, names, out cmd) && cmd.Length <= 2)
			{
				parsedCmd = cmd.Length == 1 ? new MoveCommand() : new MoveCommand(cmd[1]);
				return true;
			}

			return false;
		}
	}
}
