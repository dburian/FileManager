using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MultithreadedFileOperations
{
	public interface ISearchView : IDisposable
	{
		SearchSettings Settings { get; }
		Task Task { get; }
	
		event OnSearchDoneDelegate SearchDone;
		event OnFoundBatchFullDelegate FoundBatchFull;
		event OnExceptionRaiseDelegate ExceptionRaise;

		void Stop();
		void Start();
	}
}
