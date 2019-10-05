using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager
{
	public partial class CommandPrompt : UserControl, ICommandPrompt
	{
		bool _inFocus;

		public CommandPrompt()
		{
			InitializeComponent();
		}

		int ICommandPrompt.Width { get => commandLabel.Width - commandLabel.Padding.Left - commandLabel.Padding.Right; }
		Font ICommandPrompt.Font { get => commandLabel.Font; }

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
