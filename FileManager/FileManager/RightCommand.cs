using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
	struct RightCommand : ICommand
	{
		Panes pane;

		public RightCommand(Panes pane)
		{
			this.pane = pane;
		}
	}
}
