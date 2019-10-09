namespace FileManager
{
	partial class TransferArgsView
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
			this.mainTablePanel = new System.Windows.Forms.TableLayoutPanel();
			this.fromHeaderLabel = new System.Windows.Forms.Label();
			this.toHeaderLabel = new System.Windows.Forms.Label();
			this.fromArgLabel = new System.Windows.Forms.Label();
			this.toArgLabel = new System.Windows.Forms.Label();
			this.mainTablePanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainTablePanel
			// 
			this.mainTablePanel.BackColor = System.Drawing.Color.White;
			this.mainTablePanel.ColumnCount = 2;
			this.mainTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
			this.mainTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.mainTablePanel.Controls.Add(this.fromHeaderLabel, 0, 0);
			this.mainTablePanel.Controls.Add(this.toHeaderLabel, 0, 1);
			this.mainTablePanel.Controls.Add(this.fromArgLabel, 1, 0);
			this.mainTablePanel.Controls.Add(this.toArgLabel, 1, 1);
			this.mainTablePanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainTablePanel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.mainTablePanel.Location = new System.Drawing.Point(0, 0);
			this.mainTablePanel.Margin = new System.Windows.Forms.Padding(0);
			this.mainTablePanel.Name = "mainTablePanel";
			this.mainTablePanel.RowCount = 2;
			this.mainTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.mainTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.mainTablePanel.Size = new System.Drawing.Size(469, 50);
			this.mainTablePanel.TabIndex = 0;
			// 
			// fromHeaderLabel
			// 
			this.fromHeaderLabel.BackColor = System.Drawing.Color.White;
			this.fromHeaderLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.fromHeaderLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.fromHeaderLabel.Location = new System.Drawing.Point(0, 0);
			this.fromHeaderLabel.Margin = new System.Windows.Forms.Padding(0);
			this.fromHeaderLabel.MinimumSize = new System.Drawing.Size(0, 25);
			this.fromHeaderLabel.Name = "fromHeaderLabel";
			this.fromHeaderLabel.Size = new System.Drawing.Size(80, 25);
			this.fromHeaderLabel.TabIndex = 0;
			this.fromHeaderLabel.Text = "FROM:";
			this.fromHeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// toHeaderLabel
			// 
			this.toHeaderLabel.BackColor = System.Drawing.Color.White;
			this.toHeaderLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toHeaderLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.toHeaderLabel.Location = new System.Drawing.Point(0, 25);
			this.toHeaderLabel.Margin = new System.Windows.Forms.Padding(0);
			this.toHeaderLabel.MinimumSize = new System.Drawing.Size(0, 25);
			this.toHeaderLabel.Name = "toHeaderLabel";
			this.toHeaderLabel.Size = new System.Drawing.Size(80, 25);
			this.toHeaderLabel.TabIndex = 1;
			this.toHeaderLabel.Text = "TO:";
			this.toHeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// fromArgLabel
			// 
			this.fromArgLabel.BackColor = System.Drawing.Color.White;
			this.fromArgLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.fromArgLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.fromArgLabel.Location = new System.Drawing.Point(80, 0);
			this.fromArgLabel.Margin = new System.Windows.Forms.Padding(0);
			this.fromArgLabel.MinimumSize = new System.Drawing.Size(0, 25);
			this.fromArgLabel.Name = "fromArgLabel";
			this.fromArgLabel.Size = new System.Drawing.Size(389, 25);
			this.fromArgLabel.TabIndex = 2;
			this.fromArgLabel.Text = "C:/Users/David/Documents/Matfyz/somedoc.pdf";
			this.fromArgLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// toArgLabel
			// 
			this.toArgLabel.BackColor = System.Drawing.Color.White;
			this.toArgLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toArgLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.toArgLabel.Location = new System.Drawing.Point(80, 25);
			this.toArgLabel.Margin = new System.Windows.Forms.Padding(0);
			this.toArgLabel.MinimumSize = new System.Drawing.Size(0, 25);
			this.toArgLabel.Name = "toArgLabel";
			this.toArgLabel.Size = new System.Drawing.Size(389, 25);
			this.toArgLabel.TabIndex = 3;
			this.toArgLabel.Text = "C:/Users/David/Documents/Matfyz/someotherdoc.pdf";
			this.toArgLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// CopyMoveArgs
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.mainTablePanel);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.MinimumSize = new System.Drawing.Size(0, 50);
			this.Name = "CopyMoveArgs";
			this.Size = new System.Drawing.Size(469, 50);
			this.mainTablePanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel mainTablePanel;
		private System.Windows.Forms.Label fromHeaderLabel;
		private System.Windows.Forms.Label toHeaderLabel;
		private System.Windows.Forms.Label fromArgLabel;
		private System.Windows.Forms.Label toArgLabel;
	}
}
