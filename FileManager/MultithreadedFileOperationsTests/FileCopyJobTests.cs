﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using MultithreadedFileOperations;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;


namespace MultithreadedFileOperationsTests
{
	[TestClass]
	public class FileCopyJobTests
	{
		IOTestState state = new IOTestState();
		
		[TestInitialize]
		public void BeforeEachTest() => state.Init();

		[TestCleanup]
		public void AfterEachTest() => state.Cleanup();


		[TestMethod]
		public void RunSynchronously()
		{
			var ctSource = new CancellationTokenSource();
			var cpJob = new FileCopyJob(
				new FileTransferJobArguments(state.ExistingFiles[0], state.NonExistingFiles[0]),
				ctSource.Token,
				state.OnExceptionDebugPrint,
				state.OnProgressDebugPrint
			);
			
			cpJob.Run();

			state.NonExistingFiles[0].Refresh();
			Assert.IsTrue(state.NonExistingFiles[0].Exists);
			Assert.AreEqual(File.ReadAllText(state.ExistingFiles[0].FullName), File.ReadAllText(state.NonExistingFiles[0].FullName));
		}

		[TestMethod]
		public void RunAsynchronously()
		{
			FileCopyJob[] jobs = new FileCopyJob[state.NonExistingFiles.Length];
			var ctSource = new CancellationTokenSource();

			for (int i = 0; i < state.NonExistingFiles.Length; i++)
			{
				jobs[i] = new FileCopyJob(
					new FileTransferJobArguments(state.ExistingFiles[0], state.NonExistingFiles[i]),
					ctSource.Token,
					state.OnExceptionDebugPrint,
					state.OnProgressDebugPrint
				);
			}

			Task[] jobTasks = new Task[state.NonExistingFiles.Length];

			for (int i = 0; i < state.NonExistingFiles.Length; i++)
				jobTasks[i] = Task.Run(jobs[i].Run);
			


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
			var cpJob = new FileCopyJob(
				new FileTransferJobArguments(state.NonExistingFiles[0], state.NonExistingFiles[1]),
				ctSource.Token,
				 state.OnExceptionDebugPrint,
				 state.OnProgressDebugPrint
			);

			try
			{
				cpJob.Run();
			}
			catch (FileCopyException e)
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
			var cpJob = new FileCopyJob(
				new FileTransferJobArguments(state.ExistingFiles[0], state.NonExistingFiles[0]),
				ctSource.Token,
				state.OnExceptionDebugPrint,
				state.OnProgressDebugPrint
			);

			try
			{
				cpJob.Run();
			}
			catch (FileCopyException e)
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
			FileCopyJob job = new FileCopyJob(
				new FileTransferJobArguments(state.ExistingFiles[0], state.NonExistingFiles[0]),
				ctSource.Token,
				state.OnExceptionDebugPrint,
				state.OnProgressDebugPrint
			);

			ctSource.Cancel();

			var t = Task.Run(job.Run, ctSource.Token);
			try
			{
				t.Wait();
			}catch (AggregateException ea)
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
			FileCopyJob job = new FileCopyJob(
				new FileTransferJobArguments(state.ExistingFiles[0], state.NonExistingFiles[0]),
				ctSource.Token,
				state.OnExceptionDebugPrint,
				state.OnProgressDebugPrint
			);

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

			var job = new FileCopyJob(
				new FileTransferJobArguments(state.ExistingFiles[0], state.NonExistentFileWithoutRights),
				cts.Token,
				state.OnExceptionDebugPrint,
				state.OnProgressDebugPrint
			);

			try
			{
				job.Run();
			}
			catch (FileCopyException e)
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
				var cpJob = new FileCopyJob(
					new FileTransferJobArguments(state.ExistingFiles[0], state.NonExistingFiles[0]),
					ctSource.Token,
					state.OnExceptionDebugPrint,
					state.OnProgressDebugPrint
				);

				try
				{
					cpJob.Run();
				}catch(FileCopyException e)
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