using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager
{
	interface IMainForm
	{
		event ProcessKeyPress ProcessKeyPressEvent;

		Control LeftPane { get; set; }
		Control RightPane { get; set; }
		ICommandPrompt CommandPrompt { get; }

		bool FullScreenRight { get; set; }
		bool FullScreenLeft { get; set; }
	}
}
