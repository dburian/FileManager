using System;
using System.Windows.Forms;

namespace FileManager
{
	public partial class ErrorMessage : UserControl, IErrorMessage
	{
		public ErrorMessage()
		{
			InitializeComponent();
		}

		public event EventHandler OkButtonClicked;

		public string ErrorType
		{
			get => errorTypeLabel.Text;
			set => errorTypeLabel.Text = value;
		}
		public string ErrorDetail
		{
			get => errorDetailLabel.Text;
			set => errorDetailLabel.Text = value;
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			OkButtonClicked?.Invoke(sender, e);
		}

		public Control GetControl()
		{
			return this;
		}
	}
}
