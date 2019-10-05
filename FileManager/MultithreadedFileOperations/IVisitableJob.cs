using System;
using System.Collections.Generic;
using System.Text;

namespace MultithreadedFileOperations
{
	internal interface IVisitableJob
	{
		void Accept(IJobVisitor visitor);
	}
}
