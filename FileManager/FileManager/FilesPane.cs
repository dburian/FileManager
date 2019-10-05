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
	public partial class FilesPane : 
		//UserControl,
		EntriesPane<FileSystemNodeEntry>, 
		IFilesPane
	{

		DirectoryInfo _currentDir;
		int _directoryEntriesCount;
		int _fileEntriesCount;
		int _selectedEntriesCount;
		long _selectedEntriesSize;
		bool _inFocus = false;
		
		public FilesPane()
		{
			InitializeComponent();
		}

		public override bool InFocus
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

				_currentDir = value;

				currentDirectoryLabel.Text = _currentDir.FullName;
			}
		}
		public override ScrollableControl ViewPanel { get => filesViewPanel; }
		public long FreeSpaceInDir
		{
			set => freeSpaceLabel.Text = Format.Size(value);
		}
		public int SelectedEntriesCount
		{
			get => _selectedEntriesCount;
			set
			{
				_selectedEntriesCount = value;
				RefreshHighlightedEntriesLabel();
			}
		}
		public long SelectedEntriesSize
		{
			get => _selectedEntriesSize;
			set
			{
				_selectedEntriesSize = value;
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

		public override Control GetControl() => this;

		void RefreshHighlightedEntriesLabel()
		{
			highlightedEntriesLabel.Text = $"{_selectedEntriesCount} hihglighted - {Format.Size(_selectedEntriesSize)}";
		}

		void RefreshNumberOfFilesLabel()
		{
			numberOfFilesLabel.Text = $"{_fileEntriesCount} files - {_directoryEntriesCount} directories";
		}
	}
}
