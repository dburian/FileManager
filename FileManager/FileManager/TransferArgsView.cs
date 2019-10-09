using MultithreadedFileOperations;
using System.Windows.Forms;

namespace FileManager
{
	public partial class TransferArgsView : UserControl, IJobArgsView
	{
		private ITransferJobArguments _args;

		public TransferArgsView()
		{
			//Dummy init for VS
			InitializeComponent();

			fromArgLabel.Text = "Source file";
			toArgLabel.Text = "Destination file";
		}

		public TransferArgsView(ITransferJobArguments args)
		{
			InitializeComponent();

			Args = args;
		}

		public ITransferJobArguments Args
		{
			get => _args;
			set
			{
				_args = value;

				fromArgLabel.Text = Args.From.FullName;
				toArgLabel.Text = Args.To.FullName;
			}
		}
		public Control GetControl()
		{
			return this;
		}
	}
}
