#define TEST

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace MultithreadedFileOperationsTests
{
	[TestClass]
	public class DirectoryMoveJobTests
	{
		private readonly IOTestState state = new IOTestState();

		[TestInitialize]
		public void BeforeTest()
		{
			try
			{
				state.Init();
			}
			catch (Exception e)
			{
				Debug.WriteLine("Test Init Exception...");
				Debug.WriteLine(e);
			}
		}

		[TestCleanup]
		public void AfterTest()
		{
			state.Cleanup();
		}

		[TestMethod]
		public void RunSynchronously()
		{
			var cts = new CancellationTokenSource();
			var job = new DirectoryTransferJob(
				new DirectoryTransferArguments(state.ExistingEmptyDirectories[0], state.NonExistingDirectories[0], TransferSettings.DeleteOriginal),
				state.OnProgressDebugPrint
,
				cts.Token);

			job.Run();

			state.NonExistingDirectories[0].Refresh();
			state.ExistingEmptyDirectories[0].Refresh();
			Assert.IsTrue(state.NonExistingDirectories[0].Exists);
			Assert.IsFalse(state.ExistingEmptyDirectories[0].Exists);
		}
		[TestMethod]
		public void RunSynchronouslyNonEmpty()
		{
			var cts = new CancellationTokenSource();
			var job = new DirectoryTransferJob(
				new DirectoryTransferArguments(state.ExistingNonEmptyDirectories[0], state.NonExistingDirectories[0], TransferSettings.DeleteOriginal),
				state.OnProgressDebugPrint
,
				cts.Token);

			job.Run();

			state.NonExistingDirectories[0].Refresh();
			state.ExistingNonEmptyDirectories[0].Refresh();
			Assert.IsTrue(state.NonExistingDirectories[0].Exists);
			Assert.IsFalse(state.ExistingNonEmptyDirectories[0].Exists);

			Assert.IsTrue(state.CheckDirectoryStructure(state.NonExistingDirectories[0]));
		}
		[TestMethod]
		public void RunAsynchronously()
		{
			var cts = new CancellationTokenSource();
			var tasks = new Task[state.ExistingNonEmptyDirectories.Length];
			for (int i = 0; i < state.ExistingNonEmptyDirectories.Length; i++)
			{
				var job = new DirectoryTransferJob(
				new DirectoryTransferArguments(state.ExistingNonEmptyDirectories[i], state.NonExistingDirectories[i], TransferSettings.DeleteOriginal),
					state.OnProgressDebugPrint
,
					cts.Token);

				tasks[i] = Task.Run(job.Run, cts.Token);
			}


			Task.WaitAll(tasks);

			for (int i = 0; i < state.ExistingNonEmptyDirectories.Length; i++)
			{
				state.NonExistingDirectories[i].Refresh();
				state.ExistingNonEmptyDirectories[i].Refresh();
				Assert.IsTrue(state.NonExistingDirectories[i].Exists);
				Assert.IsFalse(state.ExistingNonEmptyDirectories[i].Exists);

				Assert.IsTrue(state.CheckDirectoryStructure(state.NonExistingDirectories[i]));
			}
		}
		[TestMethod]
		public void DestinationDoesExist()
		{
			var cts = new CancellationTokenSource();
			var job = new DirectoryTransferJob(
				new DirectoryTransferArguments(state.ExistingNonEmptyDirectories[0], state.ExistingEmptyDirectories[0], TransferSettings.DeleteOriginal),
				state.OnProgressDebugPrint
,
				cts.Token);

			try
			{
				job.Run();
			}
			catch (DirectoryTransferException e)
			{
				state.ExceptionThrown = true;
				Assert.IsTrue(e.InnerException is IOException);
			}

			Assert.IsTrue(state.ExceptionThrown);

			state.ExistingNonEmptyDirectories[0].Refresh();
			state.ExistingEmptyDirectories[0].Refresh();
			Assert.IsTrue(state.ExistingNonEmptyDirectories[0].Exists);
			Assert.IsTrue(state.ExistingEmptyDirectories[0].Exists);

			Assert.IsFalse(state.CheckDirectoryStructure(state.ExistingEmptyDirectories[0]));
		}
		[TestMethod]
		public void SourceDoesNotExist()
		{
			var cts = new CancellationTokenSource();
			var job = new DirectoryTransferJob(
				new DirectoryTransferArguments(state.NonExistingDirectories[0], state.NonExistingDirectories[1], TransferSettings.DeleteOriginal),
				state.OnProgressDebugPrint
,
				cts.Token);

			try
			{
				job.Run();
			}
			catch (DirectoryTransferException e)
			{
				state.ExceptionThrown = true;
				Assert.IsTrue(e.InnerException is IOException);
			}

			Assert.IsTrue(state.ExceptionThrown);

			state.NonExistingDirectories[0].Refresh();
			state.NonExistingDirectories[1].Refresh();
			Assert.IsFalse(state.NonExistingDirectories[0].Exists);
			Assert.IsFalse(state.NonExistingDirectories[1].Exists);
		}
		[TestMethod]
		public void CanceledBefore()
		{
			var cts = new CancellationTokenSource();
			var job = new DirectoryTransferJob(
				new DirectoryTransferArguments(state.ExistingNonEmptyDirectories[0], state.NonExistingDirectories[0], TransferSettings.DeleteOriginal),
				state.OnProgressDebugPrint
,
				cts.Token);

			cts.Cancel();

			try
			{
				job.Run();
			}
			catch (OperationCanceledException)
			{
				state.ExceptionThrown = true;
			}

			Assert.IsTrue(state.ExceptionThrown);

			state.NonExistingDirectories[0].Refresh();
			state.ExistingNonEmptyDirectories[0].Refresh();
			Assert.IsFalse(state.NonExistingDirectories[0].Exists);
			Assert.IsTrue(state.ExistingNonEmptyDirectories[0].Exists);

			Assert.IsTrue(state.CheckDirectoryStructure(state.ExistingNonEmptyDirectories[0]));
		}
		[TestMethod]
		public void CanceledBeforeAndDestinationDoesExists()
		{
			var cts = new CancellationTokenSource();
			var job = new DirectoryTransferJob(
				new DirectoryTransferArguments(state.ExistingNonEmptyDirectories[0], state.ExistingNonEmptyDirectories[1], TransferSettings.DeleteOriginal),
				state.OnProgressDebugPrint
,
				cts.Token);

			cts.Cancel();

			try
			{
				job.Run();
			}
			catch (OperationCanceledException)
			{
				state.ExceptionThrown = true;
			}

			Assert.IsTrue(state.ExceptionThrown);

			state.ExistingNonEmptyDirectories[0].Refresh();
			state.ExistingNonEmptyDirectories[1].Refresh();
			Assert.IsTrue(state.ExistingNonEmptyDirectories[0].Exists);
			Assert.IsTrue(state.ExistingNonEmptyDirectories[1].Exists);

			Assert.IsTrue(state.CheckDirectoryStructure(state.ExistingNonEmptyDirectories[0]));
		}
		[TestMethod]
		public void DestinationWithoutRights()
		{
			var cts = new CancellationTokenSource();
			var job = new DirectoryTransferJob(
				new DirectoryTransferArguments(state.ExistingNonEmptyDirectories[0], state.NonExistingDirecoryWithoutWriteRights, TransferSettings.DeleteOriginal),
				state.OnProgressDebugPrint
,
				cts.Token);

			try
			{
				job.Run();
			}
			catch (DirectoryTransferException e)
			{
				state.ExceptionThrown = true;
				Assert.IsTrue(e.InnerException is UnauthorizedAccessException);
			}

			Assert.IsTrue(state.ExceptionThrown);

			state.ExistingNonEmptyDirectories[0].Refresh();
			state.NonExistingDirecoryWithoutWriteRights.Refresh();
			Assert.IsTrue(state.ExistingNonEmptyDirectories[0].Exists);
			Assert.IsFalse(state.NonExistingDirecoryWithoutWriteRights.Exists);
		}
		[TestMethod]
		public void SourceWithoutRights()
		{
			var cts = new CancellationTokenSource();
			var job = new DirectoryTransferJob(
				new DirectoryTransferArguments(state.ExistingDirecoryWithoutReadRights, state.NonExistingDirectories[0], TransferSettings.DeleteOriginal),
				state.OnProgressDebugPrint
,
				cts.Token);

			try
			{
				job.Run();
			}
			catch (DirectoryTransferException e)
			{
				state.ExceptionThrown = true;
				Assert.IsTrue(e.InnerException is UnauthorizedAccessException);
			}

			Assert.IsTrue(state.ExceptionThrown);

			state.ExistingDirecoryWithoutReadRights.Refresh();
			state.NonExistingDirectories[0].Refresh();
			Assert.IsFalse(state.NonExistingDirectories[0].Exists);
			Assert.IsTrue(state.ExistingDirecoryWithoutReadRights.Exists);
		}
		[TestMethod]
		public void OpenedHandle()
		{
			using (var fs = new FileStream(Path.Combine(Path.GetTempPath(), @"existingNonEmptyDirectory1\existingEmptyDirectory1\existingFile1.txt"), FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				var cts = new CancellationTokenSource();
				var job = new DirectoryTransferJob(
					new DirectoryTransferArguments(state.ExistingNonEmptyDirectories[0], state.NonExistingDirectories[0], TransferSettings.DeleteOriginal),
					state.OnProgressDebugPrint
,
					cts.Token);

				try
				{
					job.Run();
				}
				catch (DirectoryTransferException e)
				{
					state.ExceptionThrown = true;
					//Exceptions reflect file system hierarchy
					Assert.IsTrue(e.InnerException is DirectoryTransferException);
					Assert.IsTrue(e.InnerException.InnerException is FileTransferException);
					Assert.IsTrue(e.InnerException.InnerException.InnerException is IOException);
				}

				Assert.IsTrue(state.ExceptionThrown);

				state.ExistingNonEmptyDirectories[0].Refresh();
				state.NonExistingDirectories[0].Refresh();
				Assert.IsFalse(state.NonExistingDirectories[0].Exists);
				Assert.IsTrue(state.ExistingNonEmptyDirectories[0].Exists);
			}
		}
	}
}
