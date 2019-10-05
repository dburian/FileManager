using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
	class LeftRightCommandFactory : ICommandFactory
	{
		readonly string[] names;

		public LeftRightCommandFactory()
		{
			names = new string[] { "left", "right" };
		}
		public LeftRightCommandFactory(string[] cmdNames)
		{
			names = cmdNames;
		}

		public bool Parse(string stringInput, out ICommand parsedCmd)
		{
			parsedCmd = null;

			string[] cmd;
			if (!CommandParser.ParseWithStrArgs(stringInput, names, out cmd) || cmd.Length <= 1 || cmd.Length > 2)
				return false;

			Panes pane;
			if (!Enum.TryParse(cmd[1], true, out pane)) return false;

			if (cmd[0] == "left") parsedCmd = new LeftCommand(pane);
			else parsedCmd = new RightCommand(pane);

			return true;
		}
	}
}
