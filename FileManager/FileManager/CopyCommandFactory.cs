using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
	class CopyCommandFactory : ICommandFactory
	{
		readonly string[] names;

		public CopyCommandFactory()
		{
			this.names = new string[] { "copy", "cp", "yank", "y" };
		}
		public CopyCommandFactory(string[] cmdNames)
		{
			this.names = cmdNames;
		}

		public bool Parse(string stringInput, out ICommand parsedCmd)
		{
			parsedCmd = null;
			string[] cmd;
			if (CommandParser.ParseWithStrArgs(stringInput, names, out cmd) && cmd.Length <= 2)
			{
				parsedCmd = cmd.Length == 1 ? new CopyCommand() : new CopyCommand(cmd[1]);
				return true;
			}

			return false;
		}
	}

}
