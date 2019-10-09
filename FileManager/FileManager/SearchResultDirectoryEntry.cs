using HelperExtensionLibrary;
using System.IO;

namespace FileManager
{
	/// <summary>
	/// Entry which represents a found directory.
	/// </summary>
	internal class SearchResultDirectoryEntry : DirectoryEntry
	{
		private readonly string searchedDirPath;

		public SearchResultDirectoryEntry(string searchedDirPath)
		{
			this.searchedDirPath = searchedDirPath;
		}

		public override string EntryName => ((DirectoryInfo)Info).GetRelativePath(searchedDirPath);

	}
}
