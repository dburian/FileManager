using HelperExtensionLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FileManager
{
	/// <summary>
	/// Controls files pane through IFilesPane interface.
	/// </summary>
	internal class FilesPanePresenter : IPanePresenter, IDisposable
	{
		private readonly IFilesPane pane;
		private readonly SortedEntriesHolder<FileSystemNodeEntry> entriesHolder;

		/// <summary>
		/// Initializes FilesPanePresenter using default comparison of entries.
		/// </summary>
		/// <param name="paneView">Underlying files pane.</param>
		/// <param name="currentDir">Current position in the file system tree.</param>
		public FilesPanePresenter(IFilesPane paneView, DirectoryInfo currentDir)
			: this(paneView, currentDir, (x, y) => string.Compare(x.EntryName, y.EntryName, StringComparison.OrdinalIgnoreCase))
		{ }

		/// <summary>
		/// Initializes FilesPanePresenter using the comparison provided in <paramref name="sortOrder"/>.
		/// </summary>
		/// <param name="paneView">Underlying files pane.</param>
		/// <param name="currentDir">Current position in file system tree.</param>
		/// <param name="sortOrder">Sort order according to which should be the entries sorted.</param>
		public FilesPanePresenter(IFilesPane paneView, DirectoryInfo currentDir, Comparison<FileSystemNodeEntry> sortOrder)
		{
			pane = paneView;

			entriesHolder = new SortedEntriesHolder<FileSystemNodeEntry>((EntriesPane<FileSystemNodeEntry>)pane, AlterSortOrder(sortOrder))
			{
				HighlightingFilter = entry => entry.GetType() != typeof(ParentDirectoryEntry) && entry.GetType() != typeof(ErrorEntry)
			};

			entriesHolder.NewEntryHighlighted += NewEntryHighlighted;
			entriesHolder.OldEntryUnhighlighted += OldEntryUnhighlighted;

			ChangeDirectory(currentDir);

		}

		/// <summary>
		/// Gets invoked if FilesPanePresenter needs MainFormPresenter to process a command.
		/// </summary>
		public event ProcessCommandDelegate InvokeCommand;

		/// <summary>
		/// Gets the current dirrectory being viewed.
		/// </summary>
		public DirectoryInfo CurrentDir => pane.CurrentDir;

		private bool HasParentDir => pane.CurrentDir.Parent != null;

		/// <summary>
		/// Processes key press.
		/// </summary>
		/// <param name="pressedKey">Pressed key.</param>
		/// <returns>True if the event was handled, false otherwise.</returns>
		public bool ProcessKeyPress(InputKey pressedKey)
		{
			if (entriesHolder.ProcessKeyPress(pressedKey))
			{
				return true;
			}

			if (pressedKey == 'l' || pressedKey == Keys.Return || pressedKey == Keys.Right)
			{
				DirectoryInfo targetDir = entriesHolder.EntryInFocus.Info as DirectoryInfo;
				if (targetDir != null)
				{
					ChangeDirectory(targetDir);
				}

				return true;
			}

			if (pressedKey == 'h' || pressedKey == Keys.Left)
			{
				DirectoryInfo parentDir = HasParentDir ? entriesHolder[0].Info as DirectoryInfo : null;
				if (parentDir != null)
				{
					ChangeDirectory(parentDir);
				}

				return true;
			}

			return false;
		}

		/// <summary>
		/// Sets focus on underlying files pane.
		/// </summary>
		/// <param name="inFocus"></param>
		public void SetFocusOnView(bool inFocus)
		{
			pane.InFocus = inFocus;
			entriesHolder.InFocus = inFocus;
		}

		public Control GetViewsControl()
		{
			return pane.GetControl();
		}

		/// <summary>
		/// Changes the current viewing directory to <paramref name="targetDir"/>.
		/// </summary>
		/// <param name="targetDir">New directory to be viewed.</param>
		public void ChangeDirectory(DirectoryInfo targetDir)
		{
			targetDir.Refresh();
			while (!targetDir.Exists)
			{
				targetDir = targetDir.Parent;
			}

			entriesHolder.ClearAndReset();

			pane.SelectedEntriesCount = 0;
			pane.SelectedEntriesSize = 0;
			pane.CurrentDir = targetDir;

			RefreshEntries();

			entriesHolder.UpdateView();
		}

		/// <summary>
		/// Sets different sorting of entries.
		/// </summary>
		/// <param name="order">New sorting of entries.</param>
		public void SetEntrySortOrder(Comparison<FileSystemNodeEntry> order)
		{
			entriesHolder.SortOrder = AlterSortOrder(order);
		}

		/// <summary>
		/// Gets the selected entries and returns FileSystemInfo (i.e. handle to the file or directory they are representing).
		/// </summary>
		/// <returns>Enumerable of selected FileSystemInfos, null if no eligible entries are selected.</returns>
		public IEnumerable<FileSystemInfo> GetSelectedFileSystemInfos()
		{
			if (entriesHolder.SelectedEntries.Count > 0)
			{
				return entriesHolder.SelectedEntries.Select(e => e.Info);
			}
			else if (entriesHolder.EntryInFocus.GetType() != typeof(ParentDirectoryEntry) && entriesHolder.EntryInFocus.GetType() != typeof(ErrorEntry))
			{
				return entriesHolder.EntryInFocus.Info.AsSingleEnumerable();
			}

			return null;
		}
		public void Dispose()
		{
			pane.GetControl().Dispose();
		}

		private void RefreshEntries()
		{
			pane.CurrentDir.Refresh();

			if (HasParentDir)
			{
				entriesHolder.Add(new ParentDirectoryEntry { Info = pane.CurrentDir.Parent });
			}

			FileInfo[] files;
			DirectoryInfo[] dirs;
			try
			{
				files = pane.CurrentDir.GetFiles();
				dirs = pane.CurrentDir.GetDirectories();

				entriesHolder.AddRange((from info in files select new FileEntry { Info = info }).ToArray());
				entriesHolder.AddRange((from info in dirs select new DirectoryEntry { Info = info }).ToArray());

				pane.FileEntriesCount = files.Length;
				pane.DirectoryEntriesCount = dirs.Length;
			}
			catch (UnauthorizedAccessException e)
			{
				entriesHolder.Add(new ErrorEntry(e.Message, "Unauthorized Access"));
				pane.FileEntriesCount = 0;
				pane.DirectoryEntriesCount = 0;
			}

			var d = new DriveInfo(pane.CurrentDir.Root.FullName);

			pane.FreeSpaceInDir = d.TotalFreeSpace;
		}

		private void NewEntryHighlighted(FileSystemNodeEntry entry)
		{
			pane.SelectedEntriesCount++;
			pane.SelectedEntriesSize += ComputeSize(entriesHolder.EntryInFocus.Info);
		}

		private void OldEntryUnhighlighted(FileSystemNodeEntry entry)
		{
			pane.SelectedEntriesCount--;
			pane.SelectedEntriesSize -= ComputeSize(entriesHolder.EntryInFocus.Info);
		}

		private static long ComputeSize(FileSystemInfo info)
		{
			var l = (info as FileInfo)?.Length;
			return l == null ? 0 : (long)l;
		}

		private static Comparison<FileSystemNodeEntry> AlterSortOrder(Comparison<FileSystemNodeEntry> order)
		{
			return (x, y) =>
			{
				if (x.GetType() == typeof(ParentDirectoryEntry))
				{
					return -1;
				}

				if (y.GetType() == typeof(ParentDirectoryEntry))
				{
					return 1;
				}

				return order(x, y);
			};
		}

	}
}