namespace FileManager
{
	partial class JobsPane
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
			this.jobsViewPanel = new System.Windows.Forms.Panel();
			this.underlinePanel1 = new FileManager.UnderlinePanel();
			this.jobEntryHeader1 = new FileManager.JobEntryHeader();
			this.bottomPanel = new System.Windows.Forms.Panel();
			this.bottomInnerTablePanel = new System.Windows.Forms.TableLayoutPanel();
			this.jobsInProgressLabel = new System.Windows.Forms.Label();
			this.jobsQueuedLabel = new System.Windows.Forms.Label();
			this.jobsSelectedLabel = new System.Windows.Forms.Label();
			this.jobsLabel = new System.Windows.Forms.Label();
			this.mainPanel.SuspendLayout();
			this.bottomPanel.SuspendLayout();
			this.bottomInnerTablePanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainPanel
			// 
			this.mainPanel.BackColor = System.Drawing.Color.White;
			this.mainPanel.Controls.Add(this.jobsViewPanel);
			this.mainPanel.Controls.Add(this.underlinePanel1);
			this.mainPanel.Controls.Add(this.jobEntryHeader1);
			this.mainPanel.Controls.Add(this.bottomPanel);
			this.mainPanel.Controls.Add(this.jobsLabel);
			this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainPanel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.mainPanel.Location = new System.Drawing.Point(5, 5);
			this.mainPanel.Name = "mainPanel";
			this.mainPanel.Size = new System.Drawing.Size(320, 482);
			this.mainPanel.TabIndex = 0;
			// 
			// jobsViewPanel
			// 
			this.jobsViewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.jobsViewPanel.Location = new System.Drawing.Point(0, 49);
			this.jobsViewPanel.Margin = new System.Windows.Forms.Padding(0);
			this.jobsViewPanel.Name = "jobsViewPanel";
			this.jobsViewPanel.Size = new System.Drawing.Size(320, 399);
			this.jobsViewPanel.TabIndex = 6;
			// 
			// underlinePanel1
			// 
			this.underlinePanel1.AutoSize = true;
			this.underlinePanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.underlinePanel1.Location = new System.Drawing.Point(0, 48);
			this.underlinePanel1.Margin = new System.Windows.Forms.Padding(0);
			this.underlinePanel1.MaximumSize = new System.Drawing.Size(0, 1);
			this.underlinePanel1.MinimumSize = new System.Drawing.Size(0, 1);
			this.underlinePanel1.Name = "underlinePanel1";
			this.underlinePanel1.Size = new System.Drawing.Size(320, 1);
			this.underlinePanel1.TabIndex = 5;
			// 
			// jobEntryHeader1
			// 
			this.jobEntryHeader1.BackColor = System.Drawing.Color.White;
			this.jobEntryHeader1.Dock = System.Windows.Forms.DockStyle.Top;
			this.jobEntryHeader1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.jobEntryHeader1.Location = new System.Drawing.Point(0, 23);
			this.jobEntryHeader1.Margin = new System.Windows.Forms.Padding(0);
			this.jobEntryHeader1.MaximumSize = new System.Drawing.Size(0, 25);
			this.jobEntryHeader1.MinimumSize = new System.Drawing.Size(0, 25);
			this.jobEntryHeader1.Name = "jobEntryHeader1";
			this.jobEntryHeader1.Size = new System.Drawing.Size(320, 25);
			this.jobEntryHeader1.TabIndex = 4;
			// 
			// bottomPanel
			// 
			this.bottomPanel.BackColor = System.Drawing.Color.White;
			this.bottomPanel.Controls.Add(this.bottomInnerTablePanel);
			this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.bottomPanel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.bottomPanel.Location = new System.Drawing.Point(0, 448);
			this.bottomPanel.Name = "bottomPanel";
			this.bottomPanel.Size = new System.Drawing.Size(320, 34);
			this.bottomPanel.TabIndex = 3;
			// 
			// bottomInnerTablePanel
			// 
			this.bottomInnerTablePanel.BackColor = System.Drawing.Color.White;
			this.bottomInnerTablePanel.ColumnCount = 3;
			this.bottomInnerTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.29742F));
			this.bottomInnerTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.40516F));
			this.bottomInnerTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.29742F));
			this.bottomInnerTablePanel.Controls.Add(this.jobsInProgressLabel, 0, 0);
			this.bottomInnerTablePanel.Controls.Add(this.jobsQueuedLabel, 1, 0);
			this.bottomInnerTablePanel.Controls.Add(this.jobsSelectedLabel, 2, 0);
			this.bottomInnerTablePanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bottomInnerTablePanel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.bottomInnerTablePanel.Location = new System.Drawing.Point(0, 0);
			this.bottomInnerTablePanel.Name = "bottomInnerTablePanel";
			this.bottomInnerTablePanel.RowCount = 1;
			this.bottomInnerTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.bottomInnerTablePanel.Size = new System.Drawing.Size(320, 34);
			this.bottomInnerTablePanel.TabIndex = 2;
			// 
			// jobsInProgressLabel
			// 
			this.jobsInProgressLabel.BackColor = System.Drawing.Color.White;
			this.jobsInProgressLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.jobsInProgressLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.jobsInProgressLabel.Location = new System.Drawing.Point(0, 0);
			this.jobsInProgressLabel.Margin = new System.Windows.Forms.Padding(0);
			this.jobsInProgressLabel.Name = "jobsInProgressLabel";
			this.jobsInProgressLabel.Size = new System.Drawing.Size(100, 34);
			this.jobsInProgressLabel.TabIndex = 0;
			this.jobsInProgressLabel.Text = "0 jobs in progress";
			this.jobsInProgressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// jobsQueuedLabel
			// 
			this.jobsQueuedLabel.BackColor = System.Drawing.Color.White;
			this.jobsQueuedLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.jobsQueuedLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.jobsQueuedLabel.Location = new System.Drawing.Point(100, 0);
			this.jobsQueuedLabel.Margin = new System.Windows.Forms.Padding(0);
			this.jobsQueuedLabel.Name = "jobsQueuedLabel";
			this.jobsQueuedLabel.Size = new System.Drawing.Size(119, 34);
			this.jobsQueuedLabel.TabIndex = 1;
			this.jobsQueuedLabel.Text = "0 jobs queued";
			this.jobsQueuedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// jobsSelectedLabel
			// 
			this.jobsSelectedLabel.AutoSize = true;
			this.jobsSelectedLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.jobsSelectedLabel.Location = new System.Drawing.Point(219, 0);
			this.jobsSelectedLabel.Margin = new System.Windows.Forms.Padding(0);
			this.jobsSelectedLabel.Name = "jobsSelectedLabel";
			this.jobsSelectedLabel.Size = new System.Drawing.Size(101, 34);
			this.jobsSelectedLabel.TabIndex = 2;
			this.jobsSelectedLabel.Text = "0 jobs selected";
			this.jobsSelectedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// jobsLabel
			// 
			this.jobsLabel.BackColor = System.Drawing.Color.White;
			this.jobsLabel.Dock = System.Windows.Forms.DockStyle.Top;
			this.jobsLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.jobsLabel.Location = new System.Drawing.Point(0, 0);
			this.jobsLabel.Margin = new System.Windows.Forms.Padding(0);
			this.jobsLabel.Name = "jobsLabel";
			this.jobsLabel.Size = new System.Drawing.Size(320, 32);
			this.jobsLabel.TabIndex = 0;
			this.jobsLabel.Text = "Jobs queue";
			this.jobsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// JobsPane
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.mainPanel);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.MinimumSize = new System.Drawing.Size(330, 0);
			this.Name = "JobsPane";
			this.Padding = new System.Windows.Forms.Padding(5);
			this.Size = new System.Drawing.Size(330, 492);
			this.mainPanel.ResumeLayout(false);
			this.mainPanel.PerformLayout();
			this.bottomPanel.ResumeLayout(false);
			this.bottomInnerTablePanel.ResumeLayout(false);
			this.bottomInnerTablePanel.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel mainPanel;
		private System.Windows.Forms.TableLayoutPanel bottomInnerTablePanel;
		private System.Windows.Forms.Label jobsLabel;
		private System.Windows.Forms.Panel bottomPanel;
		private System.Windows.Forms.Label jobsInProgressLabel;
		private System.Windows.Forms.Label jobsQueuedLabel;
		private System.Windows.Forms.Panel jobsViewPanel;
		private UnderlinePanel underlinePanel1;
		private JobEntryHeader jobEntryHeader1;
		private System.Windows.Forms.Label jobsSelectedLabel;
	}
}
