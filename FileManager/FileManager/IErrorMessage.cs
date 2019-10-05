using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
