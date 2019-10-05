using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager
{
	interface IPanePresenter : IDisposable
	{
		Control GetViewsControl();
		void SetFocusOnView(bool inFocus);
		bool ProcessKeyPress(InputKey pressedKey);

		event ProcessCommandDelegate ProcessComand;
	}
}
