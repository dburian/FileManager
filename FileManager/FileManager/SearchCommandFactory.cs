using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
	class SearchCommandFactory : ICommandFactory
	{
		readonly string[] names;

		public SearchCommandFactory()
		{
			names = new string[] { "search", "/" };
		}
		public SearchCommandFactory(string[] cmdNames)
		{
			names = cmdNames;
		}

		public bool Parse(string stringInput, out ICommand parsedCmd)
		{
			parsedCmd = null;

			string[] cmd;
			if (CommandParser.ParseWithStrArgs(stringInput, names, out cmd) && cmd.Length > 1 && cmd.Length <= 3)
			{
				parsedCmd = cmd.Length == 2 ? new SearchCommand(cmd[1]) : new SearchCommand(cmd[1], cmd[2]);
				return true;
			}

			return false;
		}
	}
}
