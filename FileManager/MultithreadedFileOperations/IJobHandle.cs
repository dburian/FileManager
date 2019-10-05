using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MultithreadedFileOperations
{
	interface IJobHandle
	{
		Task Task { get; }
		int Id { get; set; }

		event OnJobChangeDelegate JobChange;

		void Run();
		void Cancel();
		IJobView GetView();
	}
}
