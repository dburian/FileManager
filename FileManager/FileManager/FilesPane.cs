using System;
using System.IO;
using System.Windows.Forms;

namespace FileManager
{
	/// <summary>
	/// Displays contents of a folder.
	/// </summary>
	public partial class FilesPane : EntriesPane<FileSystemNodeEntry>, IFilesPane
	{
		private DirectoryInfo _currentDir;
		private int _directoryEntriesCount;
		private int _fileEntriesCount;
		private int _selectedEntriesCount;
		private long _selectedEntriesSize;
		private bool _inFocus = false;

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
				if (!value.Exists)
				{
					throw new ArgumentException($"FilesPane.CurrentDir: directory {value.FullName} is invalid.");
				}

				_currentDir = value;

				currentDirectoryLabel.Text = Format.GetCamelCasedPath(_currentDir.FullName);
			}
		}
		public override ScrollableControl ViewPanel => filesViewPanel;
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

		public override Control GetControl()
		{
			return this;
		}

		private void RefreshHighlightedEntriesLabel()
		{
			highlightedEntriesLabel.Text = $"{_selectedEntriesCount} hihglighted - {Format.Size(_selectedEntriesSize)}";
		}

		private void RefreshNumberOfFilesLabel()
		{
			numberOfFilesLabel.Text = $"{_fileEntriesCount} files - {_directoryEntriesCount} directories";
		}
	}
}
