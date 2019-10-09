using System;
using System.Threading.Tasks;

namespace MultithreadedFileSystemOperations
{
	/// <summary>
	/// Owner handle of a search operation.
	/// </summary>
	public interface ISearchHandle : IDisposable
	{
		SearchSettings Settings { get; }
		Task ProducerTask { get; }

		event OnSearchDoneDelegate SearchDone;
		event OnFoundBatchFullDelegate FoundBatchFull;
		event OnExceptionRaiseDelegate ExceptionRaise;

		void Cancel();
		void Start();
	}
}
