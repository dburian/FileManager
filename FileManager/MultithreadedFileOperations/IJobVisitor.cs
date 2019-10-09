namespace MultithreadedFileOperations
{
	internal interface IJobVisitor
	{
		void Visit(FileTransferJob job);
		void Visit(DeleteJob job);
		void Visit(DirectoryTransferJob job);
	}
}
