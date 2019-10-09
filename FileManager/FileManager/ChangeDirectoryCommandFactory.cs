namespace FileManager
{
	internal class ChangeDirectoryCommandFactory : ICommandFactory
	{
		private readonly string[] names;

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

			if (!CommandParser.ParseWithStrArgs(stringInput, names, out string[] cmd) || cmd.Length != 2)
			{
				return false;
			}

			parsedCmd = new ChangeDirectoryCommand(cmd[1]);

			return true;
		}
	}
}
