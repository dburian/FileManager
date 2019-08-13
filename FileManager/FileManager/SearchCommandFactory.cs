using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
	class SearchCommandFactory : ICommandFactory
	{
		readonly string[] names = new string[] { "search", "/" };


		bool initialized = false;
		SearchCommand parsedCmd;

		public ICommand GetCommandInstance() => initialized ? parsedCmd : throw new InvalidOperationException();
		public bool Parse(string stringInput)
		{
			string[] cmd;
			if (CommandParser.ParseWithStrArgs(stringInput, names, out cmd) && cmd.Length > 1 && cmd.Length <= 3)
			{
				initialized = true;
				parsedCmd = cmd.Length == 2 ? new SearchCommand(cmd[1]) : new SearchCommand(cmd[1], cmd[2]);
				return true;
			}

			return false;
		}
	}
}
