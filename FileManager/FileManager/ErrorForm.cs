using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager
{
	public partial class ErrorForm : Form, IErrorForm
	{
		Control _errorMessage;

		public ErrorForm()
		{
			InitializeComponent();
		}

		public Control ErrorMessage
		{
			get => _errorMessage;
			set {
				_errorMessage = value;
				mainPanel.Controls.Clear();
				mainPanel.Controls.Add(_errorMessage);
				_errorMessage.Dock = DockStyle.Fill;
			} 
		}

		public string Title
		{
			get => Text;
			set => Text = value;
		}

		public new DialogResult ShowDialog()
		{
			this.CenterToScreen();
			return base.ShowDialog();
		}
	}
}
