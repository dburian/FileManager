namespace FileManager
{
	internal struct SearchCommand : ICommand
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
