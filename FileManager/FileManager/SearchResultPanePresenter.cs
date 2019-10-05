using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MultithreadedFileOperations;

namespace FileManager
{
	class SearchResultPanePresenter : IPanePresenter, IDisposable
	{
		ISearchResultPane pane;
		UnsortedEntriesHolder<FileSystemNodeEntry> entries;

		public SearchResultPanePresenter(ISearchResultPane pane, ISearchView search)
		{
			this.pane = pane;
			entries = new UnsortedEntriesHolder<FileSystemNodeEntry>((EntriesPane<FileSystemNodeEntry>)pane)
			{
				HighlightingFilter = _ => false
			};

			TaskScheduler UIScheduler = TaskScheduler.FromCurrentSynchronizationContext();

			Search = search;
			Search.SearchDone += () => Task.Factory.StartNew(OnSearchDone, new CancellationToken(), TaskCreationOptions.None, UIScheduler);
			Search.FoundBatchFull += (FileSystemInfo[] batch) => Task.Factory.StartNew(() => ProcessFoundBatch(batch), new CancellationToken(), TaskCreationOptions.None, UIScheduler);

			pane.Status = JobStatus.Waiting;
			pane.SearchingName = SearchedName;
			pane.Found = 0;
			pane.InDirectory = SearchedDirectory.FullName;

			Search.Start();
		}

		public event ProcessCommandDelegate ProcessComand;

		public DirectoryInfo SearchedDirectory { get => Search.Settings.InDirectory; }
		public string SearchedName { get => Search.Settings.Target.Name; }

		ISearchView Search { get; }

		public bool ProcessKeyPress(InputKey pressedKey)
		{
			if (entries.ProcessKeyPress(pressedKey)) return true;

			if (pressedKey == 'l' || pressedKey == Keys.Left || pressedKey == Keys.Enter)
			{
				var parentDir = entries.EntryInFocus.GetType() == typeof(SearchResultDirectoryEntry) ?
					entries.EntryInFocus.Info.FullName :
					((FileInfo)entries.EntryInFocus.Info).DirectoryName;

				Search.Stop();

				ProcessComand?.Invoke(new ChangeDirectoryCommand(parentDir));
				return true;
			}

			if (pressedKey == Keys.Escape)
			{
				Search.Stop();
				pane.Status = JobStatus.Canceled;

				return false;
			}

			return false;
		}
		public Control GetViewsControl() => pane.GetControl();
		public void SetFocusOnView(bool inFocus)
		{
			pane.InFocus = inFocus;
			entries.InFocus = inFocus;
		}
		public void Dispose()
		{
			Search.Stop();
			Search.Dispose();
		}

		void ProcessFoundBatch(FileSystemInfo[] foundBatch)
		{
			if (entries.Count == 0) pane.Status = JobStatus.Running;

			pane.Found += foundBatch.Length;
			var newEntries = from info in foundBatch
							 select FromInfoToEntry(info);

			entries.AddRange(newEntries.ToArray());
		}
		void OnSearchExceptionRise(FileOperationException e)
		{
			if (entries.Count == 0) pane.Status = JobStatus.Running;

			entries.Add(new ErrorEntry(e.Message, e.InnerException.GetType().ToString()));
		}
		void OnSearchDone()
		{
			if (pane.Status != JobStatus.Canceled)
				pane.Status = JobStatus.Done;
		}

		FileSystemNodeEntry FromInfoToEntry(FileSystemInfo info)
		{
			if (info.GetType() == typeof(FileInfo))
				return new SearchResultFileEntry(SearchedDirectory.FullName) { Info = info };
			else
				return new SearchResultDirectoryEntry(SearchedDirectory.FullName) { Info = info };
		}

	}
}
