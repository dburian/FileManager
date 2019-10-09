namespace FileManager
{
	internal struct CopyCommand : ITransferCommand
	{
		public CopyCommand(string to)
		{
			To = to;
		}

		public string To { get; }
	}
}
