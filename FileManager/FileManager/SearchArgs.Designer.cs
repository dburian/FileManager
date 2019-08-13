namespace FileManager
{
	partial class SearchArgs
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
			this.searchingHeaderLabel = new System.Windows.Forms.Label();
			this.inHeaderLabel = new System.Windows.Forms.Label();
			this.searchingArgLabel = new System.Windows.Forms.Label();
			this.inArgLabel = new System.Windows.Forms.Label();
			this.mainTablePanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainTablePanel
			// 
			this.mainTablePanel.BackColor = System.Drawing.Color.White;
			this.mainTablePanel.ColumnCount = 2;
			this.mainTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
			this.mainTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.mainTablePanel.Controls.Add(this.searchingHeaderLabel, 0, 0);
			this.mainTablePanel.Controls.Add(this.inHeaderLabel, 0, 1);
			this.mainTablePanel.Controls.Add(this.searchingArgLabel, 1, 0);
			this.mainTablePanel.Controls.Add(this.inArgLabel, 1, 1);
			this.mainTablePanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainTablePanel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.mainTablePanel.Location = new System.Drawing.Point(0, 0);
			this.mainTablePanel.Name = "mainTablePanel";
			this.mainTablePanel.RowCount = 2;
			this.mainTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.mainTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.mainTablePanel.Size = new System.Drawing.Size(381, 66);
			this.mainTablePanel.TabIndex = 1;
			// 
			// searchingHeaderLabel
			// 
			this.searchingHeaderLabel.BackColor = System.Drawing.Color.White;
			this.searchingHeaderLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.searchingHeaderLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.searchingHeaderLabel.Location = new System.Drawing.Point(0, 0);
			this.searchingHeaderLabel.Margin = new System.Windows.Forms.Padding(0);
			this.searchingHeaderLabel.MinimumSize = new System.Drawing.Size(0, 25);
			this.searchingHeaderLabel.Name = "searchingHeaderLabel";
			this.searchingHeaderLabel.Size = new System.Drawing.Size(80, 33);
			this.searchingHeaderLabel.TabIndex = 0;
			this.searchingHeaderLabel.Text = "SEARCHING:";
			this.searchingHeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// inHeaderLabel
			// 
			this.inHeaderLabel.BackColor = System.Drawing.Color.White;
			this.inHeaderLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.inHeaderLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.inHeaderLabel.Location = new System.Drawing.Point(0, 33);
			this.inHeaderLabel.Margin = new System.Windows.Forms.Padding(0);
			this.inHeaderLabel.MinimumSize = new System.Drawing.Size(0, 25);
			this.inHeaderLabel.Name = "inHeaderLabel";
			this.inHeaderLabel.Size = new System.Drawing.Size(80, 33);
			this.inHeaderLabel.TabIndex = 1;
			this.inHeaderLabel.Text = "IN:";
			this.inHeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// searchingArgLabel
			// 
			this.searchingArgLabel.BackColor = System.Drawing.Color.White;
			this.searchingArgLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.searchingArgLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.searchingArgLabel.Location = new System.Drawing.Point(80, 0);
			this.searchingArgLabel.Margin = new System.Windows.Forms.Padding(0);
			this.searchingArgLabel.MinimumSize = new System.Drawing.Size(0, 25);
			this.searchingArgLabel.Name = "searchingArgLabel";
			this.searchingArgLabel.Size = new System.Drawing.Size(301, 33);
			this.searchingArgLabel.TabIndex = 2;
			this.searchingArgLabel.Text = "somedoc.pdf";
			this.searchingArgLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// inArgLabel
			// 
			this.inArgLabel.BackColor = System.Drawing.Color.White;
			this.inArgLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.inArgLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.inArgLabel.Location = new System.Drawing.Point(80, 33);
			this.inArgLabel.Margin = new System.Windows.Forms.Padding(0);
			this.inArgLabel.MinimumSize = new System.Drawing.Size(0, 25);
			this.inArgLabel.Name = "inArgLabel";
			this.inArgLabel.Size = new System.Drawing.Size(301, 33);
			this.inArgLabel.TabIndex = 3;
			this.inArgLabel.Text = "C:/Users/David/Documents";
			this.inArgLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// SearchArgs
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.mainTablePanel);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.MinimumSize = new System.Drawing.Size(0, 50);
			this.Name = "SearchArgs";
			this.Size = new System.Drawing.Size(381, 66);
			this.mainTablePanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel mainTablePanel;
		private System.Windows.Forms.Label searchingHeaderLabel;
		private System.Windows.Forms.Label inHeaderLabel;
		private System.Windows.Forms.Label searchingArgLabel;
		private System.Windows.Forms.Label inArgLabel;
	}
}
