using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultithreadedFileOperations;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MultithreadedFileOperationsTests
{
	[TestClass]
	public class FileMoveJobTests
	{
		private readonly IOTestState state = new IOTestState();

		[TestInitialize]
		public void BeforeTest()
		{
			state.Init();
		}

		[TestCleanup]
		public void AfterTest()
		{
			state.Cleanup();
		}

		[TestMethod]
		public void RunSynchornously()
		{
			string existingFileContent = File.ReadAllText(state.ExistingFiles[0].FullName);

			var cts = new CancellationTokenSource();
			var moveJob = new FileTransferJob(
				new FileTransferArguments(state.ExistingFiles[0], state.NonExistingFiles[0], TransferSettings.DeleteOriginal),
				state.OnProgressDebugPrint
,
				cts.Token);

			moveJob.Run();

			state.NonExistingFiles[0].Refresh();
			state.ExistingFiles[0].Refresh();

			Assert.IsTrue(state.NonExistingFiles[0].Exists);
			Assert.IsFalse(state.ExistingFiles[0].Exists);

			Assert.AreEqual(existingFileContent, File.ReadAllText(state.NonExistingFiles[0].FullName));
		}

		[TestMethod]
		public void RunAsynchornously()
		{
			var cts = new CancellationTokenSource();
			var tasks = new Task[state.NonExistingFiles.Length];

			Assert.AreEqual(state.NonExistingFiles.Length, state.ExistingFiles.Length);
			for (int i = 0; i < state.NonExistingFiles.Length; i++)
			{
				var moveJob = new FileTransferJob(
					new FileTransferArguments(state.ExistingFiles[i], state.NonExistingFiles[i], TransferSettings.DeleteOriginal),

					state.OnProgressDebugPrint
,
					cts.Token);

				tasks[i] = Task.Run(moveJob.Run);
			}

			Task.WaitAll(tasks);

			for (int i = 0; i < state.NonExistingFiles.Length; i++)
			{
				Assert.IsTrue(tasks[i].IsCompleted);

				state.NonExistingFiles[i].Refresh();
				state.ExistingFiles[i].Refresh();
				Assert.IsFalse(state.ExistingFiles[i].Exists);

				Assert.IsTrue(state.NonExistingFiles[i].Exists);
			}
		}

		[TestMethod]
		public void SourceDoesNotExist()
		{
			var cts = new CancellationTokenSource();
			var moveJob = new FileTransferJob(
				new FileTransferArguments(state.NonExistingFiles[0], state.NonExistingFiles[1], TransferSettings.DeleteOriginal),
				state.OnProgressDebugPrint
,
				cts.Token);

			try
			{
				moveJob.Run();
			}
			catch (FileTransferException e)
			{
				state.ExceptionThrown = true;
				Assert.IsTrue(e.InnerException is FileNotFoundException);
			}


			state.NonExistingFiles[1].Refresh();

			Assert.IsFalse(state.NonExistingFiles[1].Exists);
			Assert.IsTrue(state.ExceptionThrown);
		}

		[TestMethod]
		public void DestinationAlreadyExists()
		{
			var cts = new CancellationTokenSource();
			var moveJob = new FileTransferJob(
				new FileTransferArguments(state.ExistingFiles[0], state.ExistingFiles[1], TransferSettings.DeleteOriginal),
				state.OnProgressDebugPrint
,
				cts.Token);

			try
			{
				moveJob.Run();
			}
			catch (FileTransferException e)
			{
				state.ExceptionThrown = true;
				Assert.IsTrue(e.InnerException is IOException);
			}

			state.ExistingFiles[0].Refresh();
			state.ExistingFiles[1].Refresh();

			Assert.IsTrue(state.ExceptionThrown);
			Assert.IsTrue(state.ExistingFiles[0].Exists);
			Assert.IsTrue(state.ExistingFiles[1].Exists);
			Assert.AreNotEqual(File.ReadAllText(state.ExistingFiles[0].FullName), File.ReadAllText(state.ExistingFiles[1].FullName));
		}

		[TestMethod]
		public void SourceWithoutRights()
		{
			var cts = new CancellationTokenSource();
			var moveJob = new FileTransferJob(
				new FileTransferArguments(state.ExistentFileWithoutRights, state.NonExistingFiles[0], TransferSettings.DeleteOriginal),
				state.OnProgressDebugPrint
,
				cts.Token);

			try
			{
				moveJob.Run();
			}
			catch (FileTransferException e)
			{
				state.ExceptionThrown = true;
				Assert.IsTrue(e.InnerException is UnauthorizedAccessException);
			}

			state.ExistentFileWithoutRights.Refresh();
			state.NonExistingFiles[0].Refresh();

			Assert.IsTrue(state.ExceptionThrown);
			Assert.IsTrue(state.ExistentFileWithoutRights.Exists);
			Assert.IsFalse(state.NonExistingFiles[0].Exists);
		}

		[TestMethod]
		public void DestinationWithoutRights()
		{
			var cts = new CancellationTokenSource();
			var moveJob = new FileTransferJob(
				new FileTransferArguments(state.ExistingFiles[0], state.NonExistentFileWithoutRights, TransferSettings.DeleteOriginal),
				state.OnProgressDebugPrint
,
				cts.Token);

			try
			{
				moveJob.Run();
			}
			catch (FileTransferException e)
			{
				state.ExceptionThrown = true;
				Assert.IsTrue(e.InnerException is UnauthorizedAccessException);
			}

			state.ExistingFiles[0].Refresh();
			state.NonExistentFileWithoutRights.Refresh();

			Assert.IsTrue(state.ExceptionThrown);
			Assert.IsTrue(state.ExistingFiles[0].Exists);
		}

		[TestMethod]
		public void CanceledBefore()
		{
			var cts = new CancellationTokenSource();
			var moveJob = new FileTransferJob(
				new FileTransferArguments(state.ExistingFiles[0], state.NonExistingFiles[0], TransferSettings.DeleteOriginal),

				state.OnProgressDebugPrint
,
				cts.Token);

			cts.Cancel();

			var t = Task.Run(moveJob.Run, cts.Token);

			try
			{
				t.Wait();
			}
			catch (AggregateException ae)
			{
				state.ExceptionThrown = true;
				Assert.IsTrue(ae.InnerExceptions.All(e => e is OperationCanceledException));
			}

			Assert.AreEqual(t.Status, TaskStatus.Canceled);
			Assert.IsTrue(state.ExceptionThrown);

			state.ExistingFiles[0].Refresh();
			state.NonExistingFiles[0].Refresh();

			Assert.IsTrue(state.ExistingFiles[0].Exists);
			Assert.IsFalse(state.NonExistingFiles[0].Exists);
		}

		[TestMethod]
		public void CanceledBeforeAndSourceDoesNotExist()
		{
			var cts = new CancellationTokenSource();
			var moveJob = new FileTransferJob(
				new FileTransferArguments(state.NonExistingFiles[0], state.NonExistingFiles[1], TransferSettings.DeleteOriginal),
				state.OnProgressDebugPrint
,
				cts.Token);

			cts.Cancel();

			var t = Task.Run(moveJob.Run, cts.Token);

			try
			{
				t.Wait();
			}
			catch (AggregateException ae)
			{
				state.ExceptionThrown = true;
				Assert.IsTrue(ae.InnerExceptions.All(e => e is OperationCanceledException));
			}

			Assert.AreEqual(t.Status, TaskStatus.Canceled);
			Assert.IsTrue(state.ExceptionThrown);

			state.NonExistingFiles[0].Refresh();
			state.NonExistingFiles[1].Refresh();

			Assert.IsFalse(state.NonExistingFiles[0].Exists);
			Assert.IsFalse(state.NonExistingFiles[1].Exists);
		}

		[TestMethod]
		public void CanceledBeforeAndSourceWithoutRights()
		{
			var cts = new CancellationTokenSource();
			var moveJob = new FileTransferJob(
				new FileTransferArguments(state.ExistentFileWithoutRights, state.NonExistingFiles[0], TransferSettings.DeleteOriginal),
				state.OnProgressDebugPrint
,
				cts.Token);

			cts.Cancel();

			var t = Task.Run(moveJob.Run, cts.Token);

			try
			{
				t.Wait();
			}
			catch (AggregateException ae)
			{
				state.ExceptionThrown = true;
				Assert.IsTrue(ae.InnerExceptions.All(e => e is OperationCanceledException));
			}

			Assert.AreEqual(t.Status, TaskStatus.Canceled);
			Assert.IsTrue(state.ExceptionThrown);

			state.ExistentFileWithoutRights.Refresh();
			state.NonExistingFiles[0].Refresh();

			Assert.IsTrue(state.ExistentFileWithoutRights.Exists);
			Assert.IsFalse(state.NonExistingFiles[0].Exists);
		}

		[TestMethod]
		public void OpenedHandle()
		{
			using (var fs = new FileStream(state.ExistingFiles[0].FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				var ctSource = new CancellationTokenSource();
				var mvJob = new FileTransferJob(
					new FileTransferArguments(state.ExistingFiles[0], state.NonExistingFiles[0], TransferSettings.DeleteOriginal),
					state.OnProgressDebugPrint
,
					ctSource.Token);

				try
				{
					mvJob.Run();
				}
				catch (FileTransferException e)
				{
					state.ExceptionThrown = true;
					Assert.IsTrue(e.InnerException is IOException);
				}

				Assert.IsTrue(state.ExceptionThrown);

				state.NonExistingFiles[0].Refresh();
				state.ExistingFiles[0].Refresh();
				Assert.IsTrue(state.ExistingFiles[0].Exists);
				Assert.IsFalse(state.NonExistingFiles[0].Exists);
			}
		}
	}
}
