namespace FileManager
{
	partial class FilesPane
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
			this.filesViewPanel = new System.Windows.Forms.Panel();
			this.underlinePanel1 = new FileManager.UnderlinePanel();
			this.fileEntryHeader1 = new FileManager.FileEntryHeader();
			this.bottomPanel = new System.Windows.Forms.Panel();
			this.bottomInnerTablePanel = new System.Windows.Forms.TableLayoutPanel();
			this.highlightedEntriesLabel = new System.Windows.Forms.Label();
			this.numberOfFilesLabel = new System.Windows.Forms.Label();
			this.freeSpaceLabel = new System.Windows.Forms.Label();
			this.currentDirectoryLabel = new System.Windows.Forms.Label();
			this.mainPanel.SuspendLayout();
			this.bottomPanel.SuspendLayout();
			this.bottomInnerTablePanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainPanel
			// 
			this.mainPanel.BackColor = System.Drawing.Color.White;
			this.mainPanel.Controls.Add(this.filesViewPanel);
			this.mainPanel.Controls.Add(this.underlinePanel1);
			this.mainPanel.Controls.Add(this.fileEntryHeader1);
			this.mainPanel.Controls.Add(this.bottomPanel);
			this.mainPanel.Controls.Add(this.currentDirectoryLabel);
			this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainPanel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.mainPanel.Location = new System.Drawing.Point(5, 5);
			this.mainPanel.Name = "mainPanel";
			this.mainPanel.Size = new System.Drawing.Size(395, 218);
			this.mainPanel.TabIndex = 0;
			// 
			// filesViewPanel
			// 
			this.filesViewPanel.AutoScroll = true;
			this.filesViewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.filesViewPanel.Location = new System.Drawing.Point(0, 62);
			this.filesViewPanel.Margin = new System.Windows.Forms.Padding(0);
			this.filesViewPanel.Name = "filesViewPanel";
			this.filesViewPanel.Size = new System.Drawing.Size(395, 122);
			this.filesViewPanel.TabIndex = 8;
			// 
			// underlinePanel1
			// 
			this.underlinePanel1.AutoSize = true;
			this.underlinePanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.underlinePanel1.Location = new System.Drawing.Point(0, 61);
			this.underlinePanel1.Margin = new System.Windows.Forms.Padding(0);
			this.underlinePanel1.MaximumSize = new System.Drawing.Size(0, 1);
			this.underlinePanel1.MinimumSize = new System.Drawing.Size(0, 1);
			this.underlinePanel1.Name = "underlinePanel1";
			this.underlinePanel1.Size = new System.Drawing.Size(395, 1);
			this.underlinePanel1.TabIndex = 7;
			// 
			// fileEntryHeader1
			// 
			this.fileEntryHeader1.Dock = System.Windows.Forms.DockStyle.Top;
			this.fileEntryHeader1.Location = new System.Drawing.Point(0, 32);
			this.fileEntryHeader1.Margin = new System.Windows.Forms.Padding(0);
			this.fileEntryHeader1.MaximumSize = new System.Drawing.Size(0, 29);
			this.fileEntryHeader1.MinimumSize = new System.Drawing.Size(380, 29);
			this.fileEntryHeader1.Name = "fileEntryHeader1";
			this.fileEntryHeader1.Size = new System.Drawing.Size(395, 29);
			this.fileEntryHeader1.TabIndex = 6;
			// 
			// bottomPanel
			// 
			this.bottomPanel.BackColor = System.Drawing.Color.White;
			this.bottomPanel.Controls.Add(this.bottomInnerTablePanel);
			this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.bottomPanel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.bottomPanel.Location = new System.Drawing.Point(0, 184);
			this.bottomPanel.Name = "bottomPanel";
			this.bottomPanel.Size = new System.Drawing.Size(395, 34);
			this.bottomPanel.TabIndex = 5;
			// 
			// bottomInnerTablePanel
			// 
			this.bottomInnerTablePanel.BackColor = System.Drawing.Color.White;
			this.bottomInnerTablePanel.ColumnCount = 3;
			this.bottomInnerTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.bottomInnerTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.bottomInnerTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.bottomInnerTablePanel.Controls.Add(this.highlightedEntriesLabel, 2, 0);
			this.bottomInnerTablePanel.Controls.Add(this.numberOfFilesLabel, 0, 0);
			this.bottomInnerTablePanel.Controls.Add(this.freeSpaceLabel, 0, 0);
			this.bottomInnerTablePanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bottomInnerTablePanel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.bottomInnerTablePanel.Location = new System.Drawing.Point(0, 0);
			this.bottomInnerTablePanel.Margin = new System.Windows.Forms.Padding(0);
			this.bottomInnerTablePanel.Name = "bottomInnerTablePanel";
			this.bottomInnerTablePanel.RowCount = 1;
			this.bottomInnerTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.bottomInnerTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
			this.bottomInnerTablePanel.Size = new System.Drawing.Size(395, 34);
			this.bottomInnerTablePanel.TabIndex = 0;
			// 
			// highlightedEntriesLabel
			// 
			this.highlightedEntriesLabel.BackColor = System.Drawing.Color.White;
			this.highlightedEntriesLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.highlightedEntriesLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.highlightedEntriesLabel.Location = new System.Drawing.Point(262, 0);
			this.highlightedEntriesLabel.Margin = new System.Windows.Forms.Padding(0);
			this.highlightedEntriesLabel.Name = "highlightedEntriesLabel";
			this.highlightedEntriesLabel.Size = new System.Drawing.Size(133, 34);
			this.highlightedEntriesLabel.TabIndex = 7;
			this.highlightedEntriesLabel.Text = "5 files - 1 directory / 1.1 GB";
			this.highlightedEntriesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// numberOfFilesLabel
			// 
			this.numberOfFilesLabel.BackColor = System.Drawing.Color.White;
			this.numberOfFilesLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.numberOfFilesLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.numberOfFilesLabel.Location = new System.Drawing.Point(131, 0);
			this.numberOfFilesLabel.Margin = new System.Windows.Forms.Padding(0);
			this.numberOfFilesLabel.Name = "numberOfFilesLabel";
			this.numberOfFilesLabel.Size = new System.Drawing.Size(131, 34);
			this.numberOfFilesLabel.TabIndex = 6;
			this.numberOfFilesLabel.Text = "22 files - 5 directories";
			this.numberOfFilesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// freeSpaceLabel
			// 
			this.freeSpaceLabel.BackColor = System.Drawing.Color.White;
			this.freeSpaceLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.freeSpaceLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.freeSpaceLabel.Location = new System.Drawing.Point(0, 0);
			this.freeSpaceLabel.Margin = new System.Windows.Forms.Padding(0);
			this.freeSpaceLabel.Name = "freeSpaceLabel";
			this.freeSpaceLabel.Size = new System.Drawing.Size(131, 34);
			this.freeSpaceLabel.TabIndex = 5;
			this.freeSpaceLabel.Text = "2.43 GB free";
			this.freeSpaceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// currentDirectoryLabel
			// 
			this.currentDirectoryLabel.BackColor = System.Drawing.Color.White;
			this.currentDirectoryLabel.Dock = System.Windows.Forms.DockStyle.Top;
			this.currentDirectoryLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.currentDirectoryLabel.Location = new System.Drawing.Point(0, 0);
			this.currentDirectoryLabel.Margin = new System.Windows.Forms.Padding(0);
			this.currentDirectoryLabel.Name = "currentDirectoryLabel";
			this.currentDirectoryLabel.Size = new System.Drawing.Size(395, 32);
			this.currentDirectoryLabel.TabIndex = 0;
			this.currentDirectoryLabel.Text = "C:/Users/David/Documents/Matfyz/3.1/DotNet/File\\ Manager";
			this.currentDirectoryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// FilesPane
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.mainPanel);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.MinimumSize = new System.Drawing.Size(405, 0);
			this.Name = "FilesPane";
			this.Padding = new System.Windows.Forms.Padding(5);
			this.Size = new System.Drawing.Size(405, 228);
			this.mainPanel.ResumeLayout(false);
			this.mainPanel.PerformLayout();
			this.bottomPanel.ResumeLayout(false);
			this.bottomInnerTablePanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel mainPanel;
		private System.Windows.Forms.Label currentDirectoryLabel;
		private System.Windows.Forms.Panel bottomPanel;
		private System.Windows.Forms.TableLayoutPanel bottomInnerTablePanel;
		private System.Windows.Forms.Label numberOfFilesLabel;
		private System.Windows.Forms.Label highlightedEntriesLabel;
		private System.Windows.Forms.Label freeSpaceLabel;
		private System.Windows.Forms.Panel filesViewPanel;
		private UnderlinePanel underlinePanel1;
		private FileEntryHeader fileEntryHeader1;
	}
}
