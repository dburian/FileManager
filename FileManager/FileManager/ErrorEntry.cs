using System;
using System.IO;

namespace FileManager
{
	public partial class ErrorEntry : FileSystemNodeEntry
	{
		public override string EntryName => "";
		public override string EntryExt => "";
		public override long EntrySize => 0;
		public override DateTime EntryCreated => new DateTime();
		public override DateTime EntryModified => new DateTime();
		public override FileSystemInfo Info { get => null; set { } }

		public ErrorEntry(string exDetail, string exType)
		{
			InitializeComponent();

			errorDetailLabel.Text = $"{exDetail} - {exType}";

			// Remove inhereted tablePanel
			Controls.Remove(fileEntryTablePanel);
		}
	}
}
