using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager
{
	public interface IPane
	{
		bool InFocus { get; set; }
		ScrollableControl ScrollPanel { get; }
		Control GetControl();
	}
}
