using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
	class FullScreenCommandFactory : ICommandFactory
	{
		readonly string[] names;

		public FullScreenCommandFactory()
		{
			names = new string[] { "fullscreen", "fs" };
		}

		public FullScreenCommandFactory(string[] cmdNames)
		{
			names = cmdNames;
		}

		public bool Parse(string stringInput, out ICommand parsedCmd)
		{
			parsedCmd = null;

			if (CommandParser.ParseWithoutArgs(stringInput, names))
			{
				parsedCmd = new FullScreenCommand();
				return true;
			}

			return false;
		}
	}
}
