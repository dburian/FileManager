namespace MultithreadedFileSystemOperations
{
	internal interface IVisitableJob
	{
		void Accept(IJobVisitor visitor);
	}
}
