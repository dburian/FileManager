using MultithreadedFileSystemOperations;
using System;
using System.Diagnostics;

namespace FileManager
{
	public partial class JobEntry : AbstractEntry
	{
		private IJobArgsView _args;
		private JobTypeDescription _type;
		private float _progress;
		private JobStatus _status;
		private FileOperationException _exception;
		private readonly IJobView _jobView;

		public JobEntry(IJobView jobView, JobTypeDescription typeDescription)
		{
			InitializeComponent();

			_jobView = jobView;

			EntryType = typeDescription;
			EntryProgress = jobView.Progress;
			EntryException = jobView.Exception;
			Status = jobView.LastStatus;
			JobId = jobView.Id;

			switch (jobView.Type)
			{
				case JobType.FileTransfer:
					EntryArgs = new TransferArgsView(jobView.GetArgumentsView().FileTransferArguments);
					break;

				case JobType.Delete:
					EntryArgs = new DelArgsView(jobView.GetArgumentsView().DeleteArguments);
					break;

				case JobType.DirTransfer:
					EntryArgs = new TransferArgsView(jobView.GetArgumentsView().DirectoryTransferArguments);
					break;

				default:
					throw new ArgumentOutOfRangeException("JobEntry.ctor: new type of job?");
			}
		}


		public JobTypeDescription EntryType
		{
			get => _type;
			private set
			{
				_type = value;
				jobTypeLabel.Text = _type.ToString().ToUpper();
			}
		}
		public IJobView JobView => _jobView;
		public IJobArgsView EntryArgs
		{
			get => _args;
			set
			{
				if (_args != null)
				{
					jobEntryTablePanel.Controls.Remove(_args.GetControl());
				}

				_args = value;
				jobEntryTablePanel.Controls.Add(_args.GetControl(), 1, 0);
			}
		}
		public float EntryProgress
		{
			get => _progress;
			set
			{
				_progress = value;

				jobProgressLabel.Text = $"{_progress:0.00} %";
			}
		}
		public FileOperationException EntryException
		{
			get => _exception;
			set => _exception = value;
		}
		public int JobId { get; }
		public JobStatus Status
		{
			get => _status;
			set
			{
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
			jobProgressLabel.BackColor = BackColor;

			EntryArgs.SetBackgroundColor(BackColor);
		}
	}
}
