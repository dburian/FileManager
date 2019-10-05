namespace FileManager
{
	partial class SearchResultPane
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
			this.searchResultsViewPanel = new System.Windows.Forms.Panel();
			this.searchingLabel = new System.Windows.Forms.Label();
			this.fileEntryHeader1 = new FileManager.FileEntryHeader();
			this.underlinePanel1 = new FileManager.UnderlinePanel();
			this.bottomPanel = new System.Windows.Forms.Panel();
			this.innerBottomTablePanel = new System.Windows.Forms.TableLayoutPanel();
			this.searchedDirectoryLabel = new System.Windows.Forms.Label();
			this.statusLabel = new System.Windows.Forms.Label();
			this.progressLabel = new System.Windows.Forms.Label();
			this.mainPanel.SuspendLayout();
			this.bottomPanel.SuspendLayout();
			this.innerBottomTablePanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainPanel
			// 
			this.mainPanel.Controls.Add(this.searchResultsViewPanel);
			this.mainPanel.Controls.Add(this.bottomPanel);
			this.mainPanel.Controls.Add(this.underlinePanel1);
			this.mainPanel.Controls.Add(this.fileEntryHeader1);
			this.mainPanel.Controls.Add(this.searchingLabel);
			this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainPanel.Location = new System.Drawing.Point(0, 0);
			this.mainPanel.Name = "mainPanel";
			this.mainPanel.Size = new System.Drawing.Size(389, 479);
			this.mainPanel.TabIndex = 0;
			// 
			// searchingLabel
			// 
			this.searchingLabel.Dock = System.Windows.Forms.DockStyle.Top;
			this.searchingLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.searchingLabel.Location = new System.Drawing.Point(0, 0);
			this.searchingLabel.Name = "searchingLabel";
			this.searchingLabel.Size = new System.Drawing.Size(389, 32);
			this.searchingLabel.TabIndex = 0;
			this.searchingLabel.Text = "Search results";
			this.searchingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// fileEntryHeader1
			// 
			this.fileEntryHeader1.Dock = System.Windows.Forms.DockStyle.Top;
			this.fileEntryHeader1.Location = new System.Drawing.Point(0, 23);
			this.fileEntryHeader1.Margin = new System.Windows.Forms.Padding(0);
			this.fileEntryHeader1.MaximumSize = new System.Drawing.Size(0, 29);
			this.fileEntryHeader1.MinimumSize = new System.Drawing.Size(380, 29);
			this.fileEntryHeader1.Name = "fileEntryHeader1";
			this.fileEntryHeader1.Size = new System.Drawing.Size(389, 29);
			this.fileEntryHeader1.TabIndex = 1;
			// 
			// underlinePanel1
			// 
			this.underlinePanel1.AutoSize = true;
			this.underlinePanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.underlinePanel1.Location = new System.Drawing.Point(0, 52);
			this.underlinePanel1.Margin = new System.Windows.Forms.Padding(0);
			this.underlinePanel1.MaximumSize = new System.Drawing.Size(0, 1);
			this.underlinePanel1.MinimumSize = new System.Drawing.Size(0, 1);
			this.underlinePanel1.Name = "underlinePanel1";
			this.underlinePanel1.Size = new System.Drawing.Size(389, 1);
			this.underlinePanel1.TabIndex = 2;
			// 
			// bottomPanel
			// 
			this.bottomPanel.Controls.Add(this.innerBottomTablePanel);
			this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.bottomPanel.Location = new System.Drawing.Point(0, 445);
			this.bottomPanel.Margin = new System.Windows.Forms.Padding(0);
			this.bottomPanel.Name = "bottomPanel";
			this.bottomPanel.Size = new System.Drawing.Size(389, 34);
			this.bottomPanel.TabIndex = 3;
			// 
			// innerBottomTablePanel
			// 
			this.innerBottomTablePanel.ColumnCount = 3;
			this.innerBottomTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.innerBottomTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.innerBottomTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.innerBottomTablePanel.Controls.Add(this.searchedDirectoryLabel, 0, 0);
			this.innerBottomTablePanel.Controls.Add(this.statusLabel, 1, 0);
			this.innerBottomTablePanel.Controls.Add(this.progressLabel, 2, 0);
			this.innerBottomTablePanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.innerBottomTablePanel.Location = new System.Drawing.Point(0, 0);
			this.innerBottomTablePanel.Margin = new System.Windows.Forms.Padding(0);
			this.innerBottomTablePanel.Name = "innerBottomTablePanel";
			this.innerBottomTablePanel.RowCount = 1;
			this.innerBottomTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.innerBottomTablePanel.Size = new System.Drawing.Size(389, 34);
			this.innerBottomTablePanel.TabIndex = 0;
			// 
			// searchedFolderLabel
			// 
			this.searchedDirectoryLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.searchedDirectoryLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.searchedDirectoryLabel.Location = new System.Drawing.Point(3, 0);
			this.searchedDirectoryLabel.Name = "searchedFolderLabel";
			this.searchedDirectoryLabel.Size = new System.Drawing.Size(183, 34);
			this.searchedDirectoryLabel.TabIndex = 0;
			this.searchedDirectoryLabel.Text = "C:\\Program Files\\Git";
			this.searchedDirectoryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// statusLabel
			// 
			this.statusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.statusLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.statusLabel.Location = new System.Drawing.Point(192, 0);
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Size = new System.Drawing.Size(94, 34);
			this.statusLabel.TabIndex = 1;
			this.statusLabel.Text = "Not started";
			this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// progressLabel
			// 
			this.progressLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.progressLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.progressLabel.Location = new System.Drawing.Point(292, 0);
			this.progressLabel.Name = "progressLabel";
			this.progressLabel.Size = new System.Drawing.Size(94, 34);
			this.progressLabel.TabIndex = 1;
			this.progressLabel.Text = "0 %";
			this.progressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// serachResultsViewPanel
			// 
			this.searchResultsViewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.searchResultsViewPanel.Location = new System.Drawing.Point(0, 53);
			this.searchResultsViewPanel.Name = "serachResultsViewPanel";
			this.searchResultsViewPanel.Size = new System.Drawing.Size(389, 392);
			this.searchResultsViewPanel.TabIndex = 4;
			this.searchResultsViewPanel.AutoScroll = true;
			// 
			// SearchResultPane
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.mainPanel);
			this.Name = "SearchResultPane";
			this.Size = new System.Drawing.Size(389, 479);
			this.Padding = new System.Windows.Forms.Padding(5);
			this.mainPanel.ResumeLayout(false);
			this.mainPanel.PerformLayout();
			this.bottomPanel.ResumeLayout(false);
			this.innerBottomTablePanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel mainPanel;
		private System.Windows.Forms.Label searchingLabel;
		private UnderlinePanel underlinePanel1;
		private FileEntryHeader fileEntryHeader1;
		private System.Windows.Forms.Panel bottomPanel;
		private System.Windows.Forms.TableLayoutPanel innerBottomTablePanel;
		private System.Windows.Forms.Label searchedDirectoryLabel;
		private System.Windows.Forms.Label statusLabel;
		private System.Windows.Forms.Label progressLabel;
		private System.Windows.Forms.Panel searchResultsViewPanel;
	}
}
