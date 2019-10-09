namespace MultithreadedFileOperations
{
	internal interface IVisitableJob
	{
		void Accept(IJobVisitor visitor);
	}
}
