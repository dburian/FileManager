﻿namespace FileManager
{
	partial class JobEntryHeader
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
			this.jobEntryTabelPanel = new System.Windows.Forms.TableLayoutPanel();
			this.jobProgressLabel = new System.Windows.Forms.Label();
			this.jobStatusHeaderLabel = new System.Windows.Forms.Label();
			this.jobTypeHeaderLabel = new System.Windows.Forms.Label();
			this.jobDetailsHeaderLabel = new System.Windows.Forms.Label();
			this.jobEntryTabelPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// jobEntryTabelPanel
			// 
			this.jobEntryTabelPanel.AutoSize = true;
			this.jobEntryTabelPanel.BackColor = System.Drawing.Color.White;
			this.jobEntryTabelPanel.ColumnCount = 4;
			this.jobEntryTabelPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.jobEntryTabelPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.jobEntryTabelPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.jobEntryTabelPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
			this.jobEntryTabelPanel.Controls.Add(this.jobProgressLabel, 3, 0);
			this.jobEntryTabelPanel.Controls.Add(this.jobStatusHeaderLabel, 1, 0);
			this.jobEntryTabelPanel.Controls.Add(this.jobTypeHeaderLabel, 0, 0);
			this.jobEntryTabelPanel.Controls.Add(this.jobDetailsHeaderLabel, 2, 0);
			this.jobEntryTabelPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.jobEntryTabelPanel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.jobEntryTabelPanel.Location = new System.Drawing.Point(0, 0);
			this.jobEntryTabelPanel.Margin = new System.Windows.Forms.Padding(0);
			this.jobEntryTabelPanel.Name = "jobEntryTabelPanel";
			this.jobEntryTabelPanel.RowCount = 1;
			this.jobEntryTabelPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.jobEntryTabelPanel.Size = new System.Drawing.Size(397, 25);
			this.jobEntryTabelPanel.TabIndex = 2;
			// 
			// jobProgressLabel
			// 
			this.jobProgressLabel.BackColor = System.Drawing.Color.White;
			this.jobProgressLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.jobProgressLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.jobProgressLabel.Location = new System.Drawing.Point(317, 0);
			this.jobProgressLabel.Margin = new System.Windows.Forms.Padding(0);
			this.jobProgressLabel.Name = "jobProgressLabel";
			this.jobProgressLabel.Size = new System.Drawing.Size(80, 25);
			this.jobProgressLabel.TabIndex = 14;
			this.jobProgressLabel.Text = "Progress";
			this.jobProgressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// jobStatusHeaderLabel
			// 
			this.jobStatusHeaderLabel.BackColor = System.Drawing.Color.White;
			this.jobStatusHeaderLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.jobStatusHeaderLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.jobStatusHeaderLabel.Location = new System.Drawing.Point(100, 0);
			this.jobStatusHeaderLabel.Margin = new System.Windows.Forms.Padding(0);
			this.jobStatusHeaderLabel.Name = "jobStatusHeaderLabel";
			this.jobStatusHeaderLabel.Size = new System.Drawing.Size(100, 25);
			this.jobStatusHeaderLabel.TabIndex = 8;
			this.jobStatusHeaderLabel.Text = "Status";
			this.jobStatusHeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// jobTypeHeaderLabel
			// 
			this.jobTypeHeaderLabel.BackColor = System.Drawing.Color.White;
			this.jobTypeHeaderLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.jobTypeHeaderLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.jobTypeHeaderLabel.Location = new System.Drawing.Point(0, 0);
			this.jobTypeHeaderLabel.Margin = new System.Windows.Forms.Padding(0);
			this.jobTypeHeaderLabel.Name = "jobTypeHeaderLabel";
			this.jobTypeHeaderLabel.Size = new System.Drawing.Size(100, 25);
			this.jobTypeHeaderLabel.TabIndex = 12;
			this.jobTypeHeaderLabel.Text = "Type";
			this.jobTypeHeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// jobDetailsHeaderLabel
			// 
			this.jobDetailsHeaderLabel.BackColor = System.Drawing.Color.White;
			this.jobDetailsHeaderLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.jobDetailsHeaderLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.jobDetailsHeaderLabel.Location = new System.Drawing.Point(203, 0);
			this.jobDetailsHeaderLabel.Name = "jobDetailsHeaderLabel";
			this.jobDetailsHeaderLabel.Size = new System.Drawing.Size(111, 25);
			this.jobDetailsHeaderLabel.TabIndex = 13;
			this.jobDetailsHeaderLabel.Text = "Details";
			this.jobDetailsHeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// JobEntryHeader
			// 
			this.Controls.Add(this.jobEntryTabelPanel);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.MaximumSize = new System.Drawing.Size(0, 25);
			this.MinimumSize = new System.Drawing.Size(0, 25);
			this.Name = "JobEntryHeader";
			this.Size = new System.Drawing.Size(397, 25);
			this.jobEntryTabelPanel.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel jobEntryTabelPanel;
		private System.Windows.Forms.Label jobStatusHeaderLabel;
		private System.Windows.Forms.Label jobTypeHeaderLabel;
		private System.Windows.Forms.Label jobDetailsHeaderLabel;
		private System.Windows.Forms.Label jobProgressLabel;
	}
}
