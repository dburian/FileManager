using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
	class CopyCommandFactory : ICommandFactory
	{
		readonly string[] names = new string[] { "copy", "cp", "yank", "y" };

		bool initialized = false;
		CopyCommand parsedCmd;

		public ICommand GetCommandInstance() => initialized ? parsedCmd : throw new InvalidOperationException();
		public bool Parse(string stringInput)
		{
			if (CommandParser.ParseWithoutArgs(stringInput, names))
			{
				initialized = true;
				parsedCmd = new CopyCommand();
				return true;
			}

			return false;
		}
	}
}
