using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
	class DeleteCommandFactory : ICommandFactory
	{
		readonly string[] names = new string[] { "delete", "d" };

		bool initialized = false;
		DeleteCommand parsedCmd;

		public ICommand GetCommandInstance() => initialized ? parsedCmd : throw new InvalidOperationException();
		public bool Parse(string stringInput)
		{
			if (CommandParser.ParseWithoutArgs(stringInput, names))
			{
				initialized = true;
				parsedCmd = new DeleteCommand();
				return true;
			}

			return false;
		}
	}
}
