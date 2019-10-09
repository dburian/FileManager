using System;

namespace MultithreadedFileOperations
{
	/// <summary>
	/// Representation of an enqueued or running job
	/// </summary>
	internal interface IJobHandle : IDisposable
	{
		int Id { get; }
		JobStatus LastStatus { get; set; }
		bool IsDisposed { get; }

		event OnJobChangeDelegate JobChange;

		/// <summary>
		/// Executes the underlying job.
		/// </summary>
		void Run();
		void Cancel();
		IJobView GetView();
	}
}
