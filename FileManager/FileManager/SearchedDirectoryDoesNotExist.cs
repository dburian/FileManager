using System;

namespace FileManager
{
	public class SearchedDirectoryDoesNotExist : Exception, ICommandException
	{
		private readonly string pathToSearchedDir;

		public SearchedDirectoryDoesNotExist(string pathToSearchedDir)
		{
			this.pathToSearchedDir = pathToSearchedDir;
		}
		public string Type => "Searched directory does not exist";
		public override string Message => $"Directory to search {pathToSearchedDir} does not exist.";
	}
}
