using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
	static class RegisteredCommands
	{
		readonly static ICommandFactory[] commandFactories;
		static RegisteredCommands()
		{
			commandFactories = new ICommandFactory[] {
				new CopyCommandFactory(),
				new MoveCommandFactory(),
				new DeleteCommandFactory(),
				new SearchCommandFactory(),
				new SortCommandFactory(),
				new LeftRightCommandFactory(),
				new FullScreenCommandFactory()
			};
		}

		public static IEnumerable<ICommandFactory> GetCommandFactories()
		{
			foreach (var factory in commandFactories)
				yield return factory;
		}
		public static ICommand GetUnknownCommand(string unknownCommand)
		{
			return new UnknownCommand(unknownCommand);
		}
	}
}
