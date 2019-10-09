using System.IO;

namespace MultithreadedFileSystemOperations
{
	public interface ITransferJobArguments
	{
		FileSystemInfo From { get; }
		FileSystemInfo To { get; }
		TransferSettings Settings { get; }
	}
}
