#define TEST

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace MultithreadedFileOperationsTests
{
	[TestClass]
	public class FileCopyJobTests
	{
		private readonly IOTestState state = new IOTestState();

		[TestInitialize]
		public void BeforeEachTest()
		{
			state.Init();
		}

		[TestCleanup]
		public void AfterEachTest()
		{
			state.Cleanup();
		}

		[TestMethod]
		public void RunSynchronously()
		{
			var ctSource = new CancellationTokenSource();
			var cpJob = new FileTransferJob(
				new FileTransferArguments(state.ExistingFiles[0], state.NonExistingFiles[0], TransferSettings.None),
				state.OnProgressDebugPrint
,
				ctSource.Token);

			cpJob.Run();

			state.NonExistingFiles[0].Refresh();
			Assert.IsTrue(state.NonExistingFiles[0].Exists);
			Assert.AreEqual(File.ReadAllText(state.ExistingFiles[0].FullName), File.ReadAllText(state.NonExistingFiles[0].FullName));
		}

		[TestMethod]
		public void RunAsynchronously()
		{
			FileTransferJob[] jobs = new FileTransferJob[state.NonExistingFiles.Length];
			var ctSource = new CancellationTokenSource();

			for (int i = 0; i < state.NonExistingFiles.Length; i++)
			{
				jobs[i] = new FileTransferJob(
					new FileTransferArguments(state.ExistingFiles[0], state.NonExistingFiles[i], TransferSettings.None),
					state.OnProgressDebugPrint
,
					ctSource.Token);
			}

			Task[] jobTasks = new Task[state.NonExistingFiles.Length];

			for (int i = 0; i < state.NonExistingFiles.Length; i++)
			{
				jobTasks[i] = Task.Run(jobs[i].Run);
			}

			Task.WaitAll(jobTasks);
			foreach (var dest in state.NonExistingFiles)
			{
				dest.Refresh();
				Assert.IsTrue(dest.Exists);
				Assert.AreEqual(File.ReadAllText(state.ExistingFiles[0].FullName), File.ReadAllText(dest.FullName));
			}
		}

		[TestMethod]
		public void SourceFileDoesNotExist()
		{
			var ctSource = new CancellationTokenSource();
			var cpJob = new FileTransferJob(
				new FileTransferArguments(state.NonExistingFiles[0], state.NonExistingFiles[1], TransferSettings.None),
				state.OnProgressDebugPrint
,
				ctSource.Token);

			try
			{
				cpJob.Run();
			}
			catch (FileTransferException e)
			{
				state.ExceptionThrown = true;
				Assert.IsTrue(e.InnerException is FileNotFoundException);
			}

			Assert.IsTrue(state.ExceptionThrown);
			Assert.IsFalse(state.NonExistingFiles[1].Exists);
		}

		[TestMethod]
		public void DestinationFileExists()
		{
			File.WriteAllText(state.NonExistingFiles[0].FullName, "Oh this suddenly exists..");

			var ctSource = new CancellationTokenSource();
			var cpJob = new FileTransferJob(
				new FileTransferArguments(state.ExistingFiles[0], state.NonExistingFiles[0], TransferSettings.None),
				state.OnProgressDebugPrint
,
				ctSource.Token);

			try
			{
				cpJob.Run();
			}
			catch (FileTransferException e)
			{
				state.ExceptionThrown = true;
				Assert.IsTrue(e.InnerException is IOException);
			}

			Assert.IsTrue(state.ExceptionThrown);
			Assert.IsTrue(state.NonExistingFiles[0].Exists);
			Assert.AreNotEqual(File.ReadAllText(state.ExistingFiles[0].FullName), File.ReadAllText(state.NonExistingFiles[0].FullName));
		}

		[TestMethod]
		public void CanceledBefore()
		{
			bool threwException = false;

			var ctSource = new CancellationTokenSource();
			FileTransferJob job = new FileTransferJob(
				new FileTransferArguments(state.ExistingFiles[0], state.NonExistingFiles[0], TransferSettings.None),
				state.OnProgressDebugPrint
,
				ctSource.Token);

			ctSource.Cancel();

			var t = Task.Run(job.Run, ctSource.Token);
			try
			{
				t.Wait();
			}
			catch (AggregateException ea)
			{
				threwException = true;
				Assert.IsTrue(ea.InnerExceptions.All(e => e is OperationCanceledException));
			}

			Assert.IsTrue(threwException);
			Assert.IsFalse(state.NonExistingFiles[0].Exists);
			Assert.IsTrue(t.IsCanceled);
		}

		[TestMethod]
		public void CanceledBeforeAndDestFileExists()
		{
			File.WriteAllText(state.NonExistingFiles[0].FullName, "Oh this suddenly exists..");
			var ctSource = new CancellationTokenSource();
			FileTransferJob job = new FileTransferJob(
				new FileTransferArguments(state.ExistingFiles[0], state.NonExistingFiles[0], TransferSettings.None),
				state.OnProgressDebugPrint
,
				ctSource.Token);

			ctSource.Cancel();

			var t = Task.Run(job.Run, ctSource.Token);
			try
			{
				t.Wait();
			}
			catch (AggregateException ea)
			{
				state.ExceptionThrown = true;
				Assert.IsTrue(ea.InnerExceptions.All(e => e is OperationCanceledException));
			}

			Assert.IsTrue(state.ExceptionThrown);
			Assert.IsFalse(state.NonExistingFiles[0].Exists);
			Assert.IsTrue(t.IsCanceled);
		}

		[TestMethod]
		public void DestinationWithoutRights()
		{
			CancellationTokenSource cts = new CancellationTokenSource();

			var job = new FileTransferJob(
				new FileTransferArguments(state.ExistingFiles[0], state.NonExistentFileWithoutRights, TransferSettings.None),
				state.OnProgressDebugPrint
,
				cts.Token);

			try
			{
				job.Run();
			}
			catch (FileTransferException e)
			{
				state.ExceptionThrown = true;
				Assert.IsTrue(e.InnerException is UnauthorizedAccessException);
			}

			Assert.IsTrue(state.ExceptionThrown);
			Assert.IsFalse(state.NonExistentFileWithoutRights.Exists);
		}

		[TestMethod]
		public void OpenedHandle()
		{
			using (var fs = new FileStream(state.ExistingFiles[0].FullName, FileMode.Append))
			{
				var ctSource = new CancellationTokenSource();
				var cpJob = new FileTransferJob(
					new FileTransferArguments(state.ExistingFiles[0], state.NonExistingFiles[0], TransferSettings.None),
					state.OnProgressDebugPrint
,
					ctSource.Token);

				try
				{
					cpJob.Run();
				}
				catch (FileTransferException e)
				{
					state.ExceptionThrown = true;
					Assert.IsTrue(e.InnerException is IOException);
				}

				Assert.IsTrue(state.ExceptionThrown);

				state.NonExistingFiles[0].Refresh();
				Assert.IsFalse(state.NonExistingFiles[0].Exists);
			}
		}
	}
}
