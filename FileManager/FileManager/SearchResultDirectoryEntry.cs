﻿using HelperExtensionLibrary;
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

		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// SearchResultDirectoryEntry
			// 
			this.Name = "SearchResultDirectoryEntry";
			this.Size = new System.Drawing.Size(380, 18);
			this.ResumeLayout(false);

		}
	}
}
