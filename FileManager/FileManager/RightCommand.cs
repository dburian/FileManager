namespace FileManager
{
	internal struct RightCommand : ICommand
	{
		public RightCommand(Panes pane)
		{
			Pane = pane;
		}

		public Panes Pane { get; }
	}
}
