using HelperExtensionLibrary;
using MultithreadedFileSystemOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FileManager
{
	/// <summary>
	/// Controls the jobs pane through IJobsPane interface.
	/// </summary>
	internal class JobsPanePresenter : IPanePresenter, IDisposable
	{
		private readonly IJobsPane pane;
		private readonly UnsortedEntriesHolder<JobEntry> entriesHolder;

		/// <summary>
		/// Initializes JobsPanePresenter and registers for job update callback from JobsPool.
		/// </summary>
		public JobsPanePresenter(IJobsPane pane)
		{
			this.pane = pane;

			JobsPool.RegisterNewJobsPane(JobChanged, out List<IJobView> views);

			entriesHolder = new UnsortedEntriesHolder<JobEntry>((EntriesPane<JobEntry>)pane);

			JobEntry[] entries = new JobEntry[views.Count];
			int i = 0;
			foreach (var view in views)
			{
				switch (view.LastStatus)
				{
					case JobStatus.Waiting:
						pane.JobsQueued++;
						break;
					case JobStatus.Running:
						pane.JobsQueued++;
						break;

				}

				entries[i] = new JobEntry(view, view.GetJobTypeDescription());
				i++;
			}

			entriesHolder.AddRange(entries);
		}

		public event ProcessCommandDelegate InvokeCommand;

		/// <summary>
		/// Disposes the underlying pane and sings out of the update callbacks from JobsPool.
		/// </summary>
		public void Dispose()
		{
			JobsPool.SignOutJobsPane(JobChanged);
			pane.GetControl().Dispose();
		}

		public Control GetViewsControl()
		{
			return pane.GetControl();
		}

		/// <summary>
		/// Returns selected jobs.
		/// </summary>
		public IEnumerable<IJobView> GetSelectedJobs()
		{
			var selectedEntries = entriesHolder.SelectedEntries.Count == 0 ?
				entriesHolder.EntryInFocus.AsSingleEnumerable() :
				entriesHolder.SelectedEntries;

			return from e in selectedEntries where !e.JobView.IsDisposed select e.JobView;
		}

		/// <summary>
		/// Processes key press.
		/// </summary>
		/// <param name="pressedKey">Pressed key.</param>
		/// <returns>True if the event was handled, false otherwise.</returns>
		public bool ProcessKeyPress(InputKey pressedKey)
		{
			return entriesHolder.ProcessKeyPress(pressedKey);
		}

		public void SetFocusOnView(bool inFocus)
		{
			pane.InFocus = inFocus;
			entriesHolder.InFocus = inFocus;
		}

		private void JobChanged(IJobView jobView, JobChangeEvent changeEvent)
		{
			if (pane.GetControl().IsDisposed)
			{
				return;
			}

			if (!pane.GetControl().IsHandleCreated)
			{
				pane.GetControl().HandleCreated += (object sender, EventArgs e) => JobChangeHandler(jobView, changeEvent);
			}
			else
			{
				pane.GetControl().BeginInvoke(new Action<IJobView, JobChangeEvent>(JobChangeHandler), jobView, changeEvent);
			}
		}

		private void JobChangeHandler(IJobView jobView, JobChangeEvent changeEvent)
		{
			switch (changeEvent)
			{
				case JobChangeEvent.Enqueued:
					pane.JobsQueued++;
					entriesHolder.Add(new JobEntry(jobView, jobView.GetJobTypeDescription()));
					break;
				case JobChangeEvent.BeforeRun:
					{
						var entry = entriesHolder.Find(e => e.JobId == jobView.Id);
						if (entry == null || entry.Locked)
						{
							return;
						}

						pane.JobsQueued--;
						pane.JobsInProgress++;
						entry.Status = JobStatus.Running;
					}
					break;

				case JobChangeEvent.AfterCompleted:
					{
						var entry = entriesHolder.Find(e => e.JobId == jobView.Id);
						if (entry == null || entry.Locked)
						{
							return;
						}

						pane.JobsInProgress--;

						entry.Status = JobStatus.Done;
						entry.Locked = true;
					}
					break;

				case JobChangeEvent.OnProgressChange:
					{
						var entry = entriesHolder.Find(e => e.JobId == jobView.Id);
						if (entry == null)
						{
							pane.JobsInProgress++;
							entriesHolder.AddToTop(new JobEntry(jobView, jobView.GetJobTypeDescription()));
						}
						else if (entry.Locked)
						{
							return;
						}
						else
						{
							entry.EntryProgress = jobView.Progress;
						}
					}
					break;

				case JobChangeEvent.ExceptionThrown:
					{
						var entry = entriesHolder.Find(e => e.JobId == jobView.Id);
						if (entry == null || entry.Locked)
						{
							return;
						}

						pane.JobsInProgress--;
						entry.EntryException = jobView.Exception;
						entry.Status = JobStatus.Error;
						entry.Locked = true;
					}
					break;

				case JobChangeEvent.Canceled:
					{
						var entry = entriesHolder.Find(e => e.JobId == jobView.Id);
						if (entry == null || entry.Locked)
						{
							return;
						}

						pane.JobsInProgress--;
						entry.Status = JobStatus.Canceled;
						entry.Locked = true;

					}
					break;

				default:
					throw new ArgumentOutOfRangeException("JobsPanePresenter.JobsChanged: Maybe new type of JobChangeEvent?");
			}

			if (changeEvent == JobChangeEvent.AfterCompleted || changeEvent == JobChangeEvent.ExceptionThrown || changeEvent == JobChangeEvent.Canceled)
			{
				Task.Delay(2000).ContinueWith(
					_ => { entriesHolder.Remove(e => e.JobId == jobView.Id); }
					, new CancellationToken()
					, TaskContinuationOptions.None
					, TaskScheduler.FromCurrentSynchronizationContext()
				);
			}
		}
	}
}
