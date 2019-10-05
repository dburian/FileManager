using System;
using System.Collections.Generic;
using System.Text;

namespace MultithreadedFileOperations
{
	class JobArgumentsVisitor : IJobVisitor, IJobArgumentsView
	{
		public FileTransferJobArguments FileTransferArguments { get; private set; }
		public DirectoryTransferJobArguments DirectoryTransferArguments { get; private set; }

		public DeleteJobArguments DeleteArguments { get; private set; }

		public void Visit(DirectoryCopyJob job)
		{
			DirectoryTransferArguments = job.Args;
		}

		public void Visit(DirectoryMoveJob job)
		{
			DirectoryTransferArguments = job.Args;
		}

		void IJobVisitor.Visit(FileCopyJob job)
		{
			FileTransferArguments = job.Args;
		}

		void IJobVisitor.Visit(FileMoveJob job)
		{
			FileTransferArguments = job.Args;
		}

		void IJobVisitor.Visit(DeleteJob job)
		{
			DeleteArguments = job.Args;
		}

	}
}
