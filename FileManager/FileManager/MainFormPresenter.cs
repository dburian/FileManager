using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace FileManager
{
	class MainFormPresenter
	{
		IPanePresenter _leftPresenter;
		IPanePresenter _rightPresenter;
		bool _commandPromptInFocus;
		PaneArea _paneInFocusArea;

		IMainForm form;

		public MainFormPresenter(IMainForm mainForm)
		{
			form = mainForm;

			LeftPresenter = Config.DefaultLeftPanePresenter;
			RightPresenter = Config.DefaultRightPanePresenter;

			PaneInFocusArea = PaneArea.Left;

			CommandPromptPresenter = new CommandPromptPresenter(form.CommandPrompt);
			CommandPromptPresenter.ProcessCommandEvent += ProcessCommand;

			form.ProcessKeyPressEvent += ProcessKeyPress;
		}


		// Panes
		IPanePresenter LeftPresenter
		{
			get => _leftPresenter;
			set
			{
				_leftPresenter = value;
				form.LeftPane = _leftPresenter.GetViewsControl();
			}
		}
		IPanePresenter RightPresenter
		{
			get => _rightPresenter;
			set
			{
				_rightPresenter = value;
				form.RightPane = _rightPresenter.GetViewsControl();
			}
		}

		// Command prompt
		CommandPromptPresenter CommandPromptPresenter { get; set; }

		// To switch focus
		bool CommandPromptInFocus
		{
			get => _commandPromptInFocus;
			set
			{
				_commandPromptInFocus = value;

				if (_commandPromptInFocus)
				{
					PanePresenterInFocus.SetFocusOnView(false);
					CommandPromptPresenter.SetFocusOnView(true);
				}
				else
				{
					CommandPromptPresenter.SetFocusOnView(false);
					PanePresenterInFocus.SetFocusOnView(true);
				}
			}
		}
		PaneArea PaneInFocusArea
		{
			get => _paneInFocusArea;
			set
			{
				PanePresenterInFocus.SetFocusOnView(false);
				_paneInFocusArea = value;
				PanePresenterInFocus.SetFocusOnView(true);
			}
		}

		// Helper properties
		IPanePresenter PanePresenterInFocus
		{
			get
			{
				switch (PaneInFocusArea)
				{
					case PaneArea.Left:
						return LeftPresenter;
					case PaneArea.Right:
						return RightPresenter;

					default:
						return LeftPresenter;
				}
			}
		}
		/// <summary>
		/// Presenter not in focus
		/// </summary>
		IPanePresenter PresenterOther
		{
			get
			{
				switch (PaneInFocusArea)
				{
					case PaneArea.Left:
						return RightPresenter;
					case PaneArea.Right:
						return LeftPresenter;
					default:
						return LeftPresenter;
				}
			}
		}



		/// <summary>
		/// Causes to switch focus from the pane currently in focus to that specified by argument area.
		/// </summary>
		/// <param name="area">Area of the pane to which the focus is swithed.</param>
		void SwitchPaneInFocus(PaneArea area) => PaneInFocusArea = area;

		/// <summary>
		/// Causes to switch focus to the next pane.
		/// </summary>
		void SwitchPaneInFocus() => SwitchPaneInFocus(PaneInFocusArea == PaneArea.Left ? PaneArea.Right : PaneArea.Left);
		void SwitchFocusToCommandPrompt()
		{
			//TODO
		}
		bool ProcessKeyPress(char keyChar)
		{
			if (CommandPromptPresenter.ProcessKeyPress(keyChar)) return true;
			if (PanePresenterInFocus.ProcessKeyPress(keyChar) || PresenterOther.ProcessKeyPress(keyChar)) return true;

			switch (keyChar)
			{
				case (char)Keys.Tab:
					SwitchPaneInFocus();
					return true;

				case ':':
					SwitchFocusToCommandPrompt();
					return true;

				case (char)Keys.Escape:

				default:
					return false;
			}
		}
		bool ProcessCommand(ICommand cmd)
		{
			throw new NotImplementedException();
			//TODO
		}
		void StartSearch()
		{
			//Call to lib
			throw new NotImplementedException();
		}
		void StartCopy()
		{
			//Call to lib
			throw new NotImplementedException();
		}
		void StartMove()
		{
			//Call to lib
			throw new NotImplementedException();
		}
		void StartDelete()
		{
			//Call to lib
			throw new NotImplementedException();
		}
	}

}
