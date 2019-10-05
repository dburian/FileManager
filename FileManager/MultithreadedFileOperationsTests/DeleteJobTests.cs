using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MultithreadedFileOperations;
using System.Diagnostics;
using System.Linq;

namespace MultithreadedFileOperationsTests
{
	[TestClass]
	public class DeleteJobTests
	{
		IOTestState state = new IOTestState();

		[TestInitialize]
		public void BeforeTest()
		{
			state.Init();
		}

		[TestCleanup]
		public void AfterTest() => state.Cleanup();

		[TestMethod]
		public void RunSynchronously()
		{
			CancellationTokenSource cts = new CancellationTokenSource();
			var delJob = new DeleteJob(new DeleteJobArguments(state.ExistingFiles[0]), cts.Token, state.OnExceptionDebugPrint, state.OnProgressDebugPrint);

			delJob.Run();
			state.ExistingFiles[0].Refresh();

			Assert.IsFalse(state.ExistingFiles[0].Exists);
		}

		[TestMethod]
		public void RunAsynchronously()
		{
			CancellationTokenSource cts = new CancellationTokenSource();

			Task[] jobTasks = new Task[state.ExistingFiles.Length];
			for (int i = 0; i < state.ExistingFiles.Length; i++)
				jobTasks[i] = Task.Run(
					new DeleteJob(
						new DeleteJobArguments(state.ExistingFiles[i]),
						cts.Token,
						state.OnExceptionDebugPrint,
						state.OnProgressDebugPrint
						).Run
					, cts.Token);
			

			Task.WaitAll(jobTasks);

			for (int i = 0; i < state.ExistingFiles.Length; i++)
			{
				state.ExistingFiles[i].Refresh();
				Assert.IsTrue(jobTasks[i].IsCompleted);
				Assert.IsFalse(state.ExistingFiles[i].Exists);
			}
		}

		[TestMethod]
		public void TargetDoesNotExist()
		{
			CancellationTokenSource cts = new CancellationTokenSource();
			var delJob = new DeleteJob(
				new DeleteJobArguments(state.NonExistingFiles[0]),cts.Token, state.OnExceptionDebugPrint, state.OnProgressDebugPrint
				);

			try
			{
				delJob.Run();
			}catch (FileDeleteException e)
			{
				state.ExceptionThrown = true;
				Assert.IsTrue(e.InnerException is FileNotFoundException);
			}

			Assert.IsTrue(state.ExceptionThrown);
		}

		[TestMethod]
		public void CanceledBefore()
		{
			CancellationTokenSource cts = new CancellationTokenSource();
			Task[] jobTasks = new Task[state.ExistingFiles.Length];

			cts.Cancel();

			for (int i = 0; i < state.ExistingFiles.Length; i++)
				jobTasks[i] = Task.Run(
					new DeleteJob(
						new DeleteJobArguments(state.ExistingFiles[i]),
						cts.Token,
						state.OnExceptionDebugPrint,
						state.OnProgressDebugPrint
						).Run,
					cts.Token
					);
			

			try
			{
				Task.WaitAll(jobTasks);
			}catch(AggregateException ae)
			{
				Assert.IsTrue(ae.InnerExceptions.All(e => e is OperationCanceledException));
				state.ExceptionThrown = true;
			}

			Assert.IsTrue(state.ExceptionThrown);
			for (int i = 0; i < state.ExistingFiles.Length; i++)
			{
				state.ExistingFiles[i].Refresh();

				Assert.IsTrue(jobTasks[i].IsCanceled);
				Assert.IsTrue(state.ExistingFiles[i].Exists);
			}
		}

		[TestMethod]
		public void CanceledBeforeAndTargetDoesNotExist()
		{
			CancellationTokenSource cts = new CancellationTokenSource();
			var delJob = new DeleteJob(new DeleteJobArguments(state.NonExistingFiles[0]), cts.Token, state.OnExceptionDebugPrint, state.OnProgressDebugPrint);

			cts.Cancel();

			var t = Task.Run(delJob.Run, cts.Token);
			try
			{
				t.Wait();
			}catch (AggregateException ae)
			{
				Assert.IsTrue(ae.InnerExceptions.All(e => e is OperationCanceledException));
				state.ExceptionThrown = true;
			}

			Assert.IsTrue(state.ExceptionThrown);
		}

		[TestMethod]
		public void TargetWithoutRights()
		{
			CancellationTokenSource cts = new CancellationTokenSource();
			var delJob = new DeleteJob(new DeleteJobArguments(state.ExistentFileWithoutRights), cts.Token, state.OnExceptionDebugPrint, state.OnProgressDebugPrint);

			try
			{
				delJob.Run();
			}catch (FileDeleteException e)
			{
				state.ExceptionThrown = true;
				Assert.IsTrue(e.InnerException is UnauthorizedAccessException);
			}

			state.ExistentFileWithoutRights.Refresh();
			Assert.IsTrue(state.ExistentFileWithoutRights.Exists);
			Assert.IsTrue(state.ExceptionThrown);
		}

		[TestMethod]
		public void DeleteDirectory()
		{
			var cts = new CancellationTokenSource();
			var job = new DeleteJob(new DeleteJobArguments(state.ExistingEmptyDirectories[0]), cts.Token);

			job.Run();

			state.ExistingEmptyDirectories[0].Refresh();
			Assert.IsFalse(state.ExistingEmptyDirectories[0].Exists);
		}

		[TestMethod]
		public void DeleteNonEmptyDirectory()
		{
			var cts = new CancellationTokenSource();
			var job = new DeleteJob(new DeleteJobArguments(state.ExistingNonEmptyDirectories[0], true), cts.Token);

			job.Run();

			state.ExistingNonEmptyDirectories[0].Refresh();
			Assert.IsFalse(state.ExistingNonEmptyDirectories[0].Exists);
		}

		[TestMethod]
		public void OpenedHandle()
		{
			using(var fs = new FileStream(state.ExistingFiles[0].FullName, FileMode.Open))
			{
				CancellationTokenSource cts = new CancellationTokenSource();
				var delJob = new DeleteJob(new DeleteJobArguments(state.ExistingFiles[0]), cts.Token, state.OnExceptionDebugPrint, state.OnProgressDebugPrint);

				try
				{
					delJob.Run();
				}catch(FileDeleteException e)
				{
					state.ExceptionThrown = true;
					Assert.IsTrue(e.InnerException is IOException);
				}

				Assert.IsTrue(state.ExceptionThrown);

				state.ExistingFiles[0].Refresh();
				Assert.IsTrue(state.ExistingFiles[0].Exists);
			}
		}
	}
}
