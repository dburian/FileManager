namespace FileManager
{
	internal struct MoveCommand : ITransferCommand
	{
		public MoveCommand(string to)
		{
			To = to;
		}

		public string To { get; }
	}
}
