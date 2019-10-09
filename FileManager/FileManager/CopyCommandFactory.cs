namespace FileManager
{
	internal class CopyCommandFactory : ICommandFactory
	{
		private readonly string[] names;

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
			if (CommandParser.ParseWithStrArgs(stringInput, names, out string[] cmd) && cmd.Length <= 2)
			{
				parsedCmd = cmd.Length == 1 ? new CopyCommand() : new CopyCommand(cmd[1]);
				return true;
			}

			return false;
		}
	}

}
