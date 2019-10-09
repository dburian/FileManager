using MultithreadedFileOperations;
using System.Windows.Forms;

namespace FileManager
{
	public partial class DelArgsView : UserControl, IJobArgsView
	{
		private DeleteJobArguments _args;

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

		public Control GetControl()
		{
			return this;
		}
	}
}
