using System;
using System.Windows.Forms;

namespace FileManager
{
	internal static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			var mf = new MainForm();
			MainFormPresenter mfPresenter = new MainFormPresenter(mf);

			Application.Run(mf);
		}
	}
}
