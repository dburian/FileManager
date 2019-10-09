using System.Windows.Forms;

namespace FileManager
{
	/// <summary>
	/// Displays jobs and their state.
	/// </summary>
	public partial class JobsPane : EntriesPane<JobEntry>, IJobsPane
	{
		private int _inProgress;
		private int _queued;
		private bool _inFocus;

		public JobsPane()
		{
			InitializeComponent();
		}

		public int JobsInProgress
		{
			get => _inProgress;
			set
			{
				_inProgress = value;

				jobsInProgressLabel.Text = $"{_inProgress} jobs in progress";
			}
		}

		public int JobsQueued
		{
			get => _queued;
			set
			{
				_queued = value;

				jobsQueuedLabel.Text = $"{_queued} jobs in queue";
			}
		}


		public override bool InFocus
		{
			get => _inFocus;
			set
			{
				_inFocus = value;

				if (_inFocus)
				{
					jobsLabel.BackColor = Config.ColorPalette.Black;
					jobsLabel.ForeColor = Config.ColorPalette.White;
				}
				else
				{
					jobsLabel.BackColor = Config.ColorPalette.White;
					jobsLabel.ForeColor = Config.ColorPalette.Black;
				}
			}
		}

		public override ScrollableControl ViewPanel => jobsViewPanel;

		public override Control GetControl()
		{
			return this;
		}
	}
}
