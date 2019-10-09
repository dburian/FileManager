using System.Windows.Forms;

namespace FileManager
{
	internal interface IErrorForm
	{
		Control ErrorMessage { get; set; }
		string Title { get; set; }

		void Close();
		DialogResult ShowDialog();
	}
}
