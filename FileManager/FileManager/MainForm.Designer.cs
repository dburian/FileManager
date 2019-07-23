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
			this.filesPane1 = new FileManager.FilesPane();
			this.SuspendLayout();
			// 
			// filesPane1
			// 
			this.filesPane1.AutoScroll = true;
			this.filesPane1.AutoSize = true;
			this.filesPane1.BackColor = System.Drawing.Color.Silver;
			this.filesPane1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.filesPane1.Location = new System.Drawing.Point(0, 0);
			this.filesPane1.Margin = new System.Windows.Forms.Padding(0);
			this.filesPane1.MinimumSize = new System.Drawing.Size(330, 0);
			this.filesPane1.Name = "filesPane1";
			this.filesPane1.Size = new System.Drawing.Size(666, 570);
			this.filesPane1.TabIndex = 0;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(666, 570);
			this.Controls.Add(this.filesPane1);
			this.Name = "MainForm";
			this.Text = "File Manager";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private FilesPane filesPane1;
	}
}

