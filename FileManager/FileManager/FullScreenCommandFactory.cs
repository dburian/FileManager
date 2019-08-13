using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
	class FullScreenCommandFactory : ICommandFactory
	{
		readonly string[] names = new string[] { "fullscreen", "fs" };

		bool initialized = false;
		FullScreenCommand parsedCmd;

		public ICommand GetCommandInstance() => initialized ? parsedCmd : throw new InvalidOperationException();

		public bool Parse(string stringInput)
		{
			if(CommandParser.ParseWithoutArgs(stringInput, names))
			{
				parsedCmd = new FullScreenCommand();
				initialized = true;
				return true;
			}

			return false;
		}
	}
}
