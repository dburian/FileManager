using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
	class SearchedDirectoryDoesNotExist : Exception, ICommandException
	{
		string pathToSearchedDir;

		public SearchedDirectoryDoesNotExist(string pathToSearchedDir)
		{
			this.pathToSearchedDir = pathToSearchedDir;
		}
		public string Type => "Searched directory does not exist";
		public override string Message => $"Directory to search {pathToSearchedDir} does not exist.";
	}
}
