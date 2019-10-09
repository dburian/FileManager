using System.IO;

namespace MultithreadedFileOperations
{
	public interface ITransferJobArguments
	{
		FileSystemInfo From { get; }
		FileSystemInfo To { get; }
		TransferSettings Settings { get; }
	}
}
