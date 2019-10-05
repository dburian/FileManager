namespace FileManager
{
	partial class DelArgsView
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
			this.targetHeaderLabel = new System.Windows.Forms.Label();
			this.targetArgLabel = new System.Windows.Forms.Label();
			this.mainTablePanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainTablePanel
			// 
			this.mainTablePanel.BackColor = System.Drawing.Color.White;
			this.mainTablePanel.ColumnCount = 2;
			this.mainTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
			this.mainTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.mainTablePanel.Controls.Add(this.targetHeaderLabel, 0, 0);
			this.mainTablePanel.Controls.Add(this.targetArgLabel, 1, 0);
			this.mainTablePanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainTablePanel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.mainTablePanel.Location = new System.Drawing.Point(0, 0);
			this.mainTablePanel.Margin = new System.Windows.Forms.Padding(0);
			this.mainTablePanel.MinimumSize = new System.Drawing.Size(0, 25);
			this.mainTablePanel.Name = "mainTablePanel";
			this.mainTablePanel.RowCount = 1;
			this.mainTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.mainTablePanel.Size = new System.Drawing.Size(416, 25);
			this.mainTablePanel.TabIndex = 0;
			// 
			// targetHeaderLabel
			// 
			this.targetHeaderLabel.AutoSize = true;
			this.targetHeaderLabel.BackColor = System.Drawing.Color.White;
			this.targetHeaderLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.targetHeaderLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.targetHeaderLabel.Location = new System.Drawing.Point(0, 0);
			this.targetHeaderLabel.Margin = new System.Windows.Forms.Padding(0);
			this.targetHeaderLabel.MinimumSize = new System.Drawing.Size(0, 25);
			this.targetHeaderLabel.Name = "targetHeaderLabel";
			this.targetHeaderLabel.Size = new System.Drawing.Size(80, 25);
			this.targetHeaderLabel.TabIndex = 0;
			this.targetHeaderLabel.Text = "TARGET:";
			this.targetHeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// targetArgLabel
			// 
			this.targetArgLabel.AutoSize = true;
			this.targetArgLabel.BackColor = System.Drawing.Color.White;
			this.targetArgLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.targetArgLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.targetArgLabel.Location = new System.Drawing.Point(80, 0);
			this.targetArgLabel.Margin = new System.Windows.Forms.Padding(0);
			this.targetArgLabel.MinimumSize = new System.Drawing.Size(0, 25);
			this.targetArgLabel.Name = "targetArgLabel";
			this.targetArgLabel.Size = new System.Drawing.Size(336, 25);
			this.targetArgLabel.TabIndex = 1;
			this.targetArgLabel.Text = "C:/Users/David/Documents/Matfyz/somedoc.pdf";
			this.targetArgLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// DelArgs
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.mainTablePanel);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.MinimumSize = new System.Drawing.Size(0, 25);
			this.Name = "DelArgs";
			this.Size = new System.Drawing.Size(416, 25);
			this.mainTablePanel.ResumeLayout(false);
			this.mainTablePanel.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel mainTablePanel;
		private System.Windows.Forms.Label targetHeaderLabel;
		private System.Windows.Forms.Label targetArgLabel;
	}
}
