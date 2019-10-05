using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
	class BothPanesNeedToBeFilesPaneException : Exception, ICommandException
	{
		ICommand cmd;
		public BothPanesNeedToBeFilesPaneException(ICommand cmd)
		{
			this.cmd = cmd;
		}

		public override string Message => $"{cmd} can be run only if both of the panes are files pane.";

		public string Type => "Both panes need to be file panes";
	}
}
