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
		//bool _darkStyle = false;
		bool _highlighted;
		bool _inFocus;

		public AbstractEntry()
		{
			InitializeComponent();
		}

		public virtual bool InFocus
		{
			get { return _inFocus; }
			set
			{
				_inFocus = value;
				if (_inFocus)
				{
					SuspendLayout();
					BorderStyle = BorderStyle.FixedSingle;
					Padding = new Padding(0);
					ResumeLayout();
					//BackColor = Highlighted ? Config.ColorPalette.HighlightedDark : Config.ColorPalette.Black;
				}
				else
				{
					SuspendLayout();
					BorderStyle = BorderStyle.None;
					Padding = new Padding(1);
					ResumeLayout();

					//BackColor = Highlighted ? Config.ColorPalette.HighlightedLight : Config.ColorPalette.White;
				}

				//UpdateBackgroundColor();
			}
		}

		public virtual bool Highlighted
		{
			get { return _highlighted; }
			set
			{
				_highlighted = value;
				if (_highlighted)
					BackColor = Config.ColorPalette.HighlightedLight; //DarkStyle ? Config.ColorPalette.HighlightedDark : 
				else
					BackColor = Config.ColorPalette.White; // DarkStyle ? Config.ColorPalette.Grey : 

				UpdateBackgroundColor();
			}
		}

		//public virtual bool DarkStyle
		//{
		//	get { return _darkStyle; }
		//	set
		//	{
		//		//if (value)
		//		//	BackColor = Highlighted ? Config.ColorPalette.HighlightedDark : Config.ColorPalette.Grey;
		//		//else
		//		//	BackColor = Highlighted ? Config.ColorPalette.HighlightedLight : Config.ColorPalette.White;

		//		//UpdateBackgroundColor();

		//		//_darkStyle = value;
		//	}
		//}

		protected abstract void UpdateBackgroundColor();
	}
}
