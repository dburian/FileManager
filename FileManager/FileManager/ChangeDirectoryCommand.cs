namespace FileManager
{
	internal struct ChangeDirectoryCommand : ICommand
	{
		public ChangeDirectoryCommand(string targetPath)
		{
			this.TargetPath = targetPath;
		}

		public string TargetPath { get; }

	}
}
