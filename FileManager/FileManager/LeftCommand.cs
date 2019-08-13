using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
	struct LeftCommand : ICommand
	{
		Panes pane;

		public LeftCommand(Panes pane)
		{
			this.pane = pane;
		}
	}
}
