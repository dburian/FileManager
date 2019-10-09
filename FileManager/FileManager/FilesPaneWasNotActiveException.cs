using System;

namespace FileManager
{
	internal class FilesPaneWasNotActiveException : Exception, ICommandException
	{
		private readonly ICommand cmd;

		public FilesPaneWasNotActiveException(ICommand cmd)
		{
			this.cmd = cmd;
		}

		public override string Message => $"{cmd} can be run only on an active files pane.";

		public string Type => $"Files pane wasn't active";

		internal FullScreenCommandFactory FullScreenCommandFactory
		{
			get => default;
			set
			{
			}
		}
	}
}
