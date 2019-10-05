using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager
{
	public partial class JobsPane : EntriesPane<JobEntry>, IJobsPane
	{
		int _inProgress;
		int _queued;
		int _selected;
		bool _inFocus;

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

		public int JobsQueued {
			get => _queued;
			set
			{
				_queued = value;

				jobsQueuedLabel.Text = $"{_queued} jobs in queue";
			}
		}
		public int JobsSelected {
			get => _selected;
			set
			{
				_selected = value;

				jobsSelectedLabel.Text = $"{_selected} jobs selected";
			}
		}


		public override bool InFocus {
			get => _inFocus;
			set
			{
				_inFocus = value;

				if(_inFocus)
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

		public override Control GetControl() => this;

	}
}
