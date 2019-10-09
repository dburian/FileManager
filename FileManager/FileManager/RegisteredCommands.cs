using System.Collections.Generic;

namespace FileManager
{
	/// <summary>
	/// Collection of all known types of command factories.
	/// </summary>
	internal static class RegisteredCommands
	{
		private static readonly ICommandFactory[] commandFactories = new ICommandFactory[] {
																		new CopyCommandFactory(),
																		new MoveCommandFactory(),
																		new DeleteCommandFactory(),
																		new SearchCommandFactory(),
																		new SortCommandFactory(),
																		new LeftRightCommandFactory(),
																		new FullScreenCommandFactory(),
																		new ChangeDirectoryCommandFactory(),
																	};

		public static IEnumerable<ICommandFactory> GetCommandFactories()
		{
			foreach (var factory in commandFactories)
			{
				yield return factory;
			}
		}
		public static ICommand GetUnknownCommand(string unknownCommand)
		{
			return new UnknownCommand(unknownCommand);
		}
	}
}
