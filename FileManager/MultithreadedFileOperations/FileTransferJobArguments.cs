using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MultithreadedFileOperations
{
	public struct FileTransferJobArguments : IJobArguments, ITransferJobArguments
	{

		public FileTransferJobArguments(FileInfo from, FileInfo to)
		{
			From = from;
			To = to;
		}

		//TODO: get only properties exist...
		public FileInfo From { get; }
		public FileInfo To { get; }

		FileSystemInfo ITransferJobArguments.From { get => From; }
		FileSystemInfo ITransferJobArguments.To { get => To; }
	}
}
