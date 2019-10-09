using System;

namespace FileManager
{
	internal class BothPanesNeedToBeFilesPaneException : Exception, ICommandException
	{
		private readonly ICommand cmd;
		public BothPanesNeedToBeFilesPaneException(ICommand cmd)
		{
			this.cmd = cmd;
		}

		public override string Message => $"{cmd} can be run only if both of the panes are files pane.";

		public string Type => "Both panes need to be file panes";
	}
}
