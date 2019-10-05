using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;

namespace MultithreadedFileOperations
{
	internal class DeleteJob : Job
	{

		public DeleteJob(DeleteJobArguments args, CancellationToken ct)
		{
			Args = args;
			this.ct = ct;
		}
		public DeleteJob(DeleteJobArguments args, CancellationToken ct, OnExceptionRaiseDelegate onExceptionRaise, OnProgressChangeDelegate onProgressChange)
			:this(args, ct)
		{
			ExceptionRaise += onExceptionRaise;
			ProgressChange += onProgressChange;
		}

		public override JobType Type { get => JobType.Delete; }
		public DeleteJobArguments Args { get; private set; }

		public override void Run()
		{
			try
			{
				ct.ThrowIfCancellationRequested();

				if (!Args.Target.Exists) throw new FileNotFoundException("File or directory not found.", Args.Target.FullName);

				if (Args.Recursively) ((DirectoryInfo)Args.Target).Delete(Args.Recursively);
				else Args.Target.Delete();

				OnProgressChange(100f);
			}catch (Exception e) when (e is DirectoryNotFoundException || e is IOException || e is UnauthorizedAccessException)
			{
				var fileE = new FileDeleteException(Args, e);
				OnExceptionRaise(fileE);
				throw fileE;
			}
		}

		public override void Accept(IJobVisitor visitor) => visitor.Visit(this);
	}
}
