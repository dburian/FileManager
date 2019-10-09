using System.IO;

namespace MultithreadedFileOperations
{
	/// <summary>
	/// Encapsulates all the information needed to execute FileTransferOperation.
	/// </summary>
	public struct FileTransferArguments : ITransferJobArguments
	{

		public FileTransferArguments(FileInfo from, FileInfo to, TransferSettings settings)
		{
			From = from;
			To = to;
			Settings = settings;
		}

		public FileInfo From { get; }
		public FileInfo To { get; }
		public TransferSettings Settings { get; }

		FileSystemInfo ITransferJobArguments.From => From;
		FileSystemInfo ITransferJobArguments.To => To;
		TransferSettings ITransferJobArguments.Settings => Settings;

	}
}
