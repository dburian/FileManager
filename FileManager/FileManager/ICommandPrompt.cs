using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FileManager
{
	public interface ICommandPrompt
	{
		string Command { get; set; }
		bool InFocus { get; set; }

		Font Font { get; }
		int Width { get; }
	}
}
