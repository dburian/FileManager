namespace FileManager
{
	partial class ErrorMessage
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
			this.errorTypeLabel = new System.Windows.Forms.Label();
			this.errorDetailLabel = new System.Windows.Forms.Label();
			this.okButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// errorTypeLabel
			// 
			this.errorTypeLabel.Dock = System.Windows.Forms.DockStyle.Top;
			this.errorTypeLabel.Font = new System.Drawing.Font("Consolas", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.errorTypeLabel.Location = new System.Drawing.Point(20, 0);
			this.errorTypeLabel.Margin = new System.Windows.Forms.Padding(0);
			this.errorTypeLabel.MaximumSize = new System.Drawing.Size(0, 72);
			this.errorTypeLabel.MinimumSize = new System.Drawing.Size(0, 72);
			this.errorTypeLabel.Name = "errorTypeLabel";
			this.errorTypeLabel.Padding = new System.Windows.Forms.Padding(60, 0, 0, 0);
			this.errorTypeLabel.Size = new System.Drawing.Size(633, 72);
			this.errorTypeLabel.TabIndex = 0;
			this.errorTypeLabel.Text = "Access Denied";
			this.errorTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// errorDetailLabel
			// 
			this.errorDetailLabel.AutoSize = true;
			this.errorDetailLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.errorDetailLabel.Location = new System.Drawing.Point(20, 72);
			this.errorDetailLabel.Margin = new System.Windows.Forms.Padding(0);
			this.errorDetailLabel.Name = "errorDetailLabel";
			this.errorDetailLabel.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
			this.errorDetailLabel.Size = new System.Drawing.Size(413, 24);
			this.errorDetailLabel.TabIndex = 1;
			this.errorDetailLabel.Text = "Access to the folder C:/Documents and Settings was denied.";
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.BackColor = System.Drawing.Color.Black;
			this.okButton.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.okButton.ForeColor = System.Drawing.Color.White;
			this.okButton.Location = new System.Drawing.Point(541, 119);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(100, 32);
			this.okButton.TabIndex = 2;
			this.okButton.Text = "Ok";
			this.okButton.UseVisualStyleBackColor = false;
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// ErrorMessage
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.errorDetailLabel);
			this.Controls.Add(this.errorTypeLabel);
			this.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.Margin = new System.Windows.Forms.Padding(0);
			this.MinimumSize = new System.Drawing.Size(0, 170);
			this.Name = "ErrorMessage";
			this.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
			this.Size = new System.Drawing.Size(673, 170);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label errorTypeLabel;
		private System.Windows.Forms.Label errorDetailLabel;
		private System.Windows.Forms.Button okButton;
	}
}
