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
	public abstract partial class JobEntry : AbstractEntry
	{
		
		public JobEntry()
		{
			InitializeComponent();
		}

		public JobType EntryType { get; }
		public double Progress { get; }

		public void SetProgress(double progress)
		{
			jobStatusLabel.Text = $"{progress} %"; 
		}

		protected override void UpdateBackgroundColor()
		{
			jobEntryTablePanel.BackColor = BackColor;
			jobStatusLabel.BackColor = BackColor;
			jobTypeLabel.BackColor = BackColor;
		}
	}
}
