namespace FileManager
{
	partial class MainForm
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.mainPanel = new System.Windows.Forms.Panel();
			this.commandPrompt = new FileManager.CommandPrompt();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
			this.splitContainer.SuspendLayout();
			this.mainPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer
			// 
			this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.splitContainer.Location = new System.Drawing.Point(0, 0);
			this.splitContainer.Margin = new System.Windows.Forms.Padding(0);
			this.splitContainer.Name = "splitContainer";
			// 
			// splitContainer.Panel1
			// 
			this.splitContainer.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.splitContainer.Panel1MinSize = 390;
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.splitContainer.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.splitContainer.Size = new System.Drawing.Size(1484, 463);
			this.splitContainer.SplitterDistance = 743;
			this.splitContainer.TabIndex = 0;
			// 
			// mainPanel
			// 
			this.mainPanel.Controls.Add(this.splitContainer);
			this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainPanel.Location = new System.Drawing.Point(0, 0);
			this.mainPanel.Name = "mainPanel";
			this.mainPanel.Size = new System.Drawing.Size(1484, 463);
			this.mainPanel.TabIndex = 2;
			// 
			// commandPrompt
			// 
			this.commandPrompt.Command = "Type colon to enter a command";
			this.commandPrompt.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.commandPrompt.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.commandPrompt.InFocus = false;
			this.commandPrompt.Location = new System.Drawing.Point(0, 463);
			this.commandPrompt.Margin = new System.Windows.Forms.Padding(0);
			this.commandPrompt.MaximumSize = new System.Drawing.Size(0, 35);
			this.commandPrompt.MinimumSize = new System.Drawing.Size(0, 35);
			this.commandPrompt.Name = "commandPrompt";
			this.commandPrompt.Size = new System.Drawing.Size(1484, 35);
			this.commandPrompt.TabIndex = 1;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(1484, 498);
			this.Controls.Add(this.mainPanel);
			this.Controls.Add(this.commandPrompt);
			this.DoubleBuffered = true;
			this.KeyPreview = true;
			this.MinimumSize = new System.Drawing.Size(836, 200);
			this.Name = "MainForm";
			this.Text = "File Manager";
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ProcessKeyPress);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
			this.splitContainer.ResumeLayout(false);
			this.mainPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private CommandPrompt commandPrompt;
		private System.Windows.Forms.SplitContainer splitContainer;
		private System.Windows.Forms.Panel mainPanel;
	}
}

