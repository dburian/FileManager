using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MultithreadedFileOperations
{
	public struct FileTransferArguments : IJobArguments, ITransferJobArguments
	{

		public FileTransferArguments(FileInfo from, FileInfo to, TransferSettings settings)
		{
			From = from;
			To = to;
			Settings = settings;
		}

		//TODO: get only properties exist...
		public FileInfo From { get; }
		public FileInfo To { get; }
		public TransferSettings Settings { get; }

		FileSystemInfo ITransferJobArguments.From { get => From; }
		FileSystemInfo ITransferJobArguments.To { get => To; }
		TransferSettings ITransferJobArguments.Settings { get => Settings; }

	}
}
