namespace FileManager
{
	internal struct LeftCommand : ICommand
	{
		public LeftCommand(Panes pane)
		{
			Pane = pane;
		}

		public Panes Pane { get; }
	}
}
