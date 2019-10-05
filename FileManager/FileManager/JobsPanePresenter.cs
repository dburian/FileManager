using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MultithreadedFileOperations;
using System.Threading;

namespace FileManager
{
	class JobsPanePresenter : IPanePresenter, IDisposable
	{
		IJobsPane pane;
		SortedEntriesHolder<JobEntry> entriesHolder;

		TaskScheduler UIScheduler;

		public JobsPanePresenter(IJobsPane pane)
		{
			this.pane = pane;
			List<IJobView> views;

			UIScheduler = TaskScheduler.FromCurrentSynchronizationContext();

			JobsPool.UIThreadSafe.RegisterNewJobsPane(JobChanged, out views);

			entriesHolder = new SortedEntriesHolder<JobEntry>((EntriesPane<JobEntry>)pane, (x, y) => x.EntryType - y.EntryType);

			//TODO: entries do not function properly
			entriesHolder.AddRange(views.Select(view => new JobEntry(view) { Status = JobStatus.Waiting }).ToArray());
		}

		public event ProcessCommandDelegate ProcessComand;

		public void Dispose()
		{
			JobsPool.UIThreadSafe.SignOutJobsPane(JobChanged);
		}

		public Control GetViewsControl() => pane.GetControl();

		public bool ProcessKeyPress(InputKey pressedKey)
		{
			return entriesHolder.ProcessKeyPress(pressedKey);
		}

		public void SetFocusOnView(bool inFocus)
		{
			pane.InFocus = inFocus;
			entriesHolder.InFocus = inFocus;
		}

		void JobChanged(IJobView jobView, JobChangeEvent changeEvent)
		{
			Task.Factory.StartNew(() => {
				switch (changeEvent)
				{
					case JobChangeEvent.BeforeRun:
						entriesHolder.Add(new JobEntry(jobView) { Status = JobStatus.Waiting });
						break;

					case JobChangeEvent.AfterCompleted:
						{
							var entry = entriesHolder.Find(e => e.JobId == jobView.Id);
							if (entry == null || entry.Locked) return;

							entry.Status = JobStatus.Done;
							entry.Locked = true;
							Task.Delay(500).ContinueWith(
								_ => { entriesHolder.Remove(e => e.JobId == jobView.Id); }
								, new CancellationToken()
								, TaskContinuationOptions.None
								, TaskScheduler.FromCurrentSynchronizationContext()
							);
						}
						break;

					case JobChangeEvent.OnProgressChange:
						{
							var entry = entriesHolder.Find(e => e.JobId == jobView.Id);
							if (entry != null && !entry.Locked) entry.EntryProgress = jobView.Progress;
						}
						break;

					case JobChangeEvent.OnExceptionRaise:
						{
							var entry = entriesHolder.Find(e => e.JobId == jobView.Id);
							if (entry == null || entry.Locked) return;

							entry.EntryException = jobView.Exception;
							entry.Status = JobStatus.Error;
							entry.Locked = true;
						}
						break;

					case JobChangeEvent.Canceled:
						{
							var entry = entriesHolder.Find(e => e.JobId == jobView.Id);
							if (entry == null || entry.Locked) return;

							entry.Status = JobStatus.Canceled;
							entry.Locked = true;
						}
						break;
						
					default:
						throw new ArgumentOutOfRangeException("JobsPanePresenter.JobsChanged: Maybe new type of JobChangeEvent?");
				}
			}, new CancellationToken(), TaskCreationOptions.None, UIScheduler);
		}

	}
}
