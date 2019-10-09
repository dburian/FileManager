using System;

namespace MultithreadedFileOperations
{
	/// <summary>
	/// Represents different settings of a trasfer (i.e. delete original after transfer, overwrite existing,...)
	/// </summary>
	[Flags]
	public enum TransferSettings
	{
		None = 0x0000_0000,
		DeleteOriginal = 0x0000_0001,
	}
}
