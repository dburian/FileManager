using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
	interface ISearchResultPane : IPane
	{
		int Found { get; set; }
		string SearchingName { get; set; }
		string InDirectory { set; }
		JobStatus Status { get; set; }
	}
}
