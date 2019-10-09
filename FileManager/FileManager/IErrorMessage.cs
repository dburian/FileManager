using System;
using System.Windows.Forms;

namespace FileManager
{
	public interface IErrorMessage
	{
		string ErrorType { get; set; }
		string ErrorDetail { get; set; }

		event EventHandler OkButtonClicked;
		Control GetControl();
	}
}
