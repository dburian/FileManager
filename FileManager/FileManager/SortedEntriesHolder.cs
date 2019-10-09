using System;
using System.Collections.Generic;

namespace FileManager
{
	/// <summary>
	/// Manages entries with sorting order involved.
	/// Therefore after each operation, it is necessary to call UpdateView() to propagate the changes to EntriesPane.
	/// </summary>
	/// <typeparam name="EntryType">Type of entry.</typeparam>
	internal class SortedEntriesHolder<EntryType> : EntriesHolder<EntryType> where EntryType : AbstractEntry
	{
		private Comparison<EntryType> _sortOrder;
		public SortedEntriesHolder(EntriesPane<EntryType> pane, Comparison<EntryType> sortOrder)
			: base(pane)
		{
			_sortOrder = sortOrder;
		}

		public Comparison<EntryType> SortOrder
		{
			get => _sortOrder;
			set
			{
				_sortOrder = value;
				UpdateView();
			}
		}

		/// <summary>
		/// Propagates changes done to the SortedEntriesHolder instance to the underlying EntriesPane.
		/// </summary>
		public void UpdateView()
		{
			if (Entries.Count <= 0)
			{
				return;
			}

			Entries.Sort(SortOrder);
			pane.AddEntries(Entries.ToArray());

			EntryInFocusIndex = 0;

			if (!pane.InFocus)
			{
				EntryInFocus.InFocus = false;
			}
		}

		/// <summary>
		/// Clears and resets the entries holder.
		/// </summary>
		public void ClearAndReset()
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
