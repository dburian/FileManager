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
	public abstract partial class AbstractEntry : UserControl
	{
		bool _inFocus;
		public virtual bool InFocus
		{
			get { return _inFocus; }
			set
			{
				if (value)
				{
					SuspendLayout();
					BorderStyle = BorderStyle.FixedSingle;
					Padding = new Padding(0);
					ResumeLayout();
				}
				else
				{
					SuspendLayout();
					BorderStyle = BorderStyle.None;
					Padding = new Padding(1);
					ResumeLayout();
				}

				_inFocus = value;
			}
		}

		bool _highlighted;
		public virtual bool Highlighted
		{
			get { return _highlighted; }
			set
			{
				if (value)
					BackColor = DarkStyle ? Config.ColorPalette.HighlightedDark : Config.ColorPalette.HighlightedLight;
				else
					BackColor = DarkStyle ? Config.ColorPalette.Grey : Config.ColorPalette.White;

				UpdateBackgroundColor();

				_highlighted = value;
			}
		}

		bool _darkStyle;
		public virtual bool DarkStyle
		{
			get { return _darkStyle; }
			set
			{
				if (value)
					BackColor = Highlighted ? Config.ColorPalette.HighlightedDark : Config.ColorPalette.Grey;
				else
					BackColor = Highlighted ? Config.ColorPalette.HighlightedLight : Config.ColorPalette.White;

				UpdateBackgroundColor();

				_darkStyle = value;
			}
		}
		public AbstractEntry()
		{
			InitializeComponent();
		}

		protected abstract void UpdateBackgroundColor();
	}
}
