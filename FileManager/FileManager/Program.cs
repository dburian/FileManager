using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			var mf = new MainForm();
			MainFormPresenter mfPresenter = new MainFormPresenter(mf);

			Application.Run(mf);
		}
	}
}
