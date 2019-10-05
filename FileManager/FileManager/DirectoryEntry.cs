using System;
using System.IO;
using HelperExtensionLibrary;

namespace FileManager
{
	public class DirectoryEntry : FileSystemNodeEntry
	{
		protected internal DirectoryInfo _info;

		public override string EntryName { get => _info.GetFormattedName(); }
		public override string EntryExt { get => ""; }
		public override long EntrySize { get => 0; }
		public override DateTime EntryCreated { get => _info.CreationTime; }
		public override DateTime EntryModified { get => _info.LastWriteTime; }
		public override FileSystemInfo Info
		{
			get => _info;
			set
			{
				if (!value.Exists || value.GetType() != typeof(DirectoryInfo))
					throw new ArgumentException($"DirectoryEntry.Info: DirectoryInfo {value.FullName} is invalid");

				_info = (DirectoryInfo)value;
				Initialize();
			}
		}

		public virtual void Initialize()
		{
			nameLabel.Text = EntryName;
			typeLabel.Text = EntryExt;
			sizeLabel.Text = "Dir";
			dateTimeAddedLabel.Text = Format.DateAndTime(EntryCreated);
			dateTimeLastModifiedLabel.Text = Format.DateAndTime(EntryModified);
		}
	}
}
