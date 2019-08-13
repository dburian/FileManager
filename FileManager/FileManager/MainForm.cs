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



		/// <summary>
		/// Handler for keys such as arrow keys, tab, return...
		/// </summary>
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (!char.IsLetter((char)keyData) && ProcessKeyPressEvent?.Invoke((char)keyData) == true) return true;
			else return base.ProcessCmdKey(ref msg, keyData);
		}
		/// <summary>
		/// Handler for general key press, should be used for character keys.
		/// </summary>
		void ProcessKeyPress(object sender, KeyPressEventArgs e)
		{
			if (ProcessKeyPressEvent?.Invoke(e.KeyChar) == true) e.Handled = true;
		}
	}
}
