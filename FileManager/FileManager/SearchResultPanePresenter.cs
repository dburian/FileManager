using MultithreadedFileOperations;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FileManager
{
	/// <summary>
	/// Controls SearchResultPane through ISrearchResultPane.
	/// </summary>
	internal class SearchResultPanePresenter : IPanePresenter, IDisposable
	{
		private readonly ISearchResultPane pane;
		private readonly UnsortedEntriesHolder<FileSystemNodeEntry> entries;

		/// <summary>
		/// Initializes SearchResultPanePresenter and starts the search.
		/// </summary>
		public SearchResultPanePresenter(ISearchResultPane pane, ISearchView search)
		{
			this.pane = pane;
			entries = new UnsortedEntriesHolder<FileSystemNodeEntry>((EntriesPane<FileSystemNodeEntry>)pane)
			{
				HighlightingFilter = _ => false
			};

			Search = search;
			Search.SearchDone += HandleSearchDone;
			Search.FoundBatchFull += HandleBatchFound;

			pane.Status = JobStatus.Waiting;
			pane.SearchingName = SearchedName;
			pane.Found = 0;
			pane.InDirectory = SearchedDirectory.FullName;

			Search.Start();
			pane.Status = JobStatus.Running;
		}

		public event ProcessCommandDelegate InvokeCommand;

		public DirectoryInfo SearchedDirectory => Search.Settings.InDirectory;
		public string SearchedName => Search.Settings.Target.Name;

		private ISearchView Search { get; }

		/// <summary>
		/// Processes key press.
		/// </summary>
		/// <param name="pressedKey">Pressed key.</param>
		/// <returns>True if the event was handled, false otherwise.</returns>
		public bool ProcessKeyPress(InputKey pressedKey)
		{
			if (entries.ProcessKeyPress(pressedKey))
			{
				return true;
			}

			if (pressedKey == 'l' || pressedKey == Keys.Left || pressedKey == Keys.Enter)
			{
				var parentDir = entries.EntryInFocus.GetType() == typeof(SearchResultDirectoryEntry) ?
					entries.EntryInFocus.Info.FullName :
					((FileInfo)entries.EntryInFocus.Info).DirectoryName;

				Search.Cancel();

				InvokeCommand?.Invoke(new ChangeDirectoryCommand(parentDir));
				return true;
			}

			if (pressedKey == Keys.Escape)
			{
				Search.Cancel();
				pane.Status = JobStatus.Canceled;

				return false;
			}

			return false;
		}
		public Control GetViewsControl()
		{
			return pane.GetControl();
		}

		public void SetFocusOnView(bool inFocus)
		{
			pane.InFocus = inFocus;
			entries.InFocus = inFocus;
		}
		public void Dispose()
		{
			Search.Cancel();
			Search.Dispose();
		}

		private void HandleBatchFound(FileSystemInfo[] batch)
		{
			PaneBeginInvoke(ProcessFoundBatch, batch);
		}

		private void HandleSearchDone()
		{
			PaneBeginInvoke(OnSearchDone);
		}

		private void ProcessFoundBatch(FileSystemInfo[] foundBatch)
		{
			pane.Found += foundBatch.Length;
			var newEntries = from info in foundBatch
							 select EntryFromInfo(info);

			entries.AddRange(newEntries.ToArray());
		}

		private void OnSearchExceptionRise(FileOperationException e)
		{
			if (entries.Count == 0)
			{
				pane.Status = JobStatus.Running;
			}

			entries.Add(new ErrorEntry(e.Message, e.InnerException.GetType().ToString()));
		}

		private void OnSearchDone()
		{
			if (pane.Status != JobStatus.Canceled)
			{
				pane.Status = JobStatus.Done;
			}
		}

		private void PaneBeginInvoke<T>(Action<T> operation, T args)
		{
			if (pane.GetControl().IsDisposed)
			{
				return;
			}

			if (!pane.GetControl().IsHandleCreated)
			{
				pane.GetControl().HandleCreated += (object sender, EventArgs e) => operation(args);
			}
			else
			{
				pane.GetControl().BeginInvoke(operation, args);
			}
		}

		private void PaneBeginInvoke(Action operation)
		{
			if (pane.GetControl().IsDisposed)
			{
				return;
			}

			if (!pane.GetControl().IsHandleCreated)
			{
				pane.GetControl().HandleCreated += (object sender, EventArgs e) => operation();
			}
			else
			{
				pane.GetControl().BeginInvoke(operation);
			}
		}

		private FileSystemNodeEntry EntryFromInfo(FileSystemInfo info)
		{
			if (info.GetType() == typeof(FileInfo))
			{
				return new SearchResultFileEntry(SearchedDirectory.FullName) { Info = info };
			}
			else
			{
				return new SearchResultDirectoryEntry(SearchedDirectory.FullName) { Info = info };
			}
		}

	}
}
