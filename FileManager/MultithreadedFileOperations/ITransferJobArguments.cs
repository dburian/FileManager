using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MultithreadedFileOperations
{
	public interface ITransferJobArguments
	{
		FileSystemInfo From { get; }
		FileSystemInfo To { get; }
	}
}
