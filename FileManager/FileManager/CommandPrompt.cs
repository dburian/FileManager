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
			InFocus = false;
		}

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
					mainPanel.BackColor = Color.Black;
				}
				else
				{
					mainPanel.BackColor = Color.White;
				}
			}
		}
	}
}
