namespace FileManager
{
	public class MoveJobEntry : JobEntry
	{
		public MoveJobEntry() : base() {}

		public CopyMoveArgs Args { get; set; }

		protected override void UpdateBackgroundColor()
		{
			base.UpdateBackgroundColor();

			Args.BackColor = BackColor;
		}
	}
}
