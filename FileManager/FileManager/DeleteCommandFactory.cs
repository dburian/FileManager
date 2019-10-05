using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
	class DeleteCommandFactory : ICommandFactory
	{
		readonly string[] names;

		public DeleteCommandFactory()
		{
			this.names = new string[] { "delete", "d" };
		}
		public DeleteCommandFactory(string[] cmdNames)
		{
			names = cmdNames;
		}

		public bool Parse(string stringInput, out ICommand parsedCmd)
		{
			parsedCmd = null;
			string[] cmd;
			if (CommandParser.ParseWithStrArgs(stringInput, names, out cmd) && cmd.Length <= 2)
			{
				parsedCmd = cmd.Length == 1 ? new DeleteCommand() : new DeleteCommand(cmd[1]);
				return true;
			}

			return false;
		}
	}
}
