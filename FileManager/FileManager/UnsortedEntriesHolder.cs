using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace FileManager
{
	class UnsortedEntriesHolder<EntryType> : EntriesHolder<EntryType> where EntryType: AbstractEntry
	{
		public UnsortedEntriesHolder(EntriesPane<EntryType> pane)
			:base(pane)
		{

		}

		public override void Add(EntryType entry)
		{
			Entries.Add(entry);
			pane.AddEntry(entry);
			if (!EntryInFocusExists) EntryInFocusIndex = 0;
		}

		public override void AddRange(EntryType[] entries)
		{
			Entries.AddRange(entries);

			pane.AddEntries(entries);
			if (!EntryInFocusExists) EntryInFocusIndex = 0;
		}

		public override bool Remove(Predicate<EntryType> filter)
		{
			var entry = Entries.Find(filter);
			if (EntryInFocus == entry) EntryInFocusIndex--;

			if (entry == null) return false;

			if(Entries.Remove(entry))
			{
				pane.RemoveEntry(entry);
				return true;
			}

			return false;
		}

		public override bool FindAndReplace(Predicate<EntryType> filter, EntryType updatedEntry)
		{
			var entry = Entries.Find(filter);
			var index = pane.IndexOfEntry(entry);

			if (base.FindAndReplace(filter, updatedEntry) && index != -1)
			{
				pane.ReplaceEntry(index, updatedEntry);

				return true;
			}

			return false;
		}
	}
}
