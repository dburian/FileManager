namespace FileManager
{
	partial class ErrorEntry
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
			this.errorEntryTablePanel = new System.Windows.Forms.TableLayoutPanel();
			this.errorDetailLabel = new System.Windows.Forms.Label();
			this.errorEntryTablePanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// errorEntryTablePanel
			// 
			this.errorEntryTablePanel.BackColor = System.Drawing.Color.White;
			this.errorEntryTablePanel.ColumnCount = 2;
			this.errorEntryTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.errorEntryTablePanel.Controls.Add(this.errorDetailLabel, 0, 0);
			this.errorEntryTablePanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.errorEntryTablePanel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.errorEntryTablePanel.Location = new System.Drawing.Point(1, 1);
			this.errorEntryTablePanel.Margin = new System.Windows.Forms.Padding(0);
			this.errorEntryTablePanel.Name = "errorEntryTablePanel";
			this.errorEntryTablePanel.RowCount = 1;
			this.errorEntryTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.errorEntryTablePanel.Size = new System.Drawing.Size(378, 16);
			this.errorEntryTablePanel.TabIndex = 0;
			// 
			// errorDetailLabel
			// 
			this.errorDetailLabel.BackColor = System.Drawing.Color.White;
			this.errorDetailLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.errorDetailLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.errorDetailLabel.Location = new System.Drawing.Point(0, 0);
			this.errorDetailLabel.Margin = new System.Windows.Forms.Padding(0);
			this.errorDetailLabel.Name = "errorDetailLabel";
			this.errorDetailLabel.Size = new System.Drawing.Size(258, 16);
			this.errorDetailLabel.TabIndex = 12;
			this.errorDetailLabel.Text = "Access to the folder C:\\Documents and Settings was denied";
			this.errorDetailLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ErrorEntry
			// 
			
			this.Controls.Add(this.errorEntryTablePanel);
			this.Name = "ErrorEntry";
			this.Size = new System.Drawing.Size(380, 18);
			this.errorEntryTablePanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel errorEntryTablePanel;
		private System.Windows.Forms.Label errorDetailLabel;
	}
}
