using System;
using System.Collections.Generic;
using System.Text;

namespace MultithreadedFileOperations
{
	public interface IJobArgumentsView
	{
		FileTransferArguments FileTransferArguments { get; }
		DirectoryTransferArguments DirectoryTransferArguments { get; }
		DeleteJobArguments DeleteArguments { get; }
	}
}
