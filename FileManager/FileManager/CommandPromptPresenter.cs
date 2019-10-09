using System.Windows.Forms;

namespace FileManager
{
	/// <summary>
	/// Controls command prompt through ICommandPrompt interface.
	/// </summary>
	internal class CommandPromptPresenter
	{
		private bool _enteringCommand;
		private readonly ICommandPrompt commandPrompt;

		public CommandPromptPresenter(ICommandPrompt commandPrompt)
		{
			this.commandPrompt = commandPrompt;
			commandPrompt.InFocus = false;
			commandPrompt.Command = Config.DefaultCommandPrompt;
		}

		public event ProcessCommandDelegate ProcessCommand;

		private bool EnteringCommand
		{
			get => _enteringCommand;
			set
			{
				_enteringCommand = value;
				if (_enteringCommand)
				{
					commandPrompt.Command = "";
				}
				else
				{
					commandPrompt.Command = Config.DefaultCommandPrompt;
				}
			}
		}

		/// <summary>
		/// Processes key press.
		/// </summary>
		/// <param name="keyPressed">Pressed key</param>
		/// <returns>True if the event was handled, false otherwise.</returns>
		public bool ProcessKeyPress(InputKey keyPressed)
		{
			if (!EnteringCommand)
			{
				return false;
			}

			if (commandPrompt.Command.Length == 0 && (keyPressed == '/' || keyPressed == ':'))
			{
				//Not catching the event, but acting on it
				EnteringCommand = true;
				commandPrompt.Command = (string)keyPressed;

				return false;
			}
			if (keyPressed == Keys.Return)
			{
				ProcessCommand?.Invoke(CreateCommand());
				return true;
			}

			if (keyPressed == Keys.Back)
			{
				if (commandPrompt.Command == "/")
				{
					commandPrompt.Command = ":";
				}
				else if (commandPrompt.Command.Length > 1)
				{
					commandPrompt.Command = commandPrompt.Command.Substring(0, commandPrompt.Command.Length - 1);
				}

				return true;
			}

			if (keyPressed == Keys.Escape)
			{
				//Not processing key press, but acting on it
				EnteringCommand = false;
				return false;
			}


			if (keyPressed.IsChar && CommandLenghtCheck(keyPressed.Character))
			{
				if (commandPrompt.Command == ":" && keyPressed == '/')
				{
					commandPrompt.Command = "/";
				}
				else
				{
					commandPrompt.Command += keyPressed.Character;
				}

				return true;
			}

			if (keyPressed == Keys.Space && CommandLenghtCheck(' '))
			{
				commandPrompt.Command += ' ';
				return true;
			}

			//"Processing" every keystroke when entering command...
			return true;
		}

		/// <summary>
		/// Sets focus on underlying CommandPrompt
		/// </summary>
		/// <param name="inFocus"></param>
		public void SetFocusOnView(bool inFocus)
		{
			EnteringCommand = inFocus;
			commandPrompt.InFocus = inFocus;
		}

		public void ResetCommandPrompt()
		{
			EnteringCommand = false;
		}

		private ICommand CreateCommand()
		{
			var command = commandPrompt.Command.StartsWith(":") ? commandPrompt.Command.Substring(1) : "/ " + commandPrompt.Command.Substring(1);

			foreach (var cmdFactory in RegisteredCommands.GetCommandFactories())
			{
				if (cmdFactory.Parse(command, out ICommand parsedCmd))
				{
					return parsedCmd;
				}
			}

			return RegisteredCommands.GetUnknownCommand(commandPrompt.Command.Substring(1));
		}

		private bool CommandLenghtCheck(char c)
		{
			return TextRenderer.MeasureText(commandPrompt.Command + c, commandPrompt.Font).Width < commandPrompt.Width;
		}
	}
}
