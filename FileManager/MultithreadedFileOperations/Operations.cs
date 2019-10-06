using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MultithreadedFileOperations
{
	public static class Operations
	{
		//#region Copy
		//public static void CopyFiles(IEnumerable<FileInfo> sources, DirectoryInfo destinationDirectory)
		//{
		//	IEnumerable<(FileInfo, FileInfo)> GenerateCopies()
		//	{
		//		foreach (var source in sources)
		//			yield return (source, new FileInfo(Path.Combine(destinationDirectory.FullName, source.Name)));

		//	}

		//	Task.Run(() => CopyFiles(GenerateCopies()));
		//}
		//public static void CopyFiles(IEnumerable<FileInfo> sources, DirectoryInfo destinationDirectory, string namePrefix)
		//{
		//	IEnumerable<(FileInfo, FileInfo)> GenerateCopies()
		//	{
		//		int counter = 0;
		//		foreach (var source in sources)
		//		{
		//			yield return (source, new FileInfo(Path.Combine(destinationDirectory.FullName, namePrefix + counter)));

		//			counter++;
		//		}
		//	}

		//	Task.Run(() => CopyFiles(GenerateCopies()));
		//}
		//public static void CopyFiles(IEnumerable<(FileInfo source, FileInfo destination)> copies)
		//{
		//	var t = new Task(() => 
		//	{
		//		foreach (var copy in copies)
		//		{
		//			var cts = new CancellationTokenSource();
		//			var job = new FileCopyJob(new FileTransferArguments(copy.source, copy.destination), cts.Token);

		//			JobsPool.StartNew(job, cts);
		//		}
		//	});

		//	if (TaskScheduler.Current == TaskScheduler.Default)
		//		t.RunSynchronously();
		//	else
		//		t.Start(TaskScheduler.Default);
		//}


		//public static void CopyDirectories(IEnumerable<DirectoryInfo> sources, DirectoryInfo destinationDirectory)
		//{
		//	IEnumerable<(DirectoryInfo, DirectoryInfo)> GenerateCopies()
		//	{
		//		foreach (var source in sources)
		//			yield return (source, new DirectoryInfo(Path.Combine(destinationDirectory.FullName, source.Name)));
		//	}

		//	Task.Run(() => CopyDirectories(GenerateCopies()));
		//}
		//public static void CopyDirectories(IEnumerable<DirectoryInfo> sources, DirectoryInfo destinationDirectory, string namePrefix)
		//{
		//	string name;
		//	string extension;
		//	NameExtensionSplit(namePrefix, out name, out extension);

		//	IEnumerable<(DirectoryInfo, DirectoryInfo)> GenerateCopies()
		//	{
		//		int counter = 0;
		//		foreach (var source in sources)
		//		{
		//			yield return (source, new DirectoryInfo(Path.Combine(destinationDirectory.FullName, name + counter + extension)));

		//			counter++;
		//		}
		//	}

		//	Task.Run(() => CopyDirectories(GenerateCopies()));
		//}
		//public static void CopyDirectories(IEnumerable<(DirectoryInfo source, DirectoryInfo destination)> copies)
		//{
		//	var t = new Task(() =>
		//	{
		//		foreach(var copy in copies)
		//		{
		//			var cts = new CancellationTokenSource();
		//			var job = new DirectoryCopyJob(new DirectoryTransferArguments(copy.source, copy.destination), cts.Token);

		//			JobsPool.StartNew(job, cts);
		//		}
		//	});

		//	if (TaskScheduler.Current == TaskScheduler.Default)
		//		t.RunSynchronously();
		//	else
		//		t.Start(TaskScheduler.Default);
		//}
		//#endregion

		#region Transfer
		public static void TransferFiles(IEnumerable<FileInfo> sources, DirectoryInfo destinationDirectory, TransferSettings settings)
		{
			IEnumerable<(FileInfo, FileInfo)> GenerateMoves()
			{
				foreach (var source in sources)
					yield return (source, new FileInfo(Path.Combine(destinationDirectory.FullName, source.Name)));

			}

			Task.Run(() => TransferFiles(GenerateMoves(), settings));
		}
		public static void TransferFiles(IEnumerable<FileInfo> sources, DirectoryInfo destinationDirectory, string namePrefix, TransferSettings transferSettings)
		{
			string name;
			string extension;
			NameExtensionSplit(namePrefix, out name, out extension);

			IEnumerable<(FileInfo, FileInfo)> GenerateMoves()
			{
				int counter = 0;
				foreach (var source in sources)
				{
					yield return (source, new FileInfo(Path.Combine(destinationDirectory.FullName, name + counter + extension)));

					counter++;
				}
			}

			Task.Run(() => TransferFiles(GenerateMoves(), transferSettings));
		}
		public static void TransferFiles(IEnumerable<(FileInfo source, FileInfo destination)> moves, TransferSettings transferSettings)
		{
			var t = new Task(() =>
			{
				foreach (var move in moves)
				{
					var cts = new CancellationTokenSource();
					var job = new FileTransferJob(new FileTransferArguments(move.source, move.destination, transferSettings), cts.Token);

					JobsPool.StartNew(job, cts);
				}
			});

			if (TaskScheduler.Current == TaskScheduler.Default)
				t.RunSynchronously();
			else
				t.Start(TaskScheduler.Default);
		}


		public static void TransferDirectories(IEnumerable<DirectoryInfo> sources, DirectoryInfo destinationDirectory, TransferSettings settings)
		{
			IEnumerable<(DirectoryInfo, DirectoryInfo)> GenerateMoves()
			{
				foreach (var source in sources)
					yield return (source, new DirectoryInfo(Path.Combine(destinationDirectory.FullName, source.Name)));
			}

			Task.Run(() => TransferDirectories(GenerateMoves(), settings));
		}
		public static void TransferDirectories(IEnumerable<DirectoryInfo> sources, DirectoryInfo destinationDirectory, string namePrefix, TransferSettings settings)
		{
			IEnumerable<(DirectoryInfo, DirectoryInfo)> GenerateMoves()
			{
				int counter = 0;
				foreach (var source in sources)
				{
					yield return (source, new DirectoryInfo(Path.Combine(destinationDirectory.FullName, namePrefix + counter)));

					counter++;
				}
			}

			Task.Run(() => TransferDirectories(GenerateMoves(), settings));
		}
		public static void TransferDirectories(IEnumerable<(DirectoryInfo source, DirectoryInfo destination)> moves, TransferSettings settings)
		{
			var t = new Task(() =>
			{
				foreach (var move in moves)
				{
					var cts = new CancellationTokenSource();
					var job = new DirectoryTransferJob(new DirectoryTransferArguments(move.source, move.destination, settings), cts.Token);

					JobsPool.StartNew(job, cts);
				}
			});

			if (TaskScheduler.Current == TaskScheduler.Default)
				t.RunSynchronously();
			else
				t.Start(TaskScheduler.Default);
		}
		#endregion
		
		public static void DeleteFileSystemNodes(IEnumerable<FileSystemInfo> targets)
		{
			void ForEachFileSystemNode(FileSystemInfo target)
			{
				var cts = new CancellationTokenSource();

				var job = target.GetType() == typeof(DirectoryInfo) ?
					new DeleteJob(new DeleteJobArguments((DirectoryInfo)target, true), cts.Token) :
					new DeleteJob(new DeleteJobArguments(target), cts.Token);

				JobsPool.StartNew(job, cts);
			}


			var parallelOptions = new ParallelOptions();
			parallelOptions.TaskScheduler = TaskScheduler.Default;
			Parallel.ForEach(targets, parallelOptions, ForEachFileSystemNode);
		}
		public static ISearchView SearchFor(SearchSettings settings)
		{
			var cts = new CancellationTokenSource();
			var search = new FileSystemNodeSearch(settings, cts.Token);

			var task = new Task(search.Start, cts.Token, TaskCreationOptions.LongRunning | TaskCreationOptions.HideScheduler);

			return new SearchWrapper(search, cts, task);
		}


		private static void NameExtensionSplit(string fullName, out string name, out string extension)
		{
			var lastDot = fullName.LastIndexOf('.');
			if (lastDot <= 0)
			{
				name = fullName;
				extension = "";
			}
			else
			{
				name = fullName.Substring(0, lastDot);
				extension = fullName.Substring(lastDot);
			}
		}
	}
}
