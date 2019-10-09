using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace MultithreadedFileOperations
{
	/// <summary>
	/// Operations class contains all operations which MultithreadedFileOperations library offers
	/// </summary>
	public static class Operations
	{
		#region Transfer
		/// <summary>
		/// Transfers files to the destination directory, maintaining the original file names.
		/// </summary>
		/// <param name="sources">Files to be transferd</param>
		/// <param name="destinationDirectory">Destination directory</param>
		/// <param name="settings">Settings of the transfer</param>
		public static void TransferFiles(IEnumerable<FileInfo> sources, DirectoryInfo destinationDirectory, TransferSettings settings)
		{
			if (sources == null)
			{
				throw new ArgumentNullException(nameof(sources));
			}

			if (destinationDirectory == null)
			{
				throw new ArgumentNullException(nameof(destinationDirectory));
			}

			IEnumerable<(FileInfo, FileInfo)> GenerateMoves()
			{
				foreach (var source in sources)
				{
					yield return (source, new FileInfo(Path.Combine(destinationDirectory.FullName, source.Name)));
				}
			}

			TransferFiles(GenerateMoves(), settings);
		}

		/// <summary>
		/// Transfers files to the destination directory. Each file gets new name, which is constructed of prefix, a number and a extension.
		/// </summary>
		/// <param name="sources">Files to be transfered</param>
		/// <param name="destinationDirectory">Destination directory</param>
		/// <param name="namePrefix">Template for destination file names</param>
		/// <param name="transferSettings">Settings of the transfer</param>
		public static void TransferFiles(IEnumerable<FileInfo> sources, DirectoryInfo destinationDirectory, string namePrefix, TransferSettings transferSettings)
		{
			if (sources == null)
			{
				throw new ArgumentNullException(nameof(sources));
			}

			if (destinationDirectory == null)
			{
				throw new ArgumentNullException(nameof(destinationDirectory));
			}

			if (namePrefix == null)
			{
				throw new ArgumentNullException(nameof(namePrefix));
			}

			NameExtensionSplit(namePrefix, out string name, out string extension);

			IEnumerable<(FileInfo, FileInfo)> GenerateMoves()
			{
				int counter = 0;
				foreach (var source in sources)
				{
					yield return (source, new FileInfo(Path.Combine(destinationDirectory.FullName, name + counter + extension)));

					counter++;
				}
			}

			TransferFiles(GenerateMoves(), transferSettings);
		}

		/// <summary>
		/// Transfers files with provided transfer settings.
		/// </summary>
		/// <param name="transfers">Enumerable of tuples, first source, then destination</param>
		/// <param name="transferSettings">Settings of the trasnfer</param>
		public static void TransferFiles(IEnumerable<(FileInfo source, FileInfo destination)> transfers, TransferSettings transferSettings)
		{
			if (transfers == null)
			{
				throw new ArgumentNullException(nameof(transfers));
			}

			foreach (var transfer in transfers)
			{
				var cts = new CancellationTokenSource();
				var job = new FileTransferJob(new FileTransferArguments(transfer.source, transfer.destination, transferSettings), cts.Token);

				JobsPool.EnqueueNew(job, cts);
			}
		}

		/// <summary>
		/// Transfers directories to the destination directory, maintaining the original directory names.
		/// </summary>
		/// <param name="sources">Directories to be transferd</param>
		/// <param name="destinationDirectory">Destination directory</param>
		/// <param name="settings">Settings of the transfer</param>
		public static void TransferDirectories(IEnumerable<DirectoryInfo> sources, DirectoryInfo destinationDirectory, TransferSettings settings)
		{
			if (sources == null)
			{
				throw new ArgumentNullException(nameof(sources));
			}

			if (destinationDirectory == null)
			{
				throw new ArgumentNullException(nameof(destinationDirectory));
			}

			IEnumerable<(DirectoryInfo, DirectoryInfo)> GenerateMoves()
			{
				foreach (var source in sources)
				{
					yield return (source, new DirectoryInfo(Path.Combine(destinationDirectory.FullName, source.Name)));
				}
			}

			TransferDirectories(GenerateMoves(), settings);
		}

		/// <summary>
		/// Transfers directories to the destination directory. Each directory gets new name, which is constructed of prefix and a number.
		/// </summary>
		/// <param name="sources">Directories to be transfered</param>
		/// <param name="destinationDirectory">Destination directory</param>
		/// <param name="namePrefix">Template for destination directory names</param>
		/// <param name="transferSettings">Settings of the transfer</param>
		public static void TransferDirectories(IEnumerable<DirectoryInfo> sources, DirectoryInfo destinationDirectory, string namePrefix, TransferSettings settings)
		{
			if (sources == null)
			{
				throw new ArgumentNullException(nameof(sources));
			}

			if (destinationDirectory == null)
			{
				throw new ArgumentNullException(nameof(destinationDirectory));
			}

			if (namePrefix == null)
			{
				throw new ArgumentNullException(nameof(namePrefix));
			}

			IEnumerable<(DirectoryInfo, DirectoryInfo)> GenerateMoves()
			{
				int counter = 0;
				foreach (var source in sources)
				{
					yield return (source, new DirectoryInfo(Path.Combine(destinationDirectory.FullName, namePrefix + counter)));

					counter++;
				}
			}

			TransferDirectories(GenerateMoves(), settings);
		}

		/// <summary>
		/// Transfers directories with provided transfer settings.
		/// </summary>
		/// <param name="transfers">Enumerable of tuples, first source, then destination</param>
		/// <param name="transferSettings">Settings of the trasnfer</param>
		public static void TransferDirectories(IEnumerable<(DirectoryInfo source, DirectoryInfo destination)> transfers, TransferSettings settings)
		{
			if (transfers == null)
			{
				throw new ArgumentNullException(nameof(transfers));
			}

			foreach (var transfer in transfers)
			{
				var cts = new CancellationTokenSource();
				var job = new DirectoryTransferJob(new DirectoryTransferArguments(transfer.source, transfer.destination, settings), cts.Token);

				JobsPool.EnqueueNew(job, cts);
			}
		}
		#endregion


		/// <summary>
		/// Deletes provided file system nodes. Non empty directories are also deleted.
		/// </summary>
		/// <param name="targets">File system nodes to be deleted</param>
		public static void DeleteFileSystemNodes(IEnumerable<FileSystemInfo> targets)
		{
			if (targets == null)
			{
				throw new ArgumentNullException(nameof(targets));
			}

			foreach (var target in targets)
			{
				var cts = new CancellationTokenSource();

				var job = target.GetType() == typeof(DirectoryInfo) ?
					new DeleteJob(new DeleteJobArguments((DirectoryInfo)target, true), cts.Token) :
					new DeleteJob(new DeleteJobArguments(target), cts.Token);

				JobsPool.EnqueueNew(job, cts);
			}
		}

		/// <summary>
		/// Returns view or handle to a search, initialized with provided settings. Search is not started.
		/// </summary>
		/// <param name="settings">Search settings</param>
		/// <returns>View of an initialized search</returns>
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
