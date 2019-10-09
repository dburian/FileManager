using System;
using System.Windows.Forms;

namespace FileManager
{
	/// <summary>
	/// Most basic and general interface to a pane presenter.
	/// </summary>
	internal interface IPanePresenter : IDisposable
	{
		Control GetViewsControl();
		void SetFocusOnView(bool inFocus);
		bool ProcessKeyPress(InputKey pressedKey);

		event ProcessCommandDelegate InvokeCommand;
	}
}
