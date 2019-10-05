using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace FileManager
{
	public partial class MainForm : Form, IMainForm
	{
		public MainForm()
		{
			InitializeComponent();
		}

		public event ProcessKeyPress ProcessKeyPressEvent;

		public Control LeftPane
		{
			get => splitContainer.Panel1.Controls.Count > 0 ? splitContainer.Panel1.Controls[0] : null;
			set
			{
				splitContainer.Panel1.Controls.Clear();
				splitContainer.Panel1.Controls.Add(value);
				value.Dock = DockStyle.Fill;
			}
		}
		public Control RightPane
		{
			get => splitContainer.Panel2.Controls.Count > 0 ? splitContainer.Panel2.Controls[0] : null;
			set
			{
				splitContainer.Panel2.Controls.Clear();
				splitContainer.Panel2.Controls.Add(value);
				value.Dock = DockStyle.Fill;
			}
		}
		public ICommandPrompt CommandPrompt
		{
			get => commandPrompt;
		}
		public bool FullScreenRight {
			get => splitContainer.Panel1Collapsed;
			set => splitContainer.Panel1Collapsed = value; }
		public bool FullScreenLeft {
			get => splitContainer.Panel2Collapsed;
			set => splitContainer.Panel2Collapsed = value; }



		/// <summary>
		/// Handler for keys such as arrow keys, tab, return...
		/// </summary>
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			switch (keyData)
			{
				case Keys.Back:
				case Keys.Tab:
				case Keys.Return:
				case Keys.Escape:
				case Keys.Space:
				case Keys.PageUp:
				case Keys.PageDown:
				case Keys.End:
				case Keys.Home:
				case Keys.Left:
				case Keys.Up:
				case Keys.Right:
				case Keys.Down:
				case Keys.Insert:
				case Keys.Delete:
				case Keys.F1:
				case Keys.F2:
				case Keys.F3:
				case Keys.F4:
				case Keys.F5:
				case Keys.F6:
				case Keys.F7:
				case Keys.F8:
				case Keys.F9:
				case Keys.F10:
				case Keys.F11:
				case Keys.F12:
				case Keys.F13:
				case Keys.F14:
				case Keys.F15:
				case Keys.F16:
				case Keys.F17:
				case Keys.F18:
				case Keys.F19:
				case Keys.F20:
				case Keys.F21:
				case Keys.F22:
				case Keys.F23:
				case Keys.F24:
				case Keys.Shift:
				case Keys.Control:
				case Keys.Alt:
					if (ProcessKeyPressEvent?.Invoke(new InputKey(keyData)) == true) return true;
					break;

				default:
					break;
			}

			return base.ProcessCmdKey(ref msg, keyData);
		}
		/// <summary>
		/// Handler for general key press, should be used for character keys.
		/// </summary>
		void ProcessKeyPress(object sender, KeyPressEventArgs e)
		{
			if (ProcessKeyPressEvent?.Invoke(new InputKey(e.KeyChar)) == true) e.Handled = true;
		}
	}
}
