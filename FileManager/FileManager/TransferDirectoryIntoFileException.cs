namespace FileManager
{
	internal class TransferDirectoryIntoFileException : ICommandException
	{
		private readonly ICommand cmd;
		private readonly string dirPath;
		private readonly string filePath;

		public TransferDirectoryIntoFileException(ICommand cmd, string dirPath, string filePath)
		{
			this.cmd = cmd;
			this.dirPath = dirPath;
			this.filePath = filePath;
		}
		public string Message => cmd.GetType() == typeof(CopyCommand) ?
					$"Cannot copy directory {dirPath} to file {filePath}." :
					$"Cannot move directory {dirPath} to file {filePath}.";

		public string Type => "Transfer directory into file exception.";
	}
}
