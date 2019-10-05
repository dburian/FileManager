using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
	struct LeftCommand : ICommand
	{
		public Panes Pane { get; private set; }

		public LeftCommand(Panes pane)
		{
			 Pane = pane;
		}
	}
}
