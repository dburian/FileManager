using System;
using System.Collections.Generic;
using System.Text;

namespace MultithreadedFileOperations
{
	public interface IJobArgumentsView
	{
		FileTransferJobArguments FileTransferArguments { get; }
		DirectoryTransferJobArguments DirectoryTransferArguments { get; }
		DeleteJobArguments DeleteArguments { get; }
	}
}
