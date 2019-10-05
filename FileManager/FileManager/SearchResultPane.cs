using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager
{
	public partial class SearchResultPane : EntriesPane<FileSystemNodeEntry>, ISearchResultPane
	{
		bool _inFocus;
		string _searchingName;
		int _found;
		JobStatus _status;

		public SearchResultPane()
		{
			InitializeComponent();
		}

		public string SearchingName
		{
			get => _searchingName;
			set
			{
				_searchingName = value;

				searchingLabel.Text = $"Searching for: {_searchingName}";
			}
		}
		public override bool InFocus
		{
			get => _inFocus;
			set
			{
				_inFocus = value;
				if (_inFocus)
				{
					searchingLabel.BackColor = Config.ColorPalette.Black;
					searchingLabel.ForeColor = Config.ColorPalette.White;
				}else
				{
					searchingLabel.BackColor = Config.ColorPalette.White;
					searchingLabel.ForeColor = Config.ColorPalette.Black;
				}
			}
		}
		public int Found
		{
			get => _found;
			set
			{
				_found = value;

				progressLabel.Text = $"{_found} found";
			}
		}
		public JobStatus Status
		{
			get => _status;
			set
			{
				_status = value;
				statusLabel.Text = _status.ToString().ToUpper();
			}
		}
		public string InDirectory
		{
			set => searchedDirectoryLabel.Text = value;
		}

		public override ScrollableControl ViewPanel => searchResultsViewPanel;
		public override Control GetControl() => this;
	}
}
