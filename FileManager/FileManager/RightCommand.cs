using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
	struct RightCommand : ICommand
	{
		public Panes Pane { get; private set; }

		public RightCommand(Panes pane)
		{
			Pane = pane;
		}
	}
}
