using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MultithreadedFileOperations
{
	public struct DirectoryTransferArguments : IJobArguments, ITransferJobArguments
	{
		public DirectoryTransferArguments(DirectoryInfo from, DirectoryInfo to, TransferSettings settings)
		{
			From = from;
			To = to;
			Settings = settings;
		}

		public DirectoryInfo From { get; }
		public DirectoryInfo To { get; }
		public TransferSettings Settings { get; }

		FileSystemInfo ITransferJobArguments.From { get => From; }
		FileSystemInfo ITransferJobArguments.To { get => To; }
		TransferSettings ITransferJobArguments.Settings { get => Settings; }
	}
}
