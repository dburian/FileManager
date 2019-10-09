using System;

namespace FileManager
{
	internal class LeftRightCommandFactory : ICommandFactory
	{
		private readonly string[] names;

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

			if (!CommandParser.ParseWithStrArgs(stringInput, names, out string[] cmd) || cmd.Length <= 1 || cmd.Length > 2)
			{
				return false;
			}

			if (!Enum.TryParse(cmd[1], true, out Panes pane))
			{
				return false;
			}

			if (cmd[0] == "left")
			{
				parsedCmd = new LeftCommand(pane);
			}
			else
			{
				parsedCmd = new RightCommand(pane);
			}

			return true;
		}
	}
}
