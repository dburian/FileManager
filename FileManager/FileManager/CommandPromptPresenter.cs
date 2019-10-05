using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace FileManager
{
	class CommandPromptPresenter
	{
		bool _enteringCommand;

		ICommandPrompt commandPrompt;

		public CommandPromptPresenter(ICommandPrompt commandPrompt)
		{
			this.commandPrompt = commandPrompt;
			commandPrompt.InFocus = false;
			commandPrompt.Command = Config.DefaultCommandPrompt;
		}

		public event ProcessCommandDelegate ProcessCommand;

		bool EnteringCommand
		{
			get => _enteringCommand;
			set {
				_enteringCommand = value;
				if (!_enteringCommand)
					commandPrompt.Command = Config.DefaultCommandPrompt;
			}
		}

		public bool ProcessKeyPress(InputKey keyPressed)
		{
			if (!EnteringCommand && (keyPressed == '/' || keyPressed == ':'))
			{
				//Not catching the event, but acting on it
				EnteringCommand = true;
				commandPrompt.Command = (string)keyPressed;

				return false;
			}

			if (!EnteringCommand) return false;

			if (keyPressed == Keys.Return)
			{
				ProcessCommand?.Invoke(CreateCommand());
				return true;
			}

			if (keyPressed == Keys.Back)
			{
				if (commandPrompt.Command == "/") commandPrompt.Command = ":";
				else if (commandPrompt.Command.Length > 1)
					commandPrompt.Command = commandPrompt.Command.Substring(0, commandPrompt.Command.Length - 1);
				return true;
			}

			if (keyPressed == Keys.Escape)
			{
				//Not processing key press, but acting on it
				EnteringCommand = false;
				return false;
			}
			

			if (keyPressed.IsChar() && CommandLenghtCheck(keyPressed._char))
			{
				if (commandPrompt.Command == ":" && keyPressed == '/') commandPrompt.Command = "/";
				else commandPrompt.Command += keyPressed._char;
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
		
		
		public void SetFocusOnView(bool inFocus)
		{
			EnteringCommand = inFocus;
			commandPrompt.InFocus = inFocus;
		}

		public void ResetCommandPrompt()
		{
			EnteringCommand = false;
		}

		ICommand CreateCommand()
		{
			var command = commandPrompt.Command.StartsWith(":") ? commandPrompt.Command.Substring(1) : "/ " + commandPrompt.Command.Substring(1);

			ICommand parsedCmd;
			foreach(var cmdFactory in RegisteredCommands.GetCommandFactories())
			{
				if (cmdFactory.Parse(command, out parsedCmd)) return parsedCmd;
			}

			return RegisteredCommands.GetUnknownCommand(commandPrompt.Command.Substring(1));
		}

		bool CommandLenghtCheck(char c)
		{
			return TextRenderer.MeasureText(commandPrompt.Command + c, commandPrompt.Font).Width < commandPrompt.Width;
		}
	}
}
