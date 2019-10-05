using System;
using System.Collections.Generic;
using System.Text;

namespace MultithreadedFileOperations
{
	interface IJobVisitor
	{
		void Visit(FileCopyJob job);
		void Visit(FileMoveJob job);
		void Visit(DeleteJob job);
		void Visit(DirectoryCopyJob job);
		void Visit(DirectoryMoveJob job);
	}
}
