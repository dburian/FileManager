using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using MultithreadedFileOperations;
using System.Diagnostics;

namespace FileManager
{
	public partial class JobEntry : AbstractEntry
	{
		IJobArgsView _args;
		JobType _type;
		float _progress;
		JobStatus _status;
		FileOperationException _exception;

		public JobEntry(IJobView jobView)
		{
			InitializeComponent();

			Debug.WriteLine($"Created: {Thread.CurrentThread.ManagedThreadId}");

			EntryType = jobView.Type;
			EntryProgress = jobView.Progress;
			EntryException = jobView.Exception;
			JobId = jobView.Id;

			switch (EntryType)
			{
				case JobType.FileCopy:
				case JobType.FileMove:
					EntryArgs = new CopyMoveArgsView(jobView.GetArgumentsView().FileTransferArguments);
					break;

				case JobType.Delete:
					EntryArgs = new DelArgsView(jobView.GetArgumentsView().DeleteArguments);
					break;

				case JobType.DirCopy:
				case JobType.DirMove:
					EntryArgs = new CopyMoveArgsView(jobView.GetArgumentsView().DirectoryTransferArguments);
					break;

				default:
					throw new ArgumentOutOfRangeException("JobEntry.ctor: new type of job?");
			}
		}

		
		public JobType EntryType {
			get => _type;
			private set
			{
				Debug.WriteLine($"Modified: {Thread.CurrentThread.ManagedThreadId}");

				_type = value;
				jobTypeLabel.Text = _type.ToString().ToUpper();
			}
		}
		public IJobArgsView EntryArgs
		{
			get => _args;
			set
			{
				Debug.WriteLine($"Modified: {Thread.CurrentThread.ManagedThreadId}");


				if (_args != null) jobEntryTablePanel.Controls.Remove(_args.GetControl());
				_args = value;
				jobEntryTablePanel.Controls.Add(_args.GetControl(), 1, 0);
			}
		}
		public float EntryProgress {
			get => _progress;
			set
			{
				Debug.WriteLine($"Modified: {Thread.CurrentThread.ManagedThreadId}");

				_progress = value;
				jobProgressLabel.Text = $"{_progress} %";
			}
		}
		public FileOperationException EntryException
		{
			get => _exception;
			set
			{
				Debug.WriteLine($"Modified: {Thread.CurrentThread.ManagedThreadId}");

				_exception = value;
			}
		}
		public int JobId { get; private set; }
		public JobStatus Status
		{
			get => _status;
			set
			{
				Debug.WriteLine($"Modified: {Thread.CurrentThread.ManagedThreadId}");

				_status = value;
				jobStatusLabel.Text = _status.ToString().ToUpper();
			}
		}
		public bool Locked { get; set; }

		protected override void UpdateBackgroundColor()
		{
			jobEntryTablePanel.BackColor = BackColor;
			jobStatusLabel.BackColor = BackColor;
			jobTypeLabel.BackColor = BackColor;

			EntryArgs.GetControl().BackColor = BackColor;
		}
	}
}
