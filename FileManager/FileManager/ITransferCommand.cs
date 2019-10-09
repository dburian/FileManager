namespace FileManager
{
	internal interface ITransferCommand : ICommand
	{
		string To { get; }
	}
}
