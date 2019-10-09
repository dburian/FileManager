namespace FileManager
{
	internal struct DeleteCommand : ICommand
	{
		public DeleteCommand(string targetPath)
		{
			TargetPath = targetPath;
		}

		public string TargetPath { get; }
	}
}
