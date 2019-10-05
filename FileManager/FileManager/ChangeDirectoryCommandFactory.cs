using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
	class ChangeDirectoryCommandFactory : ICommandFactory
	{
		readonly string[] names;

		public ChangeDirectoryCommandFactory()
		{
			names = new string[] { "cd" };
		}
		public ChangeDirectoryCommandFactory(string[] cmdNames)
		{
			names = cmdNames;
		}

		public bool Parse(string stringInput, out ICommand parsedCmd)
		{
			parsedCmd = null;

			string[] cmd;
			if (!CommandParser.ParseWithStrArgs(stringInput, names, out cmd) || cmd.Length != 2) return false;

			parsedCmd = new ChangeDirectoryCommand(cmd[1]);

			return true;
		}
	}
}
