using System;

namespace FileManager
{
	/// <summary>
	/// Command which specifies sorting of the file system node entries.
	/// </summary>
	internal struct SortCommand : ICommand
	{
		public SortCommand(Comparison<FileSystemNodeEntry> comparer)
		{
			this.Comparer = comparer;
		}

		public Comparison<FileSystemNodeEntry> Comparer { get; }
	}
}
