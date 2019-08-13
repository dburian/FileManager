namespace FileManager
{
	public class CopyJobEntry : JobEntry
	{
		public CopyJobEntry() : base() {}

		public CopyMoveArgs Args { get; set; }

		protected override void UpdateBackgroundColor()
		{
			base.UpdateBackgroundColor();

			Args.BackColor = BackColor;
		}
	}
}
