using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager
{
	class ErrorFormPresenter
	{
		IErrorForm form;
		IErrorMessage errorMessage;

		public ErrorFormPresenter(IErrorForm form, IErrorMessage errorMessage)
		{
			this.form = form;
			this.errorMessage = errorMessage;

			form.ErrorMessage = errorMessage.GetControl();
			form.Title = errorMessage.ErrorType;

			errorMessage.OkButtonClicked += ProcessOkClick;
		}

		public DialogResult ShowAsDialog() => form.ShowDialog();

		void ProcessOkClick(object sender, EventArgs e) => form.Close();
		
	}
}
