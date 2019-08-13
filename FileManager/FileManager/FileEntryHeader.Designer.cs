namespace FileManager
{
	partial class FileEntryHeader
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
			this.fileNameHeaderLabel = new System.Windows.Forms.Label();
			this.fileEntryTableHeaderPanel = new System.Windows.Forms.TableLayoutPanel();
			this.dateTimeAddedHeaderLabel = new System.Windows.Forms.Label();
			this.dateTimeLastModifiedHeaderLabel = new System.Windows.Forms.Label();
			this.sizeHeaderLabel = new System.Windows.Forms.Label();
			this.fileTypeHeaderLabel = new System.Windows.Forms.Label();
			this.fileEntryTableHeaderPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// fileNameHeaderLabel
			// 
			this.fileNameHeaderLabel.BackColor = System.Drawing.Color.White;
			this.fileNameHeaderLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.fileNameHeaderLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.fileNameHeaderLabel.Location = new System.Drawing.Point(0, 0);
			this.fileNameHeaderLabel.Margin = new System.Windows.Forms.Padding(0);
			this.fileNameHeaderLabel.Name = "fileNameHeaderLabel";
			this.fileNameHeaderLabel.Size = new System.Drawing.Size(55, 29);
			this.fileNameHeaderLabel.TabIndex = 12;
			this.fileNameHeaderLabel.Text = "File name";
			this.fileNameHeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// fileEntryTableHeaderPanel
			// 
			this.fileEntryTableHeaderPanel.BackColor = System.Drawing.Color.White;
			this.fileEntryTableHeaderPanel.ColumnCount = 5;
			this.fileEntryTableHeaderPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.fileEntryTableHeaderPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.fileEntryTableHeaderPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
			this.fileEntryTableHeaderPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
			this.fileEntryTableHeaderPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
			this.fileEntryTableHeaderPanel.Controls.Add(this.dateTimeAddedHeaderLabel, 4, 0);
			this.fileEntryTableHeaderPanel.Controls.Add(this.dateTimeLastModifiedHeaderLabel, 3, 0);
			this.fileEntryTableHeaderPanel.Controls.Add(this.sizeHeaderLabel, 2, 0);
			this.fileEntryTableHeaderPanel.Controls.Add(this.fileTypeHeaderLabel, 1, 0);
			this.fileEntryTableHeaderPanel.Controls.Add(this.fileNameHeaderLabel, 0, 0);
			this.fileEntryTableHeaderPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.fileEntryTableHeaderPanel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.fileEntryTableHeaderPanel.Location = new System.Drawing.Point(0, 0);
			this.fileEntryTableHeaderPanel.Margin = new System.Windows.Forms.Padding(0);
			this.fileEntryTableHeaderPanel.Name = "fileEntryTableHeaderPanel";
			this.fileEntryTableHeaderPanel.RowCount = 1;
			this.fileEntryTableHeaderPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.fileEntryTableHeaderPanel.Size = new System.Drawing.Size(395, 29);
			this.fileEntryTableHeaderPanel.TabIndex = 1;
			// 
			// dateTimeAddedHeaderLabel
			// 
			this.dateTimeAddedHeaderLabel.BackColor = System.Drawing.Color.White;
			this.dateTimeAddedHeaderLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dateTimeAddedHeaderLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.dateTimeAddedHeaderLabel.Location = new System.Drawing.Point(285, 0);
			this.dateTimeAddedHeaderLabel.Margin = new System.Windows.Forms.Padding(0);
			this.dateTimeAddedHeaderLabel.Name = "dateTimeAddedHeaderLabel";
			this.dateTimeAddedHeaderLabel.Size = new System.Drawing.Size(110, 29);
			this.dateTimeAddedHeaderLabel.TabIndex = 11;
			this.dateTimeAddedHeaderLabel.Text = "Created";
			this.dateTimeAddedHeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// dateTimeLastModifiedHeaderLabel
			// 
			this.dateTimeLastModifiedHeaderLabel.BackColor = System.Drawing.Color.White;
			this.dateTimeLastModifiedHeaderLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dateTimeLastModifiedHeaderLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.dateTimeLastModifiedHeaderLabel.Location = new System.Drawing.Point(175, 0);
			this.dateTimeLastModifiedHeaderLabel.Margin = new System.Windows.Forms.Padding(0);
			this.dateTimeLastModifiedHeaderLabel.Name = "dateTimeLastModifiedHeaderLabel";
			this.dateTimeLastModifiedHeaderLabel.Size = new System.Drawing.Size(110, 29);
			this.dateTimeLastModifiedHeaderLabel.TabIndex = 7;
			this.dateTimeLastModifiedHeaderLabel.Text = "Modified";
			this.dateTimeLastModifiedHeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// sizeHeaderLabel
			// 
			this.sizeHeaderLabel.BackColor = System.Drawing.Color.White;
			this.sizeHeaderLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.sizeHeaderLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.sizeHeaderLabel.Location = new System.Drawing.Point(105, 0);
			this.sizeHeaderLabel.Margin = new System.Windows.Forms.Padding(0);
			this.sizeHeaderLabel.Name = "sizeHeaderLabel";
			this.sizeHeaderLabel.Size = new System.Drawing.Size(70, 29);
			this.sizeHeaderLabel.TabIndex = 8;
			this.sizeHeaderLabel.Text = "Size";
			this.sizeHeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// fileTypeHeaderLabel
			// 
			this.fileTypeHeaderLabel.BackColor = System.Drawing.Color.White;
			this.fileTypeHeaderLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.fileTypeHeaderLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.fileTypeHeaderLabel.Location = new System.Drawing.Point(55, 0);
			this.fileTypeHeaderLabel.Margin = new System.Windows.Forms.Padding(0);
			this.fileTypeHeaderLabel.Name = "fileTypeHeaderLabel";
			this.fileTypeHeaderLabel.Size = new System.Drawing.Size(50, 29);
			this.fileTypeHeaderLabel.TabIndex = 9;
			this.fileTypeHeaderLabel.Text = "Exten.";
			this.fileTypeHeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// FileEntryHeader
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.fileEntryTableHeaderPanel);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.MaximumSize = new System.Drawing.Size(0, 29);
			this.MinimumSize = new System.Drawing.Size(380, 29);
			this.Name = "FileEntryHeader";
			this.Size = new System.Drawing.Size(395, 29);
			this.fileEntryTableHeaderPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label fileNameHeaderLabel;
		private System.Windows.Forms.TableLayoutPanel fileEntryTableHeaderPanel;
		private System.Windows.Forms.Label dateTimeAddedHeaderLabel;
		private System.Windows.Forms.Label dateTimeLastModifiedHeaderLabel;
		private System.Windows.Forms.Label sizeHeaderLabel;
		private System.Windows.Forms.Label fileTypeHeaderLabel;
	}
}
