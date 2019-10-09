using System;

namespace FileManager
{
	/// <summary>
	/// Helper class containing common parser templates.
	/// </summary>
	internal static class CommandParser
	{
		/// <summary>
		/// Splits cmdLine into words and checks if the command is valid.
		/// </summary>
		/// <param name="cmdLine">Raw string command.</param>
		/// <param name="cmdNames">Possible names of the command.</param>
		/// <param name="cmd">Parsed cmd.</param>
		/// <returns>True if command is succesfully parsed, false otherwise.</returns>
		public static bool ParseWithStrArgs(string cmdLine, string[] cmdNames, out string[] cmd)
		{
			cmd = cmdLine.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

			if (cmd.Length < 1)
			{
				return false;
			}

			foreach (var name in cmdNames)
			{
				if (cmd[0] == name)
				{
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Parses cmdLine and checks if it has one of the cmdNames as first and only word.
		/// </summary>
		/// <param name="cmdLine">Raw string command.</param>
		/// <param name="cmdNames">Possible names of the command.</param>
		/// <returns>Returns true if name of the command matches one of the names in cmdNames and no command arguments were provided.</returns>
		public static bool ParseWithoutArgs(string cmdLine, string[] cmdNames)
		{
			return ParseWithStrArgs(cmdLine, cmdNames, out string[] args) && args.Length == 1;
		}
	}
}
