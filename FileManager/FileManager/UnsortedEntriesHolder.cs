using System;

namespace FileManager
{
	/// <summary>
	/// Manages entries without sorting order. Therefore operations on UnsortedEntriesHolder can be immediataly delegated to underlying EntriesPane.
	/// </summary>
	/// <typeparam name="EntryType">Entry type.</typeparam>
	internal class UnsortedEntriesHolder<EntryType> : EntriesHolder<EntryType> where EntryType : AbstractEntry
	{
		public UnsortedEntriesHolder(EntriesPane<EntryType> pane)
			: base(pane)
		{ }


		public override bool InFocus
		{
			get => base.InFocus;
			set
			{
				if (EntryInFocusExists)
				{
					EntryInFocusIndex = 0;
				}

				base.InFocus = value;
			}
		}
		/// <summary>
		/// Adds entry to the bottom of the list.
		/// </summary>
		public override void Add(EntryType entry)
		{
			Entries.Add(entry);

			pane.AddEntry(entry);
			if (!EntryInFocusExists)
			{
				EntryInFocusIndex = 0;
			}
		}

		/// <summary>
		/// Adds entry to the top of the list.
		/// </summary>
		public void AddToTop(EntryType entry)
		{
			Entries.Insert(0, entry);

			pane.AddEntryToTop(entry);
			if (!EntryInFocusExists)
			{
				EntryInFocusIndex = 0;
			}
		}

		/// <summary>
		/// Adds entries to the bottom of the list.
		/// </summary>
		public override void AddRange(EntryType[] entries)
		{
			Entries.AddRange(entries);

			pane.AddEntries(entries);
			if (!EntryInFocusExists)
			{
				EntryInFocusIndex = 0;
			}
		}

		/// <summary>
		/// Removes first entry that passes the <paramref name="filter"/>.
		/// </summary>
		/// <param name="filter">Returns true on entry to be deleted.</param>
		/// <returns>True if an entry was found and deleted, false if otherwise.</returns>
		public override bool Remove(Predicate<EntryType> filter)
		{
			var entry = Entries.Find(filter);

			if (entry == null)
			{
				return false;
			}

			EntryInFocus.InFocus = false;
			if (Entries.Remove(entry))
			{
				pane.RemoveEntry(entry);
				if (!EntryInFocusExists)
				{
					EntryInFocusIndex--;
				}
				if (EntryInFocusExists)
				{
					EntryInFocus.InFocus = true;
				}

				return true;
			}
			EntryInFocus.InFocus = true;

			return false;
		}

		/// <summary>
		/// Finds and replaces first entry that corresponds to the <paramref name="filter"/> and updates it with <paramref name="updatedEntry"/>.
		/// </summary>
		/// <param name="filter">Returns true on entry to be replaced.</param>
		/// <param name="updatedEntry">New value for the found entry.</param>
		/// <returns>True if an entry was found and replaced, false if otherwise.</returns>
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
