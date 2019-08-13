using System;
using System.IO;

namespace FileManager
{
	public sealed class FileEntry : FilesViewEntry
	{
		FileInfo _info;

		public override string EntryName { get => _info.GetOnlyName(); }
		public override string EntryExt { get => _info.GetOnlyExtension(); }
		public override long EntrySize { get => _info.Length; }
		public override DateTime EntryCreated { get => _info.CreationTime; }
		public override DateTime EntryModified { get => _info.LastWriteTime; }
		public override FileSystemInfo Info {
			get => _info ;
			set {
				if (!value.Exists || value.GetType() != typeof(FileInfo)) throw new ArgumentException($"FileEntry(): FileInfo {value.FullName} is invalid");

				_info = (FileInfo)value;
				Initialize();
			}
		}

		public void Initialize()
		{
			nameLabel.Text = EntryName;
			typeLabel.Text = EntryExt;
			sizeLabel.Text = Format.Size(EntrySize);
			dateTimeAddedLabel.Text = Format.DateAndTime(EntryCreated);
			dateTimeLastModifiedLabel.Text = Format.DateAndTime(EntryModified);
		}
	}
}
