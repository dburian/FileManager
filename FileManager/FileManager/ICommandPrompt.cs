using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
	public interface ICommandPrompt
	{
		string Command { get; set; }
		bool InFocus { get; set; }
	}
}
