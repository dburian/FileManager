namespace MultithreadedFileSystemOperations
{
	/// <summary>
	/// Implementation of visitor pattern. Extracts arguments from a job.
	/// </summary>
	internal class JobArgumentsVisitor : IJobVisitor, IJobArgumentsView
	{
		public FileTransferArguments FileTransferArguments { get; private set; }
		public DirectoryTransferArguments DirectoryTransferArguments { get; private set; }
		public DeleteJobArguments DeleteArguments { get; private set; }


		public void Visit(DirectoryTransferJob job)
		{
			DirectoryTransferArguments = job.Args;
		}

		void IJobVisitor.Visit(FileTransferJob job)
		{
			FileTransferArguments = job.Args;
		}

		void IJobVisitor.Visit(DeleteJob job)
		{
			DeleteArguments = job.Args;
		}

	}
}
