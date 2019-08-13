namespace FileManager
{
	partial class UnderlinePanel
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
			this.blackPanel = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// blackPanel
			// 
			this.blackPanel.BackColor = System.Drawing.Color.Black;
			this.blackPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.blackPanel.Location = new System.Drawing.Point(0, 0);
			this.blackPanel.Margin = new System.Windows.Forms.Padding(0);
			this.blackPanel.MaximumSize = new System.Drawing.Size(0, 1);
			this.blackPanel.MinimumSize = new System.Drawing.Size(0, 1);
			this.blackPanel.Name = "blackPanel";
			this.blackPanel.Size = new System.Drawing.Size(363, 1);
			this.blackPanel.TabIndex = 2;
			// 
			// UnderlinePanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.Controls.Add(this.blackPanel);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.MaximumSize = new System.Drawing.Size(0, 1);
			this.MinimumSize = new System.Drawing.Size(0, 1);
			this.Name = "UnderlinePanel";
			this.Size = new System.Drawing.Size(363, 1);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel blackPanel;
	}
}
