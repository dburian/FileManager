using System;
using System.Collections.Generic;
using System.Text;

namespace MultithreadedFileOperations
{
	public delegate void OnJobChangeDelegate(IJobView job, JobChangeEvent changeEvent);
}
