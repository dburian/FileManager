using System;
using System.Collections.Generic;
using System.Text;

namespace MultithreadedFileOperations
{
	public interface IJobView
	{
		int Id { get; }
		FileOperationException Exception { get; }
		float Progress { get; }

		JobType Type { get; }

		IJobArgumentsView GetArgumentsView();
	}
}
