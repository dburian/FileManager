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
		readonly string searchedName;

		string pathToSearchedTree;

		public SearchCommand(string searchedName, string pathToSearchedTree)
		{
			this.searchedName = searchedName;
			this.pathToSearchedTree = pathToSearchedTree;
		}
		public SearchCommand(string searchedName) : this(searchedName, ".") { }
	}
}
