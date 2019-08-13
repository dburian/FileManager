using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager
{
	interface IPanePresenter
	{
		Control GetViewsControl();
		void SetFocusOnView(bool inFocus);
		bool ProcessKeyPress(char keyChar);
	}
}
