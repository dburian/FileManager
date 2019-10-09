using System.Windows.Forms;

namespace FileManager
{
	/// <summary>
	/// View interface to MainForm.
	/// </summary>
	internal interface IMainForm
	{
		event ProcessKeyPress ProcessKeyPressEvent;
		event FormClosingEventHandler FormClosing;

		Control LeftPane { get; set; }
		Control RightPane { get; set; }
		ICommandPrompt CommandPrompt { get; }

		bool FullScreenRight { get; set; }
		bool FullScreenLeft { get; set; }
	}
}
