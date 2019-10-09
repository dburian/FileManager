using HelperExtensionLibrary;
using System;
using System.IO;

namespace FileManager
{
	/// <summary>
	/// Displayable entry representing a directory in file system.
	/// </summary>
	public class DirectoryEntry : FileSystemNodeEntry
	{
		protected internal DirectoryInfo _info;

		public override string EntryName => _info.GetFormattedName();
		public override string EntryExt => "";
		public override long EntrySize => 0;
		public override DateTime EntryCreated => _info.CreationTime;
		public override DateTime EntryModified => _info.LastWriteTime;
		public override FileSystemInfo Info
		{
			get => _info;
			set
			{
				if (!value.Exists || value.GetType() != typeof(DirectoryInfo))
				{
					throw new ArgumentException($"DirectoryEntry.Info: DirectoryInfo {value.FullName} is invalid");
				}

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
