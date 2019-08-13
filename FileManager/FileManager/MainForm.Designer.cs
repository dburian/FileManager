namespace FileManager
{
	partial class MainForm
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.goToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.moveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.searchFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sortFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.byExtensionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.nameAscendingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.nameDescendingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.bySizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sizeAscendingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sizeDescendingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.byToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.extAscendingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.extDescendingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.byLastModifiedDateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.modifiedAscendingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.modifiedDescendingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.byDateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.createdAscendingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.createdDescendingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.windowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.filesViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.filesLeftMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.filesRightMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.jobsViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.jobsLeftMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.jobsRightMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.switchPaneToFullScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fullscreenLeftMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fullscreenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.commandPrompt = new FileManager.CommandPrompt();
			this.mainPanel = new System.Windows.Forms.Panel();
			this.mainMenuStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
			this.splitContainer.SuspendLayout();
			this.mainPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainMenuStrip
			// 
			this.mainMenuStrip.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.windowsToolStripMenuItem});
			this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
			this.mainMenuStrip.Name = "mainMenuStrip";
			this.mainMenuStrip.Size = new System.Drawing.Size(820, 24);
			this.mainMenuStrip.TabIndex = 0;
			this.mainMenuStrip.Text = "menuStrip1";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.goToToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.moveToolStripMenuItem,
            this.renameToolStripMenuItem,
            this.deleteFilesToolStripMenuItem,
            this.searchFileToolStripMenuItem,
            this.sortFilesToolStripMenuItem});
			this.toolStripMenuItem1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(68, 20);
			this.toolStripMenuItem1.Text = "Actions";
			// 
			// goToToolStripMenuItem
			// 
			this.goToToolStripMenuItem.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.goToToolStripMenuItem.Name = "goToToolStripMenuItem";
			this.goToToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
			this.goToToolStripMenuItem.Text = "Go to";
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
			this.copyToolStripMenuItem.Text = "Copy";
			// 
			// moveToolStripMenuItem
			// 
			this.moveToolStripMenuItem.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.moveToolStripMenuItem.Name = "moveToolStripMenuItem";
			this.moveToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
			this.moveToolStripMenuItem.Text = "Move";
			// 
			// renameToolStripMenuItem
			// 
			this.renameToolStripMenuItem.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
			this.renameToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
			this.renameToolStripMenuItem.Text = "Rename";
			// 
			// deleteFilesToolStripMenuItem
			// 
			this.deleteFilesToolStripMenuItem.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.deleteFilesToolStripMenuItem.Name = "deleteFilesToolStripMenuItem";
			this.deleteFilesToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
			this.deleteFilesToolStripMenuItem.Text = "Delete";
			// 
			// searchFileToolStripMenuItem
			// 
			this.searchFileToolStripMenuItem.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.searchFileToolStripMenuItem.Name = "searchFileToolStripMenuItem";
			this.searchFileToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
			this.searchFileToolStripMenuItem.Text = "Search";
			// 
			// sortFilesToolStripMenuItem
			// 
			this.sortFilesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.byExtensionToolStripMenuItem,
            this.bySizeToolStripMenuItem,
            this.byToolStripMenuItem,
            this.byLastModifiedDateToolStripMenuItem,
            this.byDateToolStripMenuItem});
			this.sortFilesToolStripMenuItem.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.sortFilesToolStripMenuItem.Name = "sortFilesToolStripMenuItem";
			this.sortFilesToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
			this.sortFilesToolStripMenuItem.Text = "Sort";
			// 
			// byExtensionToolStripMenuItem
			// 
			this.byExtensionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nameAscendingMenuItem,
            this.nameDescendingMenuItem});
			this.byExtensionToolStripMenuItem.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.byExtensionToolStripMenuItem.Name = "byExtensionToolStripMenuItem";
			this.byExtensionToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
			this.byExtensionToolStripMenuItem.Text = "By name";
			// 
			// nameAscendingMenuItem
			// 
			this.nameAscendingMenuItem.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.nameAscendingMenuItem.Name = "nameAscendingMenuItem";
			this.nameAscendingMenuItem.Size = new System.Drawing.Size(144, 22);
			this.nameAscendingMenuItem.Text = "Ascending";
			// 
			// nameDescendingMenuItem
			// 
			this.nameDescendingMenuItem.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.nameDescendingMenuItem.Name = "nameDescendingMenuItem";
			this.nameDescendingMenuItem.Size = new System.Drawing.Size(144, 22);
			this.nameDescendingMenuItem.Text = "Descending";
			// 
			// bySizeToolStripMenuItem
			// 
			this.bySizeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sizeAscendingMenuItem,
            this.sizeDescendingMenuItem});
			this.bySizeToolStripMenuItem.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.bySizeToolStripMenuItem.Name = "bySizeToolStripMenuItem";
			this.bySizeToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
			this.bySizeToolStripMenuItem.Text = "By size";
			// 
			// sizeAscendingMenuItem
			// 
			this.sizeAscendingMenuItem.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.sizeAscendingMenuItem.Name = "sizeAscendingMenuItem";
			this.sizeAscendingMenuItem.Size = new System.Drawing.Size(144, 22);
			this.sizeAscendingMenuItem.Text = "Ascending";
			// 
			// sizeDescendingMenuItem
			// 
			this.sizeDescendingMenuItem.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.sizeDescendingMenuItem.Name = "sizeDescendingMenuItem";
			this.sizeDescendingMenuItem.Size = new System.Drawing.Size(144, 22);
			this.sizeDescendingMenuItem.Text = "Descending";
			// 
			// byToolStripMenuItem
			// 
			this.byToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.extAscendingMenuItem,
            this.extDescendingMenuItem});
			this.byToolStripMenuItem.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.byToolStripMenuItem.Name = "byToolStripMenuItem";
			this.byToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
			this.byToolStripMenuItem.Text = "By extension";
			// 
			// extAscendingMenuItem
			// 
			this.extAscendingMenuItem.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.extAscendingMenuItem.Name = "extAscendingMenuItem";
			this.extAscendingMenuItem.Size = new System.Drawing.Size(144, 22);
			this.extAscendingMenuItem.Text = "Ascending";
			// 
			// extDescendingMenuItem
			// 
			this.extDescendingMenuItem.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.extDescendingMenuItem.Name = "extDescendingMenuItem";
			this.extDescendingMenuItem.Size = new System.Drawing.Size(144, 22);
			this.extDescendingMenuItem.Text = "Descending";
			// 
			// byLastModifiedDateToolStripMenuItem
			// 
			this.byLastModifiedDateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modifiedAscendingMenuItem,
            this.modifiedDescendingMenuItem});
			this.byLastModifiedDateToolStripMenuItem.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.byLastModifiedDateToolStripMenuItem.Name = "byLastModifiedDateToolStripMenuItem";
			this.byLastModifiedDateToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
			this.byLastModifiedDateToolStripMenuItem.Text = "By date modified";
			// 
			// modifiedAscendingMenuItem
			// 
			this.modifiedAscendingMenuItem.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.modifiedAscendingMenuItem.Name = "modifiedAscendingMenuItem";
			this.modifiedAscendingMenuItem.Size = new System.Drawing.Size(144, 22);
			this.modifiedAscendingMenuItem.Text = "Ascending";
			// 
			// modifiedDescendingMenuItem
			// 
			this.modifiedDescendingMenuItem.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.modifiedDescendingMenuItem.Name = "modifiedDescendingMenuItem";
			this.modifiedDescendingMenuItem.Size = new System.Drawing.Size(144, 22);
			this.modifiedDescendingMenuItem.Text = "Descending";
			// 
			// byDateToolStripMenuItem
			// 
			this.byDateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createdAscendingMenuItem,
            this.createdDescendingMenuItem});
			this.byDateToolStripMenuItem.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.byDateToolStripMenuItem.Name = "byDateToolStripMenuItem";
			this.byDateToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
			this.byDateToolStripMenuItem.Text = "By date created";
			// 
			// createdAscendingMenuItem
			// 
			this.createdAscendingMenuItem.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.createdAscendingMenuItem.Name = "createdAscendingMenuItem";
			this.createdAscendingMenuItem.Size = new System.Drawing.Size(144, 22);
			this.createdAscendingMenuItem.Text = "Ascending";
			// 
			// createdDescendingMenuItem
			// 
			this.createdDescendingMenuItem.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.createdDescendingMenuItem.Name = "createdDescendingMenuItem";
			this.createdDescendingMenuItem.Size = new System.Drawing.Size(144, 22);
			this.createdDescendingMenuItem.Text = "Descending";
			// 
			// windowsToolStripMenuItem
			// 
			this.windowsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filesViewToolStripMenuItem,
            this.jobsViewToolStripMenuItem,
            this.switchPaneToFullScreenToolStripMenuItem});
			this.windowsToolStripMenuItem.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.windowsToolStripMenuItem.Name = "windowsToolStripMenuItem";
			this.windowsToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
			this.windowsToolStripMenuItem.Text = "Windows";
			// 
			// filesViewToolStripMenuItem
			// 
			this.filesViewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filesLeftMenuItem,
            this.filesRightMenuItem});
			this.filesViewToolStripMenuItem.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.filesViewToolStripMenuItem.Name = "filesViewToolStripMenuItem";
			this.filesViewToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
			this.filesViewToolStripMenuItem.Text = "View files";
			// 
			// filesLeftMenuItem
			// 
			this.filesLeftMenuItem.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.filesLeftMenuItem.Name = "filesLeftMenuItem";
			this.filesLeftMenuItem.Size = new System.Drawing.Size(109, 22);
			this.filesLeftMenuItem.Text = "Left";
			// 
			// filesRightMenuItem
			// 
			this.filesRightMenuItem.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.filesRightMenuItem.Name = "filesRightMenuItem";
			this.filesRightMenuItem.Size = new System.Drawing.Size(109, 22);
			this.filesRightMenuItem.Text = "Right";
			// 
			// jobsViewToolStripMenuItem
			// 
			this.jobsViewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.jobsLeftMenuItem,
            this.jobsRightMenuItem});
			this.jobsViewToolStripMenuItem.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.jobsViewToolStripMenuItem.Name = "jobsViewToolStripMenuItem";
			this.jobsViewToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
			this.jobsViewToolStripMenuItem.Text = "View jobs";
			// 
			// jobsLeftMenuItem
			// 
			this.jobsLeftMenuItem.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.jobsLeftMenuItem.Name = "jobsLeftMenuItem";
			this.jobsLeftMenuItem.Size = new System.Drawing.Size(109, 22);
			this.jobsLeftMenuItem.Text = "Left";
			// 
			// jobsRightMenuItem
			// 
			this.jobsRightMenuItem.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.jobsRightMenuItem.Name = "jobsRightMenuItem";
			this.jobsRightMenuItem.Size = new System.Drawing.Size(109, 22);
			this.jobsRightMenuItem.Text = "Right";
			// 
			// switchPaneToFullScreenToolStripMenuItem
			// 
			this.switchPaneToFullScreenToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fullscreenLeftMenuItem,
            this.fullscreenMenuItem});
			this.switchPaneToFullScreenToolStripMenuItem.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.switchPaneToFullScreenToolStripMenuItem.Name = "switchPaneToFullScreenToolStripMenuItem";
			this.switchPaneToFullScreenToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
			this.switchPaneToFullScreenToolStripMenuItem.Text = "Switch pane to full screen";
			// 
			// fullscreenLeftMenuItem
			// 
			this.fullscreenLeftMenuItem.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.fullscreenLeftMenuItem.Name = "fullscreenLeftMenuItem";
			this.fullscreenLeftMenuItem.Size = new System.Drawing.Size(109, 22);
			this.fullscreenLeftMenuItem.Text = "Left";
			// 
			// fullscreenMenuItem
			// 
			this.fullscreenMenuItem.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.fullscreenMenuItem.Name = "fullscreenMenuItem";
			this.fullscreenMenuItem.Size = new System.Drawing.Size(109, 22);
			this.fullscreenMenuItem.Text = "Right";
			// 
			// splitContainer
			// 
			this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.splitContainer.Location = new System.Drawing.Point(0, 0);
			this.splitContainer.Margin = new System.Windows.Forms.Padding(0);
			this.splitContainer.Name = "splitContainer";
			// 
			// splitContainer.Panel1
			// 
			this.splitContainer.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.splitContainer.Panel1MinSize = 390;
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.splitContainer.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.splitContainer.Size = new System.Drawing.Size(820, 445);
			this.splitContainer.SplitterDistance = 411;
			this.splitContainer.TabIndex = 0;
			// 
			// commandPrompt
			// 
			this.commandPrompt.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.commandPrompt.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.commandPrompt.Location = new System.Drawing.Point(0, 469);
			this.commandPrompt.Margin = new System.Windows.Forms.Padding(0);
			this.commandPrompt.Name = "commandPrompt";
			this.commandPrompt.Size = new System.Drawing.Size(820, 29);
			this.commandPrompt.TabIndex = 1;
			// 
			// mainPanel
			// 
			this.mainPanel.Controls.Add(this.splitContainer);
			this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainPanel.Location = new System.Drawing.Point(0, 24);
			this.mainPanel.Name = "mainPanel";
			this.mainPanel.Size = new System.Drawing.Size(820, 445);
			this.mainPanel.TabIndex = 2;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(820, 498);
			this.Controls.Add(this.mainPanel);
			this.Controls.Add(this.commandPrompt);
			this.Controls.Add(this.mainMenuStrip);
			this.DoubleBuffered = true;
			this.KeyPreview = true;
			this.MainMenuStrip = this.mainMenuStrip;
			this.MinimumSize = new System.Drawing.Size(836, 39);
			this.Name = "MainForm";
			this.Text = "File Manager";
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ProcessKeyPress);
			this.mainMenuStrip.ResumeLayout(false);
			this.mainMenuStrip.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
			this.splitContainer.ResumeLayout(false);
			this.mainPanel.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip mainMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem windowsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem filesViewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem filesLeftMenuItem;
		private System.Windows.Forms.ToolStripMenuItem filesRightMenuItem;
		private System.Windows.Forms.ToolStripMenuItem jobsViewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem jobsLeftMenuItem;
		private System.Windows.Forms.ToolStripMenuItem jobsRightMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem moveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem deleteFilesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem searchFileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem sortFilesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem byExtensionToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem nameAscendingMenuItem;
		private System.Windows.Forms.ToolStripMenuItem nameDescendingMenuItem;
		private System.Windows.Forms.ToolStripMenuItem byToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem extAscendingMenuItem;
		private System.Windows.Forms.ToolStripMenuItem extDescendingMenuItem;
		private System.Windows.Forms.ToolStripMenuItem byLastModifiedDateToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem modifiedAscendingMenuItem;
		private System.Windows.Forms.ToolStripMenuItem modifiedDescendingMenuItem;
		private System.Windows.Forms.ToolStripMenuItem byDateToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem createdAscendingMenuItem;
		private System.Windows.Forms.ToolStripMenuItem createdDescendingMenuItem;
		private System.Windows.Forms.ToolStripMenuItem bySizeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem sizeAscendingMenuItem;
		private System.Windows.Forms.ToolStripMenuItem sizeDescendingMenuItem;
		private CommandPrompt commandPrompt;
		private System.Windows.Forms.ToolStripMenuItem goToToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem switchPaneToFullScreenToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fullscreenLeftMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fullscreenMenuItem;
		private System.Windows.Forms.SplitContainer splitContainer;
		private System.Windows.Forms.Panel mainPanel;
	}
}

