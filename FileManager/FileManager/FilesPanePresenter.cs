using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using HelperExtensionLibrary;
using System.Diagnostics;
using System.Security.AccessControl;
using System.Security.Principal;

namespace FileManager
{
	class FilesPanePresenter : IPanePresenter, IDisposable
	{
		readonly IFilesPane pane;
		readonly SortedEntriesHolder<FileSystemNodeEntry> entriesHolder;
		readonly TaskScheduler UIScheduler;
		
		FileSystemWatcher fsWatcher;

		public FilesPanePresenter(IFilesPane paneView, DirectoryInfo currentDir)
			: this(paneView, currentDir, (x, y) => string.Compare(x.EntryName, y.EntryName))
		{}
		public FilesPanePresenter(IFilesPane paneView, DirectoryInfo currentDir, Comparison<FileSystemNodeEntry> sortOrder)
		{
			pane = paneView;

			entriesHolder = new SortedEntriesHolder<FileSystemNodeEntry>((EntriesPane<FileSystemNodeEntry>)pane, AlterSortOrder(sortOrder))
			{
				HighlightingFilter = entry => entry.GetType() != typeof(ParentDirectoryEntry)
			};

			entriesHolder.NewEntryHighlighted += NewEntryHighlighted;
			entriesHolder.OldEntryUnhighlighted += OldEntryUnhighlighted;

			ChangeDirectory(currentDir);

			UIScheduler = TaskScheduler.FromCurrentSynchronizationContext();
		}

		public event ProcessCommandDelegate ProcessComand;

		public DirectoryInfo CurrentDir { get => pane.CurrentDir; }

		bool HasParentDir { get => pane.CurrentDir.Parent != null; }

		public bool ProcessKeyPress(InputKey pressedKey)
		{
			if(entriesHolder.ProcessKeyPress(pressedKey)) return true;

			if (pressedKey == 'l' || pressedKey == Keys.Return || pressedKey == Keys.Right)
			{
				var targetDir = entriesHolder.EntryInFocus.Info as DirectoryInfo;
				if (targetDir != null) ChangeDirectory(targetDir);
				return true;
			}

			if (pressedKey == 'h' || pressedKey == Keys.Left)
			{
				var parentDir = HasParentDir ? entriesHolder[0].Info as DirectoryInfo : null;
				if (parentDir != null) ChangeDirectory(parentDir);
				return true;
			}

			return false;
		}
		public void SetFocusOnView(bool inFocus)
		{
			pane.InFocus = inFocus;
			entriesHolder.InFocus = inFocus;
		}
		public Control GetViewsControl() => pane.GetControl();
		public void ChangeDirectory(DirectoryInfo targetDir)
		{
			fsWatcher?.Dispose();

			entriesHolder.Invalidate();

			pane.SelectedEntriesCount = 0;
			pane.SelectedEntriesSize = 0;
			pane.CurrentDir = targetDir;

			RefreshEntries();

			entriesHolder.Update();

			RegisterWatcher();
		}
		public void SetEntrySortOrder(Comparison<FileSystemNodeEntry> order)
		{
			entriesHolder.SortOrder = AlterSortOrder(order);
		}
		public IEnumerable<FileSystemInfo> GetSelectedFileSystemInfos()
		{
			if (entriesHolder.SelectedEntries.Count > 0)
				return entriesHolder.SelectedEntries.Select(e => e.Info);
			else
				return entriesHolder.EntryInFocus.Info.AsSingleEnumerable();
		}
		public void Dispose()
		{
			fsWatcher.Dispose();
		}

		void OnCurrentDirChange(object sender, FileSystemEventArgs e)
		{
			Task.Factory.StartNew(() =>
			{
				entriesHolder.Invalidate();
				RefreshEntries();
				entriesHolder.Update();
			}, new CancellationToken(), TaskCreationOptions.None, UIScheduler);
		}
		void OnCurrentDirChange(object sender, RenamedEventArgs e)
		{
			OnCurrentDirChange(sender, new FileSystemEventArgs(WatcherChangeTypes.Renamed, CurrentDir.FullName, e.FullPath));
		}
		void OnWatcherError(object sender, ErrorEventArgs e)
		{
			Task.Factory.StartNew(() =>
			{
				fsWatcher.Dispose();
				RegisterWatcher();
			}, new CancellationToken(), TaskCreationOptions.None, UIScheduler);
		}
		void RegisterWatcher()
		{
			fsWatcher = new FileSystemWatcher(CurrentDir.FullName);
			fsWatcher.Changed += (object sender, FileSystemEventArgs e) => Task.Factory.StartNew(() => OnCurrentDirChange(sender, e));
			fsWatcher.Created += (object sender, FileSystemEventArgs e) => Task.Factory.StartNew(() => OnCurrentDirChange(sender, e));
			fsWatcher.Deleted += (object sender, FileSystemEventArgs e) => Task.Factory.StartNew(() => OnCurrentDirChange(sender, e));
			fsWatcher.Renamed += (object sender, RenamedEventArgs e) => Task.Factory.StartNew(() => OnCurrentDirChange(sender, e));
			fsWatcher.NotifyFilter = NotifyFilters.LastAccess |
										NotifyFilters.LastWrite |
										NotifyFilters.FileName |
										NotifyFilters.DirectoryName;
			fsWatcher.Error += (object sender, ErrorEventArgs e) => Task.Factory.StartNew(() => OnWatcherError(sender, e));
			fsWatcher.EnableRaisingEvents = true;
		}
		void RefreshEntries()
		{
			pane.CurrentDir.Refresh();

			if (HasParentDir) entriesHolder.Add(new ParentDirectoryEntry { Info = pane.CurrentDir.Parent });

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
		
		void NewEntryHighlighted(FileSystemNodeEntry entry)
		{
			pane.SelectedEntriesCount++;
			pane.SelectedEntriesSize += ComputeSize(entriesHolder.EntryInFocus.Info);
		}
		void OldEntryUnhighlighted(FileSystemNodeEntry entry)
		{
			pane.SelectedEntriesCount--;
			pane.SelectedEntriesSize -= ComputeSize(entriesHolder.EntryInFocus.Info);
		}
		long ComputeSize(FileSystemInfo info)
		{
			var l = (info as FileInfo)?.Length;
			return l == null ? 0 : (long)l;
		}
		Comparison<FileSystemNodeEntry> AlterSortOrder(Comparison<FileSystemNodeEntry> order)
		{
			return (x, y) =>
			{
				if (x.GetType() == typeof(ParentDirectoryEntry)) return -1;
				if (y.GetType() == typeof(ParentDirectoryEntry)) return 1;

				return order(x, y);
			};
		}

	}
}