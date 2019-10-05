using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace FileManager
{ 
	class SortedEntriesHolder<EntryType> : EntriesHolder<EntryType> where EntryType: AbstractEntry
	{
		Comparison<EntryType> _sortOrder;
		public SortedEntriesHolder(EntriesPane<EntryType> pane, Comparison<EntryType> sortOrder)
			:base(pane)
		{
			_sortOrder = sortOrder;
		}

		public Comparison<EntryType> SortOrder
		{
			get => _sortOrder;
			set
			{
				_sortOrder = value;
				Update();
			}
		}
		
		public void Invalidate()
		{
			ClearAndReset();
		}
		public void Update()
		{
			if (Entries.Count <= 0) return;
			
			Entries.Sort(SortOrder);
			pane.AddEntries(Entries.ToArray());

			EntryInFocusIndex = 0;

			if (!pane.InFocus) EntryInFocus.InFocus = false;	
		}
		
		void ClearAndReset()
		{
			//"Invalidate" the pointer to the entry in focus
			_entryInFocusIndex = -1;

			Highlighting = false;
			SelectedEntries = new List<EntryType>();
			Entries = new List<EntryType>();

			pane.ClearEntries();
		}
	}
}
