using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
	class MoveCommandFactory : ICommandFactory
	{
		readonly string[] names = new string[] { "move", "mv" };

		MoveCommand parsedCmd;
		bool initialized = false;

		public ICommand GetCommandInstance() => initialized ? parsedCmd : throw new InvalidOperationException();
		public bool Parse(string stringInput)
		{
			if (CommandParser.ParseWithoutArgs(stringInput, names))
			{
				initialized = true;
				parsedCmd = new MoveCommand();
				return true;
			}

			return false;
		}
	}
}
