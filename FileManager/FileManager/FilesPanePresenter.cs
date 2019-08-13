using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Security.AccessControl;
using System.Security.Principal;

namespace FileManager
{
	class FilesPanePresenter : IPanePresenter
	{
		int _entryInFocusIndex = 0;
		bool _highlighting = false;
		Comparer<FilesViewEntry> _sortOrder;

		IFilesPane pane;
		List<FilesViewEntry> selectedEntries = new List<FilesViewEntry>();
		List<FilesViewEntry> preparedEntries = new List<FilesViewEntry>();

	
		public FilesPanePresenter(IFilesPane paneView, DirectoryInfo currentDir)
			: this(paneView, currentDir, Comparer<FilesViewEntry>.Create((x, y) => string.Compare(x.EntryName, y.EntryName)))
		{}
		public FilesPanePresenter(IFilesPane paneView, DirectoryInfo currentDir, Comparer<FilesViewEntry> sortOrder)
		{
			pane = paneView;
			pane.ScrollPanel.Resize += (object sender, EventArgs e) => AdjustScrollPanel();
			_sortOrder = sortOrder;

			DirectoryChange(currentDir);
		}

		FilesViewEntry EntryInFocus { get => pane.Entries[EntryInFocusIndex]; }
		bool HasParentDir { get => pane.CurrentDir.Parent != null; }
		int FilesViewWindowTop
		{
			get => pane.ScrollPanel.VerticalScroll.Value;
			set => pane.ScrollPanel.VerticalScroll.Value = value;
		}
		int FilesViewWindowBottom
		{
			get => FilesViewWindowTop + pane.ScrollPanel.Height;
			set => pane.ScrollPanel.VerticalScroll.Value = value - pane.ScrollPanel.Height;
		}
		int EntryInFocusIndex
		{
			get => _entryInFocusIndex;
			set
			{
				if (value < 0 || value >= pane.Entries.Count) return;

				EntryInFocus.InFocus = false;
				pane.Entries[value].InFocus = true;

				_entryInFocusIndex = value;     //Switch focus to new entry

				AdjustScrollPanel();

				HighlightEntryInFocus();
			}
		}
		double EntryInFocusTop { get => EntryInFocusIndex * 18; }
		bool Highlighting
		{
			get => _highlighting;
			set
			{
				_highlighting = value;

				HighlightEntryInFocus();
			}
		}
		Comparer<FilesViewEntry> SortOrder
		{
			get => _sortOrder;
			set
			{
				_sortOrder = value;

				SortPreparedEntries();

				PushEntryChanges();
			}
		}

		public void RefreshEntries()
		{
			pane.CurrentDir.Refresh();

			preparedEntries = new List<FilesViewEntry>();

			if (HasParentDir) preparedEntries.Add(new ParentDirectoryEntry { Info = pane.CurrentDir.Parent });

			FileInfo[] files;
			DirectoryInfo[] dirs;
			try
			{
				files = pane.CurrentDir.GetFiles();
				dirs = pane.CurrentDir.GetDirectories();

				preparedEntries.AddRange(from info in files.AsParallel() select new FileEntry { Info = info });
				preparedEntries.AddRange(from info in dirs.AsParallel() select new DirectoryEntry { Info = info });

				pane.FileEntriesCount = files.Length;
				pane.DirectoryEntriesCount = dirs.Length;
			}
			catch (UnauthorizedAccessException e)
			{
				preparedEntries.Add(new ErrorEntry(e.Message, "Unauthorized Access"));
				pane.FileEntriesCount = 0;
				pane.DirectoryEntriesCount = 0;
			}

			var d = new DriveInfo(pane.CurrentDir.Root.FullName);

			pane.FreeSpaceInDir = d.TotalFreeSpace;

			SortPreparedEntries();

			PushEntryChanges();
		}
		public bool ProcessKeyPress(char keyChar)
		{
			switch (keyChar)
			{
				case 'j':
				case (char)Keys.Down:
					EntryInFocusIndex++;
					return true;

				case 'k':
				case (char)Keys.Up:
					EntryInFocusIndex--;
					return true;

				case 'g':
				case (char)Keys.Home:
					Debug.WriteLine("Home");
					if (!Highlighting) EntryInFocusIndex = 0;
					else
						while (EntryInFocusIndex > 0) EntryInFocusIndex--;
					return true;

				case 'G':
				case (char)Keys.End:
					Debug.WriteLine("End");
					if (!Highlighting) EntryInFocusIndex = pane.Entries.Count - 1;
					else
						while (EntryInFocusIndex < pane.Entries.Count - 1) EntryInFocusIndex++;
					return true;

				case 'l':
				case (char)Keys.Right:
				case (char)Keys.Return:
					var targetDir = EntryInFocus.Info as DirectoryInfo;
					if (targetDir != null) DirectoryChange(targetDir);
					return true;

				case 'h':
				case (char)Keys.Left:
					var parentDir = HasParentDir ? pane.Entries[0].Info as DirectoryInfo : null;
					if (parentDir != null) DirectoryChange(parentDir);
					return true;

				case 'v':
					Highlighting = !Highlighting;
					return true;

				default:
					return false;
			}
		}
		public void SetFocusOnView(bool inFocus) => pane.InFocus = inFocus;
		public Control GetViewsControl() => pane.GetControl();

		void DirectoryChange(DirectoryInfo targetDir)
		{
			
			pane.CurrentDir = targetDir;
			RefreshEntries();

			selectedEntries = new List<FilesViewEntry>();
			pane.HighlightedEntriesCount = 0;
			pane.HighlightedEntriesSize = 0;

			//Not using the property, because the entry that was in focus no longer exists,
			// therefore it does not make sense to set its InFocus property
			_entryInFocusIndex = 0;
			EntryInFocus.InFocus = true;

			Highlighting = false;
		}
		void PushEntryChanges() => pane.Entries = preparedEntries;
		void SortPreparedEntries()
		{
			if (HasParentDir)
				preparedEntries.Sort(1, preparedEntries.Count - 1, SortOrder);
			else
				preparedEntries.Sort(SortOrder);
		}
		void HighlightEntryInFocus()
		{
			if (Highlighting && EntryInFocus.GetType() != typeof(ParentDirectoryEntry))
			{

				if (EntryInFocus.Highlighted)
				{
					selectedEntries.Remove(EntryInFocus);
					EntryInFocus.Highlighted = false;
					pane.HighlightedEntriesCount--;
					pane.HighlightedEntriesSize -= ComputeSize(EntryInFocus.Info);
				}
				else
				{
					selectedEntries.Add(EntryInFocus);
					EntryInFocus.Highlighted = true;
					pane.HighlightedEntriesCount++;
					pane.HighlightedEntriesSize += ComputeSize(EntryInFocus.Info);
				}

			}
		}
		long ComputeSize(FileSystemInfo info)
		{
			var l = (info as FileInfo)?.Length;
			return l == null ? 0 : (long)l;
		}
		void AdjustScrollPanel()
		{
			var entryInFocusTop = EntryInFocusIndex * EntryInFocus.Height;

			while (entryInFocusTop < FilesViewWindowTop) FilesViewWindowTop -= Math.Min(EntryInFocus.Height, FilesViewWindowTop);

			while (entryInFocusTop + EntryInFocus.Height > FilesViewWindowBottom) FilesViewWindowTop += EntryInFocus.Height;
		}
	}
}