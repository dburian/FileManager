using HelperExtensionLibrary;
using System.IO;

namespace FileManager
{
	/// <summary>
	/// Entry which represents a found file.
	/// </summary>
	internal class SearchResultFileEntry : FileEntry
	{
		private readonly string searchedDirPath;
		public SearchResultFileEntry(string searchedDirPath)
		{
			this.searchedDirPath = searchedDirPath;
		}

		public override string EntryName => ((FileInfo)Info).GetRelativePath(searchedDirPath);

	}
}
