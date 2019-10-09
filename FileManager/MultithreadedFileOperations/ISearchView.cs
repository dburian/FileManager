using System;
using System.Threading.Tasks;

namespace MultithreadedFileOperations
{
	/// <summary>
	/// View (read-only reference) of a search operation.
	/// </summary>
	public interface ISearchView : IDisposable
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
