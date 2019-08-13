using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FileManager
{
	public interface IFilesPane : IPane
	{
		DirectoryInfo CurrentDir { get; set; }
		List<FilesViewEntry> Entries { get; set; }
		long FreeSpaceInDir { set; }
		int HighlightedEntriesCount { get; set; }
		long HighlightedEntriesSize { get; set; }
		int FileEntriesCount { set; }
		int DirectoryEntriesCount { set; }
	}
}
