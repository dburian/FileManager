using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MultithreadedFileOperations
{
	public struct DirectoryTransferJobArguments : IJobArguments, ITransferJobArguments
	{
		public DirectoryTransferJobArguments(DirectoryInfo from, DirectoryInfo to)
		{
			From = from;
			To = to;
		}

		public DirectoryInfo From { get; }
		public DirectoryInfo To { get; }

		FileSystemInfo ITransferJobArguments.From { get => From; }
		FileSystemInfo ITransferJobArguments.To { get => To; }
	}
}
