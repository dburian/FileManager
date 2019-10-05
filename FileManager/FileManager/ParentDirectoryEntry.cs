namespace FileManager
{
	public sealed class ParentDirectoryEntry : DirectoryEntry
	{
		public override void Initialize()
		{
			nameLabel.Text = "..";
			typeLabel.Text = EntryExt;
			sizeLabel.Text = "Dir";
			dateTimeAddedLabel.Text = Format.DateAndTime(EntryCreated);
			dateTimeLastModifiedLabel.Text = Format.DateAndTime(EntryModified);
		}

		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// ParentDirectoryEntry
			// 
			this.Name = "ParentDirectoryEntry";
			this.Size = new System.Drawing.Size(732, 18);
			this.ResumeLayout(false);

		}
	}
}
