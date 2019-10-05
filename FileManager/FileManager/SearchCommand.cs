using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileManager
{
	struct SearchCommand : ICommand
	{

		public SearchCommand(string searchedName, string pathToSearchedTree)
		{
			SearchedName = searchedName;
			SearchedDirectory = pathToSearchedTree;
		}
		public SearchCommand(string searchedName) : this(searchedName, ".") { }

		public string SearchedName { get; }
		public string SearchedDirectory { get; }
	}
}
