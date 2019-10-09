using System;
using System.IO;

namespace FileManager
{
	/// <summary>
	/// Base class to all entries which represent a file system node (i.e. file or directory).
	/// </summary>
	public abstract partial class FileSystemNodeEntry : AbstractEntry
	{

		public FileSystemNodeEntry()
		{
			InitializeComponent();
		}

		public abstract string EntryName { get; }
		public abstract string EntryExt { get; }
		public abstract long EntrySize { get; }
		public abstract DateTime EntryCreated { get; }
		public abstract DateTime EntryModified { get; }
		public abstract FileSystemInfo Info { get; set; }

		protected override void UpdateBackgroundColor()
		{
			fileEntryTablePanel.BackColor = BackColor;
			nameLabel.BackColor = BackColor;
			typeLabel.BackColor = BackColor;
			sizeLabel.BackColor = BackColor;
			dateTimeLastModifiedLabel.BackColor = BackColor;
			dateTimeAddedLabel.BackColor = BackColor;
		}
	}
}
