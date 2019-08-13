namespace FileManager
{
	public sealed class ParentDirectoryEntry : DirectoryEntry
	{
		public override bool Highlighted { get => false; set { } }

		public override void Initialize()
		{
			nameLabel.Text = "..";
			typeLabel.Text = EntryExt;
			sizeLabel.Text = "Dir";
			dateTimeAddedLabel.Text = Format.DateAndTime(EntryCreated);
			dateTimeLastModifiedLabel.Text = Format.DateAndTime(EntryModified);
		}
	}
}
