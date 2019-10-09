namespace FileManager
{
	internal class SearchCommandFactory : ICommandFactory
	{
		private readonly string[] names;

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

			if (CommandParser.ParseWithStrArgs(stringInput, names, out string[] cmd) && cmd.Length > 1 && cmd.Length <= 3)
			{
				parsedCmd = cmd.Length == 2 ? new SearchCommand(cmd[1]) : new SearchCommand(cmd[1], cmd[2]);
				return true;
			}

			return false;
		}
	}
}
