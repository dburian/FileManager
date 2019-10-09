using System;
using System.Windows.Forms;

namespace FileManager
{
	/// <summary>
	/// Controls the ErrorForm through IErrorForm interface.
	/// </summary>
	internal class ErrorFormPresenter
	{
		private readonly IErrorForm form;
		private readonly IErrorMessage errorMessage;

		public ErrorFormPresenter(IErrorForm form, IErrorMessage errorMessage)
		{
			this.form = form;
			this.errorMessage = errorMessage;

			form.ErrorMessage = errorMessage.GetControl();
			form.Title = errorMessage.ErrorType;

			errorMessage.OkButtonClicked += ProcessOkClick;
		}

		public DialogResult ShowAsDialog()
		{
			return form.ShowDialog();
		}

		private void ProcessOkClick(object sender, EventArgs e)
		{
			form.Close();
		}
	}
}
