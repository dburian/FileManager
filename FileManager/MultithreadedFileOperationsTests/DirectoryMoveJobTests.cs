﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultithreadedFileOperations;
using System.Diagnostics;
using System.IO;
using System.Security;

namespace MultithreadedFileOperationsTests
{
	[TestClass]
	public class DirectoryMoveJobTests
	{
		IOTestState state = new IOTestState();

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
			var job = new DirectoryMoveJob(
				new DirectoryTransferJobArguments(state.ExistingEmptyDirectories[0], state.NonExistingDirectories[0]),
				cts.Token,
				state.OnExceptionDebugPrint,
				state.OnProgressDebugPrint
			);

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
			var job = new DirectoryMoveJob(
				new DirectoryTransferJobArguments(state.ExistingNonEmptyDirectories[0], state.NonExistingDirectories[0]),
				cts.Token,
				state.OnExceptionDebugPrint,
				state.OnProgressDebugPrint
			);

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
				var job = new DirectoryMoveJob(
				new DirectoryTransferJobArguments(state.ExistingNonEmptyDirectories[i], state.NonExistingDirectories[i]),
				cts.Token,
				state.OnExceptionDebugPrint,
				state.OnProgressDebugPrint
			);

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
			var job = new DirectoryMoveJob(
				new DirectoryTransferJobArguments(state.ExistingNonEmptyDirectories[0], state.ExistingEmptyDirectories[0]),
				cts.Token,
				state.OnExceptionDebugPrint,
				state.OnProgressDebugPrint
			);

			try
			{
				job.Run();
			}catch(DirectoryMoveException e)
			{
				state.ExceptionThrown = true;
				Assert.IsTrue(e.InnerException is DirectoryCopyException);
				Assert.IsTrue(e.InnerException.InnerException is IOException);
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
			var job = new DirectoryMoveJob(
				new DirectoryTransferJobArguments(state.NonExistingDirectories[0], state.NonExistingDirectories[1]),
				cts.Token,
				state.OnExceptionDebugPrint,
				state.OnProgressDebugPrint
			);

			try
			{
				job.Run();
			}
			catch (DirectoryMoveException e)
			{
				state.ExceptionThrown = true;
				Assert.IsTrue(e.InnerException is DirectoryCopyException);
				Assert.IsTrue(e.InnerException.InnerException is IOException);
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
			var job = new DirectoryMoveJob(
				new DirectoryTransferJobArguments(state.ExistingNonEmptyDirectories[0], state.NonExistingDirectories[0]),
				cts.Token,
				state.OnExceptionDebugPrint,
				state.OnProgressDebugPrint
			);
			
			cts.Cancel();
			
			try
			{
				job.Run();
			}catch(OperationCanceledException)
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
			var job = new DirectoryMoveJob(
				new DirectoryTransferJobArguments(state.ExistingNonEmptyDirectories[0], state.ExistingNonEmptyDirectories[1]),
				cts.Token,
				state.OnExceptionDebugPrint,
				state.OnProgressDebugPrint
			);

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
			var job = new DirectoryMoveJob(
				new DirectoryTransferJobArguments(state.ExistingNonEmptyDirectories[0], state.NonExistingDirecoryWithoutWriteRights),
				cts.Token,
				state.OnExceptionDebugPrint,
				state.OnProgressDebugPrint
			);

			try
			{
				job.Run();
			}
			catch (DirectoryMoveException e)
			{
				state.ExceptionThrown = true;
				Assert.IsTrue(e.InnerException is DirectoryCopyException);
				Assert.IsTrue(e.InnerException.InnerException is UnauthorizedAccessException);
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
			var job = new DirectoryMoveJob(
				new DirectoryTransferJobArguments(state.ExistingDirecoryWithoutReadRights, state.NonExistingDirectories[0]),
				cts.Token,
				state.OnExceptionDebugPrint,
				state.OnProgressDebugPrint
			);

			try
			{
				job.Run();
			}
			catch (DirectoryMoveException e)
			{
				state.ExceptionThrown = true;
				Assert.IsTrue(e.InnerException is DirectoryCopyException);
				Assert.IsTrue(e.InnerException.InnerException is UnauthorizedAccessException);
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
				var job = new DirectoryMoveJob(
					new DirectoryTransferJobArguments(state.ExistingNonEmptyDirectories[0], state.NonExistingDirectories[0]),
					cts.Token,
					state.OnExceptionDebugPrint,
					state.OnProgressDebugPrint
				);

				try
				{
					job.Run();
				}
				catch (DirectoryMoveException e)
				{
					state.ExceptionThrown = true;
					Assert.IsTrue(e.InnerException is FileDeleteException);
					Assert.IsTrue(e.InnerException.InnerException is IOException);
				}

				Assert.IsTrue(state.ExceptionThrown);

				state.ExistingNonEmptyDirectories[0].Refresh();
				state.NonExistingDirectories[0].Refresh();
				Assert.IsTrue(state.NonExistingDirectories[0].Exists);
				Assert.IsTrue(state.ExistingNonEmptyDirectories[0].Exists);
			}
		}
	}
}