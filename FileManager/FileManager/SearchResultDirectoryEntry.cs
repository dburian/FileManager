using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using HelperExtensionLibrary;

namespace FileManager
{
	class SearchResultDirectoryEntry : DirectoryEntry
	{
		string searchedDirPath;

		public SearchResultDirectoryEntry(string searchedDirPath)
		{
			this.searchedDirPath = searchedDirPath;
		}

		public override string EntryName { get => ((DirectoryInfo)Info).GetRelativePath(searchedDirPath); }
	}
}
