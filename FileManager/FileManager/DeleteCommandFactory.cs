namespace FileManager
{
	internal class DeleteCommandFactory : ICommandFactory
	{
		private readonly string[] names;

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
			if (CommandParser.ParseWithStrArgs(stringInput, names, out string[] cmd) && cmd.Length <= 2)
			{
				parsedCmd = cmd.Length == 1 ? new DeleteCommand() : new DeleteCommand(cmd[1]);
				return true;
			}

			return false;
		}
	}
}
