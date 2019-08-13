using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace FileManager
{
	public partial class FilesPane : UserControl, IFilesPane
	{

		DirectoryInfo _currentDir;
		int _directoryEntriesCount;
		List<FilesViewEntry> _entries;
		int _fileEntriesCount;
		int _highlightedEntriesCount;
		long _highlightedEntriesSize;
		bool _inFocus = false;
		
		public FilesPane()
		{
			InitializeComponent();
		}

		public bool InFocus
		{
			get => _inFocus;
			set
			{
				_inFocus = value;
				if (value)
				{
					currentDirectoryLabel.BackColor = Config.ColorPalette.Black;
					currentDirectoryLabel.ForeColor = Config.ColorPalette.White;
				}
				else
				{
					currentDirectoryLabel.BackColor = Config.ColorPalette.White;
					currentDirectoryLabel.ForeColor = Config.ColorPalette.Black;
				}
			}
		}
		public DirectoryInfo CurrentDir
		{
			get => _currentDir;
			set
			{
				if (!value.Exists) throw new ArgumentException($"FilesPane.CurrentDir: directory {value.FullName} is invalid.");
				currentDirectoryLabel.Text = value.FullName;

				_currentDir = value;
			}
		}
		public List<FilesViewEntry> Entries
		{
			get => _entries;
			set
			{
				_entries = value;

				UpdateFilesViewPanel();
			}
		}
		public ScrollableControl ScrollPanel { get => filesViewPanel; }
		public long FreeSpaceInDir
		{
			set => freeSpaceLabel.Text = Format.Size(value);
		}
		public int HighlightedEntriesCount
		{
			get => _highlightedEntriesCount;
			set
			{
				_highlightedEntriesCount = value;
				RefreshHighlightedEntriesLabel();
			}
		}
		public long HighlightedEntriesSize
		{
			get => _highlightedEntriesSize;
			set
			{
				_highlightedEntriesSize = value;
				RefreshHighlightedEntriesLabel();
			}
		}
		public int FileEntriesCount
		{
			set
			{
				_fileEntriesCount = value;
				RefreshNumberOfFilesLabel();
			}
		}
		public int DirectoryEntriesCount
		{
			set
			{
				_directoryEntriesCount = value;
				RefreshNumberOfFilesLabel();
			}
		}


		public void UpdateFilesViewPanel()
		{
			filesViewPanel.SuspendLayout();
			filesViewPanel.Controls.Clear();

			filesViewPanel.Controls.AddRange(Entries.Reverse<FilesViewEntry>().ToArray());

			bool darkStyle = false;
			for (int i = Entries.Count - 1; i >= 0; i--)
			{
				Entries[i].Dock = DockStyle.Top;
				Entries[i].DarkStyle = darkStyle;
				darkStyle = !darkStyle;
			}
			filesViewPanel.ResumeLayout();
		}
		public Control GetControl() => this;

		void RefreshHighlightedEntriesLabel()
		{
			highlightedEntriesLabel.Text = $"{_highlightedEntriesCount} hihglighted - {Format.Size(_highlightedEntriesSize)}";
		}

		void RefreshNumberOfFilesLabel()
		{
			numberOfFilesLabel.Text = $"{_fileEntriesCount} files - {_directoryEntriesCount} directories";
		}
	}
}
