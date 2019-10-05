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
	public partial class CopyMoveArgsView : UserControl, IJobArgsView
	{
		ITransferJobArguments _args;

		public CopyMoveArgsView()
		{
			//Dummy init for VS
			InitializeComponent();

			fromArgLabel.Text = "Source file";
			toArgLabel.Text = "Destination file";
		}

		public CopyMoveArgsView(ITransferJobArguments args)
		{
			InitializeComponent();

			Debug.WriteLine($"Modified: {Thread.CurrentThread.ManagedThreadId}");
			Args = args;
		}

		public ITransferJobArguments Args
		{
			get => _args;
			set
			{
				Debug.WriteLine($"Modified: {Thread.CurrentThread.ManagedThreadId}");
				_args = value;

				fromArgLabel.Text = Args.From.FullName;
				toArgLabel.Text = Args.To.FullName;
			}
		}

		public Control GetControl() => this;
	}
}
