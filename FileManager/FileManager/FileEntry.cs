using HelperExtensionLibrary;
using System;
using System.IO;

namespace FileManager
{
	/// <summary>
	/// Displayable entry representing a file in file system.
	/// </summary>
	public class FileEntry : FileSystemNodeEntry
	{
		private FileInfo _info;

		public override string EntryName => _info.GetOnlyName();
		public override string EntryExt => _info.GetOnlyExtension();
		public override long EntrySize => _info.Length;
		public override DateTime EntryCreated => _info.CreationTime;
		public override DateTime EntryModified => _info.LastWriteTime;
		public override FileSystemInfo Info
		{
			get => _info;
			set
			{
				if (!value.Exists || value.GetType() != typeof(FileInfo))
				{
					throw new ArgumentException($"FileEntry(): FileInfo {value.FullName} is invalid");
				}

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
