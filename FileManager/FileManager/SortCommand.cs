using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
	struct SortCommand : ICommand
	{
		public Comparison<FileSystemNodeEntry> Comparer { get; private set; }
		public SortCommand(Comparison<FileSystemNodeEntry> comparer)
		{
			this.Comparer = comparer;
		}
	}
}
