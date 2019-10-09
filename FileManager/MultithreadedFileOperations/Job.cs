using System.Threading;

namespace MultithreadedFileOperations
{
	/// <summary>
	/// Represents raw operation and it's execution context.
	/// </summary>
	internal abstract class Job : IVisitableJob
	{
		protected CancellationToken ct;

		public abstract JobType Type { get; }

		/// <summary>
		/// Is called on every major progress change of the operation.
		/// </summary>
		public event OnProgressChangeDelegate ProgressChange;

		public abstract void Accept(IJobVisitor visitor);

		/// <summary>
		/// Executes the operation.
		/// </summary>
		/// <exception cref="FileOperationException"/>
		public abstract void Run();

		protected virtual void OnProgressChange(float percentage)
		{
			ProgressChange?.Invoke(percentage);
		}

	}
}
