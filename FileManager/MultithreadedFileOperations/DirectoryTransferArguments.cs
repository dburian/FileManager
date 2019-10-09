using System.IO;

namespace MultithreadedFileSystemOperations
{
	/// <summary>
	/// Encapsulates all the needed information to execute DiretoryTransferJob
	/// </summary>
	public struct DirectoryTransferArguments : ITransferJobArguments
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

		FileSystemInfo ITransferJobArguments.From => From;
		FileSystemInfo ITransferJobArguments.To => To;
		TransferSettings ITransferJobArguments.Settings => Settings;
	}
}
