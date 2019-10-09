using System;
using System.IO;
using System.Security;
using System.Threading;

namespace MultithreadedFileSystemOperations
{
	/// <summary>
	/// Cancelable delete operation. Can delete files and (non-empty)directories.
	/// </summary>
	internal class DeleteJob : Job
	{

		public DeleteJob(DeleteJobArguments args, CancellationToken ct)
		{
			Args = args;
			this.ct = ct;
		}
		public DeleteJob(DeleteJobArguments args, OnProgressChangeDelegate onProgressChange, CancellationToken ct)
			: this(args, ct)
		{
			ProgressChange += onProgressChange;
		}

		public override JobType Type => JobType.Delete;
		public DeleteJobArguments Args { get; }

		/// <summary>
		/// Executes the operation.
		/// </summary>
		/// <exception cref="DeleteException"/>
		public override void Run()
		{
			try
			{
				ct.ThrowIfCancellationRequested();

				if (!Args.Target.Exists)
				{
					throw new FileNotFoundException("File or directory not found.", Args.Target.FullName);
				}

				if (Args.Recursively)
				{
					((DirectoryInfo)Args.Target).Delete(Args.Recursively);
				}
				else
				{
					Args.Target.Delete();
				}

				OnProgressChange(100f);
			}
			catch (Exception e) when (e is SecurityException || e is IOException || e is UnauthorizedAccessException)
			{
				throw new DeleteException(Args, e);
			}
		}

		public override void Accept(IJobVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
