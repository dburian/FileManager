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

		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// SearchResultFileEntry
			// 
			this.Name = "SearchResultFileEntry";
			this.Size = new System.Drawing.Size(380, 18);
			this.ResumeLayout(false);

		}
	}
}
