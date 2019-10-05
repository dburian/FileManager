namespace FileManager
{
	partial class CommandPrompt
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.mainPanel = new System.Windows.Forms.Panel();
			this.commandLabel = new System.Windows.Forms.Label();
			this.mainPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainPanel
			// 
			this.mainPanel.BackColor = System.Drawing.Color.White;
			this.mainPanel.Controls.Add(this.commandLabel);
			this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainPanel.Location = new System.Drawing.Point(0, 0);
			this.mainPanel.Margin = new System.Windows.Forms.Padding(0);
			this.mainPanel.Name = "mainPanel";
			this.mainPanel.Padding = new System.Windows.Forms.Padding(20, 8, 20, 8);
			this.mainPanel.Size = new System.Drawing.Size(481, 35);
			this.mainPanel.TabIndex = 0;
			// 
			// commandLabel
			// 
			this.commandLabel.BackColor = System.Drawing.Color.White;
			this.commandLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.commandLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.commandLabel.Location = new System.Drawing.Point(20, 8);
			this.commandLabel.Margin = new System.Windows.Forms.Padding(0);
			this.commandLabel.Name = "commandLabel";
			this.commandLabel.Size = new System.Drawing.Size(441, 19);
			this.commandLabel.TabIndex = 0;
			this.commandLabel.Text = "Type colon to enter a command";
			this.commandLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// CommandPrompt
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.mainPanel);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.MaximumSize = new System.Drawing.Size(0, 35);
			this.MinimumSize = new System.Drawing.Size(0, 35);
			this.Name = "CommandPrompt";
			this.Size = new System.Drawing.Size(481, 35);
			this.mainPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel mainPanel;
		private System.Windows.Forms.Label commandLabel;
	}
}
