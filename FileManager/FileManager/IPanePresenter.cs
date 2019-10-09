using System;
using System.Windows.Forms;

namespace FileManager
{
	internal interface IPanePresenter : IDisposable
	{
		Control GetViewsControl();
		void SetFocusOnView(bool inFocus);
		bool ProcessKeyPress(InputKey pressedKey);

		event ProcessCommandDelegate InvokeCommand;
	}
}
