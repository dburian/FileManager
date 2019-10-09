using System.Drawing;
using System.Windows.Forms;

namespace FileManager
{
	/// <summary>
	/// Command prompt view.
	/// </summary>
	public partial class CommandPrompt : UserControl, ICommandPrompt
	{
		private bool _inFocus;

		public CommandPrompt()
		{
			InitializeComponent();
		}

		int ICommandPrompt.Width => commandLabel.Width - commandLabel.Padding.Left - commandLabel.Padding.Right;
		Font ICommandPrompt.Font => commandLabel.Font;

		public string Command
		{
			get => commandLabel.Text;
			set => commandLabel.Text = value;
		}
		public bool InFocus
		{
			get => _inFocus;
			set
			{
				_inFocus = value;

				if (_inFocus)
				{
					mainPanel.BackColor = Config.ColorPalette.Black;
					commandLabel.BackColor = Config.ColorPalette.Black;
					commandLabel.ForeColor = Config.ColorPalette.White;
					commandLabel.Font = new Font("Consolas", 12, FontStyle.Bold);
				}
				else
				{
					mainPanel.BackColor = Config.ColorPalette.White;
					commandLabel.BackColor = Config.ColorPalette.White;
					commandLabel.ForeColor = Config.ColorPalette.Black;
					commandLabel.Font = new Font("Consolas", 12, FontStyle.Regular);
				}
			}
		}
	}
}
