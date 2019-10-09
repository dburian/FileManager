using System;
using System.IO;

namespace FileManager
{
	internal class FileOrDirectoryNotFound : Exception, ICommandException
	{
		private readonly FileSystemInfo info;
		private readonly string path;
		public FileOrDirectoryNotFound(FileSystemInfo info)
		{
			this.info = info;
		}
		public FileOrDirectoryNotFound(string path)
		{
			this.path = path;
		}

		public override string Message
		{
			get
			{
				if (path != null)
				{
					return $"File or directory at {path} could not be found.";
				}
				else
				{
					return info.GetType() == typeof(DirectoryInfo) ?
						$"Directory {info.FullName} could not be found." :
						$"File {info.FullName} could not be found.";
				}
			}
		}

		public string Type => "File or directory not found";
	}
}
