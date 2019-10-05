using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
	class FilesPaneWasNotActiveException : Exception, ICommandException
	{
		ICommand cmd;

		public FilesPaneWasNotActiveException(ICommand cmd)
		{
			this.cmd = cmd;
		}

		public override string Message => $"{cmd} can be run only on an active files pane.";

		public string Type => $"Files pane wasn't active";
	}
}
