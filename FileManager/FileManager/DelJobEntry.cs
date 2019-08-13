namespace FileManager
{
	public class DelJobEntry : JobEntry
	{
		public DelJobEntry() : base() {}

		public DelArgs Args { get; set; }

		protected override void UpdateBackgroundColor()
		{
			base.UpdateBackgroundColor();

			Args.BackColor = BackColor;
		}
	}
}
