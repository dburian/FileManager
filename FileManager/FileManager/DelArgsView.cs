using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MultithreadedFileOperations;

namespace FileManager
{
	public partial class DelArgsView : UserControl, IJobArgsView
	{
		DeleteJobArguments _args;

		public DelArgsView()
		{
			//Dummy ctor for VS
			InitializeComponent();
			targetArgLabel.Text = "File to be deleted";
		}

		public DelArgsView(DeleteJobArguments args)
		{
			InitializeComponent();
			Args = args;
		}
		
		public DeleteJobArguments Args
		{
			get => _args;
			set
			{
				_args = value;

				targetArgLabel.Text = _args.Target.FullName;
			}
		}

		public Control GetControl() => this;
	}
}
