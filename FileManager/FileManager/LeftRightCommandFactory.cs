using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
	class LeftRightCommandFactory : ICommandFactory
	{
		readonly string[] names = new string[] { "left", "right" };

		bool initialized = false;
		ICommand parsedCmd;

		public ICommand GetCommandInstance() => initialized ? parsedCmd : throw new InvalidOperationException();
		public bool Parse(string stringInput)
		{
			string[] cmd;
			if (!CommandParser.ParseWithStrArgs(stringInput, names, out cmd) || cmd.Length <= 1 || cmd.Length > 2)
				return false;

			Panes pane;
			if (!Enum.TryParse(cmd[1], out pane)) return false;

			if (cmd[0] == "left") parsedCmd = new LeftCommand(pane);
			else parsedCmd = new RightCommand(pane);

			initialized = true;
			return true;
		}
	}
}
