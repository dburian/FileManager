namespace FileManager
{
	partial class FileSystemNodeEntry
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
			this.fileEntryTablePanel = new System.Windows.Forms.TableLayoutPanel();
			this.dateTimeAddedLabel = new System.Windows.Forms.Label();
			this.dateTimeLastModifiedLabel = new System.Windows.Forms.Label();
			this.sizeLabel = new System.Windows.Forms.Label();
			this.typeLabel = new System.Windows.Forms.Label();
			this.nameLabel = new System.Windows.Forms.Label();
			this.fileEntryTablePanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// fileEntryTablePanel
			// 
			this.fileEntryTablePanel.BackColor = System.Drawing.Color.White;
			this.fileEntryTablePanel.ColumnCount = 5;
			this.fileEntryTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.fileEntryTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.fileEntryTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
			this.fileEntryTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
			this.fileEntryTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
			this.fileEntryTablePanel.Controls.Add(this.dateTimeAddedLabel, 4, 0);
			this.fileEntryTablePanel.Controls.Add(this.dateTimeLastModifiedLabel, 3, 0);
			this.fileEntryTablePanel.Controls.Add(this.sizeLabel, 2, 0);
			this.fileEntryTablePanel.Controls.Add(this.typeLabel, 1, 0);
			this.fileEntryTablePanel.Controls.Add(this.nameLabel, 0, 0);
			this.fileEntryTablePanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.fileEntryTablePanel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.fileEntryTablePanel.Location = new System.Drawing.Point(1, 1);
			this.fileEntryTablePanel.Margin = new System.Windows.Forms.Padding(0);
			this.fileEntryTablePanel.Name = "fileEntryTablePanel";
			this.fileEntryTablePanel.RowCount = 1;
			this.fileEntryTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.fileEntryTablePanel.Size = new System.Drawing.Size(393, 16);
			this.fileEntryTablePanel.TabIndex = 0;
			// 
			// dateTimeAddedLabel
			// 
			this.dateTimeAddedLabel.BackColor = System.Drawing.Color.White;
			this.dateTimeAddedLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dateTimeAddedLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.dateTimeAddedLabel.Location = new System.Drawing.Point(268, 0);
			this.dateTimeAddedLabel.Margin = new System.Windows.Forms.Padding(0);
			this.dateTimeAddedLabel.Name = "dateTimeAddedLabel";
			this.dateTimeAddedLabel.Size = new System.Drawing.Size(110, 16);
			this.dateTimeAddedLabel.TabIndex = 11;
			this.dateTimeAddedLabel.Text = "07-23-19 10:52";
			this.dateTimeAddedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dateTimeLastModifiedLabel
			// 
			this.dateTimeLastModifiedLabel.BackColor = System.Drawing.Color.White;
			this.dateTimeLastModifiedLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dateTimeLastModifiedLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.dateTimeLastModifiedLabel.Location = new System.Drawing.Point(158, 0);
			this.dateTimeLastModifiedLabel.Margin = new System.Windows.Forms.Padding(0);
			this.dateTimeLastModifiedLabel.Name = "dateTimeLastModifiedLabel";
			this.dateTimeLastModifiedLabel.Size = new System.Drawing.Size(110, 16);
			this.dateTimeLastModifiedLabel.TabIndex = 7;
			this.dateTimeLastModifiedLabel.Text = "07-23-19 10:52";
			this.dateTimeLastModifiedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// sizeLabel
			// 
			this.sizeLabel.BackColor = System.Drawing.Color.White;
			this.sizeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.sizeLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.sizeLabel.Location = new System.Drawing.Point(88, 0);
			this.sizeLabel.Margin = new System.Windows.Forms.Padding(0);
			this.sizeLabel.Name = "sizeLabel";
			this.sizeLabel.Size = new System.Drawing.Size(70, 16);
			this.sizeLabel.TabIndex = 8;
			this.sizeLabel.Text = "3328 MiB";
			this.sizeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// typeLabel
			// 
			this.typeLabel.BackColor = System.Drawing.Color.White;
			this.typeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.typeLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.typeLabel.Location = new System.Drawing.Point(38, 0);
			this.typeLabel.Margin = new System.Windows.Forms.Padding(0);
			this.typeLabel.Name = "typeLabel";
			this.typeLabel.Size = new System.Drawing.Size(50, 16);
			this.typeLabel.TabIndex = 9;
			this.typeLabel.Text = "docx";
			this.typeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// nameLabel
			// 
			this.nameLabel.BackColor = System.Drawing.Color.White;
			this.nameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.nameLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.nameLabel.Location = new System.Drawing.Point(0, 0);
			this.nameLabel.Margin = new System.Windows.Forms.Padding(0);
			this.nameLabel.Name = "nameLabel";
			this.nameLabel.Size = new System.Drawing.Size(38, 16);
			this.nameLabel.TabIndex = 12;
			this.nameLabel.Text = "Some file name";
			this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// FileSystemNodeEntry
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Dock = System.Windows.Forms.DockStyle.Top;
			this.Controls.Add(this.fileEntryTablePanel);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.MaximumSize = new System.Drawing.Size(0, 18);
			this.MinimumSize = new System.Drawing.Size(380, 18);
			this.Name = "FilesViewEntry";
			this.Padding = new System.Windows.Forms.Padding(1);
			this.Size = new System.Drawing.Size(395, 18);
			this.fileEntryTablePanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		protected internal System.Windows.Forms.TableLayoutPanel fileEntryTablePanel;
		protected internal System.Windows.Forms.Label typeLabel;
		protected internal System.Windows.Forms.Label sizeLabel;
		protected internal System.Windows.Forms.Label dateTimeLastModifiedLabel;
		protected internal System.Windows.Forms.Label dateTimeAddedLabel;
		protected internal System.Windows.Forms.Label nameLabel;
	}
}
