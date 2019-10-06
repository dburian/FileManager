using System;
using System.Collections.Generic;
using System.Text;

namespace MultithreadedFileOperations
{
	interface IJobVisitor
	{
		void Visit(FileTransferJob job);
		void Visit(DeleteJob job);
		void Visit(DirectoryTransferJob job);
	}
}
