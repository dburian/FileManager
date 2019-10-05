using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager
{
	interface IErrorForm
	{
		Control ErrorMessage { get; set; }
		string Title { get; set; }

		void Close();
		DialogResult ShowDialog();
	}
}
