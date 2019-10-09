using System.Windows.Forms;

namespace FileManager
{
	/// <summary>
	/// Base class of all displayable entry types.
	/// </summary>
	public abstract partial class AbstractEntry : UserControl
	{
		private bool _highlighted;
		private bool _inFocus;

		public AbstractEntry()
		{
			InitializeComponent();
		}

		public virtual bool InFocus
		{
			get => _inFocus;
			set
			{
				_inFocus = value;
				if (_inFocus)
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
			}
		}

		public virtual bool Highlighted
		{
			get => _highlighted;
			set
			{
				_highlighted = value;
				if (_highlighted)
				{
					BackColor = Config.ColorPalette.HighlightedLight; //DarkStyle ? Config.ColorPalette.HighlightedDark : 
				}
				else
				{
					BackColor = Config.ColorPalette.White; // DarkStyle ? Config.ColorPalette.Grey : 
				}

				UpdateBackgroundColor();
			}
		}
		protected abstract void UpdateBackgroundColor();
	}
}
