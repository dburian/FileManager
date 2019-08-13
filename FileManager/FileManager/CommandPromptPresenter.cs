using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager
{
	class CommandPromptPresenter
	{
		ICommandPrompt commandPrompt;
		string enteredCommand;

		public CommandPromptPresenter(ICommandPrompt commandPrompt)
		{
			this.commandPrompt = commandPrompt;
		}

		public event ProcessCommand ProcessCommandEvent;

		bool EnteringCommand { get; set; }

		public bool ProcessKeyPress(char keyPressed)
		{
			if (!EnteringCommand) return false;

			switch (keyPressed)
			{
				case (char)Keys.Return:
					ProcessCommandEvent(CreateCommand());
					return true;

				case (char)Keys.Escape:


				default:
					enteredCommand += keyPressed;
					return true;
			}
			
		}

		public void SetFocusOnView(bool inFocus)
		{
			EnteringCommand = inFocus;
			commandPrompt.InFocus = inFocus;
		}

		ICommand CreateCommand()
		{

			foreach(var cmdFactory in RegisteredCommands.GetCommandFactories())
			{
				if (cmdFactory.Parse(enteredCommand)) return cmdFactory.GetCommandInstance();
			}

			return RegisteredCommands.GetUnknownCommand(enteredCommand);
		}
	}
}
