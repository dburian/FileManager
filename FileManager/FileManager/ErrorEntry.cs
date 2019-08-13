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

namespace FileManager
{
	public partial class ErrorEntry : FilesViewEntry
	{
		public override string EntryName { get => ""; }
		public override string EntryExt { get => ""; }
		public override long EntrySize { get => 0; }
		public override DateTime EntryCreated { get => new DateTime(); }
		public override DateTime EntryModified { get => new DateTime(); }
		public override FileSystemInfo Info { get => throw new InvalidOperationException(); set {} }

		public ErrorEntry(string exDetail, string exType)
		{
			InitializeComponent();

			errorDetailLabel.Text  = exDetail;
			errorTypeLabel.Text = exType;

			Controls.Remove(fileEntryTablePanel);
		}
	}
}
