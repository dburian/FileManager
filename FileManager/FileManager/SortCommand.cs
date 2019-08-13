using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
	struct SortCommand : ICommand
	{
		Comparer<FilesViewEntry> comparer;
		public SortCommand(Comparer<FilesViewEntry> comparer)
		{
			this.comparer = comparer;
		}
	}
}
