using System;

namespace FileManager
{
	internal class FileOrDirectoryMustBeSelectedException : Exception, ICommandException
	{
		private readonly ICommand cmd;
		public FileOrDirectoryMustBeSelectedException(ICommand cmd)
		{
			this.cmd = cmd;
		}

		public override string Message => $"To execute command of type {cmd.GetType()}, there must be at least one file or directory selected.";
		public string Type => "File or directory must be selected to complete this operation";
	}
}
