namespace FileManager
{
	partial class FileEntry
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
			this.fileNameLabel = new System.Windows.Forms.Label();
			this.dateTimeAddedLabel = new System.Windows.Forms.Label();
			this.dateTimeLastModifiedLabel = new System.Windows.Forms.Label();
			this.sizeLabel = new System.Windows.Forms.Label();
			this.fileTypeLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// fileNameLabel
			// 
			this.fileNameLabel.Dock = System.Windows.Forms.DockStyle.Left;
			this.fileNameLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.fileNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.fileNameLabel.Location = new System.Drawing.Point(0, 0);
			this.fileNameLabel.Name = "fileNameLabel";
			this.fileNameLabel.Size = new System.Drawing.Size(300, 25);
			this.fileNameLabel.TabIndex = 0;
			this.fileNameLabel.Text = "hhhhhhhhhhhhhhhhhhhhhhhhh";
			this.fileNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// dateTimeAddedLabel
			// 
			this.dateTimeAddedLabel.Dock = System.Windows.Forms.DockStyle.Right;
			this.dateTimeAddedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dateTimeAddedLabel.Location = new System.Drawing.Point(527, 0);
			this.dateTimeAddedLabel.Name = "dateTimeAddedLabel";
			this.dateTimeAddedLabel.Size = new System.Drawing.Size(99, 25);
			this.dateTimeAddedLabel.TabIndex = 1;
			this.dateTimeAddedLabel.Text = "07-23-19 10:52";
			this.dateTimeAddedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// dateTimeLastModifiedLabel
			// 
			this.dateTimeLastModifiedLabel.Dock = System.Windows.Forms.DockStyle.Right;
			this.dateTimeLastModifiedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dateTimeLastModifiedLabel.Location = new System.Drawing.Point(428, 0);
			this.dateTimeLastModifiedLabel.Name = "dateTimeLastModifiedLabel";
			this.dateTimeLastModifiedLabel.Size = new System.Drawing.Size(99, 25);
			this.dateTimeLastModifiedLabel.TabIndex = 2;
			this.dateTimeLastModifiedLabel.Text = "07-23-19 10:52";
			this.dateTimeLastModifiedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// sizeLabel
			// 
			this.sizeLabel.Dock = System.Windows.Forms.DockStyle.Right;
			this.sizeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.sizeLabel.Location = new System.Drawing.Point(378, 0);
			this.sizeLabel.Name = "sizeLabel";
			this.sizeLabel.Size = new System.Drawing.Size(50, 25);
			this.sizeLabel.TabIndex = 3;
			this.sizeLabel.Text = "123456";
			this.sizeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// fileTypeLabel
			// 
			this.fileTypeLabel.Dock = System.Windows.Forms.DockStyle.Right;
			this.fileTypeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.fileTypeLabel.Location = new System.Drawing.Point(332, 0);
			this.fileTypeLabel.Name = "fileTypeLabel";
			this.fileTypeLabel.Size = new System.Drawing.Size(46, 25);
			this.fileTypeLabel.TabIndex = 4;
			this.fileTypeLabel.Text = "docx";
			this.fileTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// FileEntry
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ButtonShadow;
			this.Controls.Add(this.fileTypeLabel);
			this.Controls.Add(this.sizeLabel);
			this.Controls.Add(this.dateTimeLastModifiedLabel);
			this.Controls.Add(this.dateTimeAddedLabel);
			this.Controls.Add(this.fileNameLabel);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.MinimumSize = new System.Drawing.Size(0, 25);
			this.Name = "FileEntry";
			this.Size = new System.Drawing.Size(626, 25);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label fileNameLabel;
		private System.Windows.Forms.Label dateTimeAddedLabel;
		private System.Windows.Forms.Label dateTimeLastModifiedLabel;
		private System.Windows.Forms.Label sizeLabel;
		private System.Windows.Forms.Label fileTypeLabel;
	}
}
