using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace FileManager
{
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
