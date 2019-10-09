namespace FileManager
{
	internal class MoveCommandFactory : ICommandFactory
	{
		private readonly string[] names;

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
			if (CommandParser.ParseWithStrArgs(stringInput, names, out string[] cmd) && cmd.Length <= 2)
			{
				parsedCmd = cmd.Length == 1 ? new MoveCommand() : new MoveCommand(cmd[1]);
				return true;
			}

			return false;
		}
	}
}
