using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
	class TransferDirectoryIntoFileException : ICommandException
	{
		ICommand cmd;
		string dirPath;
		string filePath;

		public TransferDirectoryIntoFileException(ICommand cmd, string dirPath, string filePath)
		{
			this.cmd = cmd;
			this.dirPath = dirPath;
			this.filePath = filePath;
		}
		public string Message 
		{
			get {
				return cmd.GetType() == typeof(CopyCommand) ?
					$"Cannot copy directory {dirPath} to file {filePath}." :
					$"Cannot move directory {dirPath} to file {filePath}.";
			}
		}

		public string Type 
		{
			get => "Transfer directory into file exception."; 
		}
	}
}
