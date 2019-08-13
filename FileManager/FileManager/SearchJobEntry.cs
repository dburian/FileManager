namespace FileManager
{
	public class SearchJobEntry : JobEntry
	{
		public SearchJobEntry() : base() {}

		public SearchArgs Args { get; set; }

		protected override void UpdateBackgroundColor()
		{
			base.UpdateBackgroundColor();

			Args.BackColor = BackColor;
		}
	}
}
