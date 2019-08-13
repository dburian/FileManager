using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
	static class CommandParser
	{
		/// <summary>
		/// Splits cmdLine into cmd.
		/// </summary>
		/// <param name="cmdLine">Raw string command.</param>
		/// <param name="cmdNames">Possible names of the command.</param>
		/// <param name="cmd">Parsed cmd.</param>
		public static bool ParseWithStrArgs(string cmdLine, string[] cmdNames, out string[] cmd)
		{
			cmd = cmdLine.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

			if (cmd.Length < 1) return false;

			foreach (var name in cmdNames)
				if (cmd[0] == name) return true;

			return false;
		}

		/// <summary>
		/// Parses cmdLine into cmd.
		/// </summary>
		/// <param name="cmdLine">Raw string command.</param>
		/// <param name="cmdNames">Possible names of the command.</param>
		/// <returns>Returns true if name of the command matches one of the names in cmdNames and no arguments were provided.</returns>
		public static bool ParseWithoutArgs(string cmdLine, string[] cmdNames)
		{
			string[] args;
			return ParseWithStrArgs(cmdLine, cmdNames, out args) && args.Length == 1;
		}
	}
}
