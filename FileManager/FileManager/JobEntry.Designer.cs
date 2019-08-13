namespace FileManager
{
	partial class JobEntry
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
			this.jobEntryTablePanel = new System.Windows.Forms.TableLayoutPanel();
			this.jobStatusLabel = new System.Windows.Forms.Label();
			this.jobTypeLabel = new System.Windows.Forms.Label();
			this.jobEntryTablePanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// jobEntryTabelPanel
			// 
			this.jobEntryTablePanel.AutoSize = true;
			this.jobEntryTablePanel.BackColor = System.Drawing.Color.White;
			this.jobEntryTablePanel.ColumnCount = 3;
			this.jobEntryTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
			this.jobEntryTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.jobEntryTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
			this.jobEntryTablePanel.Controls.Add(this.jobStatusLabel, 2, 0);
			this.jobEntryTablePanel.Controls.Add(this.jobTypeLabel, 0, 0);
			this.jobEntryTablePanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.jobEntryTablePanel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.jobEntryTablePanel.Location = new System.Drawing.Point(0, 0);
			this.jobEntryTablePanel.Margin = new System.Windows.Forms.Padding(0);
			this.jobEntryTablePanel.Name = "jobEntryTabelPanel";
			this.jobEntryTablePanel.RowCount = 1;
			this.jobEntryTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.jobEntryTablePanel.Size = new System.Drawing.Size(693, 50);
			this.jobEntryTablePanel.TabIndex = 1;
			// 
			// jobStatusLabel
			// 
			this.jobStatusLabel.BackColor = System.Drawing.Color.White;
			this.jobStatusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.jobStatusLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.jobStatusLabel.Location = new System.Drawing.Point(633, 0);
			this.jobStatusLabel.Margin = new System.Windows.Forms.Padding(0);
			this.jobStatusLabel.Name = "jobStatusLabel";
			this.jobStatusLabel.Size = new System.Drawing.Size(60, 50);
			this.jobStatusLabel.TabIndex = 8;
			this.jobStatusLabel.Text = "99 %";
			this.jobStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// jobTypeLabel
			// 
			this.jobTypeLabel.BackColor = System.Drawing.Color.White;
			this.jobTypeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.jobTypeLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.jobTypeLabel.Location = new System.Drawing.Point(0, 0);
			this.jobTypeLabel.Margin = new System.Windows.Forms.Padding(0);
			this.jobTypeLabel.Name = "jobTypeLabel";
			this.jobTypeLabel.Size = new System.Drawing.Size(60, 50);
			this.jobTypeLabel.TabIndex = 12;
			this.jobTypeLabel.Text = "DELETE";
			this.jobTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// JobEntry
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.jobEntryTablePanel);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.MinimumSize = new System.Drawing.Size(0, 25);
			this.Name = "JobEntry";
			this.Size = new System.Drawing.Size(693, 50);
			this.jobEntryTablePanel.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		protected System.Windows.Forms.TableLayoutPanel jobEntryTablePanel;
		protected System.Windows.Forms.Label jobStatusLabel;
		protected System.Windows.Forms.Label jobTypeLabel;
	}
}
