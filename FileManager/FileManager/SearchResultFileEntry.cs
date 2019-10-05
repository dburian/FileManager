using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using HelperExtensionLibrary;

namespace FileManager
{
	class SearchResultFileEntry : FileEntry
	{
		string searchedDirPath;
		public SearchResultFileEntry(string searchedDirPath)
		{
			this.searchedDirPath = searchedDirPath;
		}

		public override string EntryName { get => ((FileInfo)Info).GetRelativePath(searchedDirPath); }
	}
}
