using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
	interface IJobsPane : IPane
	{
		int JobsInProgress { get; set; }
		int JobsQueued { get; set; }
		int JobsSelected { get; set; }
	}
}
