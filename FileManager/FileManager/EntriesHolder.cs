using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager
{
	abstract class EntriesHolder<EntryType> where EntryType : AbstractEntry
	{
		protected int _entryInFocusIndex;
		
		bool _highlighting;
		bool _inFocus;

		protected readonly EntriesPane<EntryType> pane;

		protected EntriesHolder(EntriesPane<EntryType> pane)
		{
			this.pane = pane;

			//Invalidate pointer to entry in focus
			_entryInFocusIndex = -1;

			SelectedEntries = new List<EntryType>();
			Entries = new List<EntryType>();

			pane.ViewPanel.SizeChanged += (object sender, EventArgs e) => AdjustScrollPanel();
			Entries = new List<EntryType>();
			HighlightingFilter = _ => true;
		}

		public bool InFocus
		{
			get => _inFocus;
			set
			{
				_inFocus = value;
				if (EntryInFocusExists) EntryInFocus.InFocus = _inFocus;
			}
		}
		public List<EntryType> SelectedEntries { get; protected set; }

		/// <summary>
		/// "Pointer" to the entry currently in focus.
		/// </summary>
		public EntryType EntryInFocus { get => EntryInFocusExists ? Entries[EntryInFocusIndex] : null; }
		public bool Highlighting
		{
			get => _highlighting;
			protected set
			{
				_highlighting = value;

				HighlightEntryInFocus();
			}
		}
		public int EntryInFocusIndex
		{
			get => _entryInFocusIndex;
			protected set
			{
				if (EntryInFocusExists) EntryInFocus.InFocus = false;

				_entryInFocusIndex = value;     //Switch focus to new entry

				if (EntryInFocusExists) EntryInFocus.InFocus = true;


				AdjustScrollPanel();
				HighlightEntryInFocus();
			}
		}
		public EntryType this[int i]
		{
			get => Entries[i];
			set => Entries[i] = value;
		}
		public int Count => Entries.Count;
		public Predicate<EntryType> HighlightingFilter { get; set; }

		protected int FilesViewWindowTop
		{
			get => pane.ViewPanel.VerticalScroll.Value;
			set => pane.ViewPanel.VerticalScroll.Value = value;
		}
		protected List<EntryType> Entries { get; set; }
		protected bool EntryInFocusExists { get => EntryInFocusIndex < Entries.Count && EntryInFocusIndex >= 0; }


		public event Action<EntryType> NewEntryHighlighted;
		public event Action<EntryType> OldEntryUnhighlighted;

		
		public virtual void Add(EntryType entry)
		{
			Entries.Add(entry);
		}
		public virtual void AddRange(EntryType[] entries)
		{
			Entries.AddRange(entries);
		}

		public virtual bool FindAndReplace(Predicate<EntryType> filter, EntryType updatedEntry)
		{
			var index = Entries.FindIndex(filter);

			if (index == -1) return false;

			Entries[index] = updatedEntry;
			return true;
		}
		public virtual bool Remove(Predicate<EntryType> filter)
		{
			var item = Entries.Find(filter);
			return item != null && Entries.Remove(item);
		}
		//TODO: Refactor delete unreferenced code
		public int FindIndex(Predicate<EntryType> filter)
		{
			return Entries.FindIndex(filter);
		}
		public EntryType Find(Predicate<EntryType> filter)
		{
			return Entries.Find(filter);
		}
		public virtual bool ProcessKeyPress(InputKey pressedKey)
		{
			if (!EntryInFocusExists) return false;

			if (pressedKey == 'j' || pressedKey == Keys.Down)
			{
				if (EntryInFocusIndex < Entries.Count - 1) EntryInFocusIndex++;
				return true;
			}

			if (pressedKey == 'k' || pressedKey == Keys.Up)
			{
				if (EntryInFocusIndex > 0) EntryInFocusIndex--;
				return true;
			}

			if (pressedKey == 'g' || pressedKey == Keys.Home)
			{
				if (!Highlighting) EntryInFocusIndex = 0;
				else
					while (EntryInFocusIndex > 0) EntryInFocusIndex--;

				return true;
			}

			if (pressedKey == 'G' || pressedKey == Keys.End)
			{
				if (!Highlighting) EntryInFocusIndex = Entries.Count - 1;
				else
					while (EntryInFocusIndex < Entries.Count - 1) EntryInFocusIndex++;
				return true;
			}

			if (pressedKey == 'v')
			{
				Highlighting = !Highlighting;
				return true;
			}


			return false;
		}

		protected void AdjustScrollPanel()
		{
			// If there is no EntryInFocus there can't be any scrolling...
			if (!EntryInFocusExists) return;

			var entryInFocusTop = EntryInFocusIndex * EntryInFocus.Height;

			while (entryInFocusTop < FilesViewWindowTop) FilesViewWindowTop -= Math.Min(EntryInFocus.Height, FilesViewWindowTop);

			while (entryInFocusTop + EntryInFocus.Height > (FilesViewWindowTop + pane.ViewPanel.Height)) FilesViewWindowTop += EntryInFocus.Height;
		}
		protected void HighlightEntryInFocus()
		{
			if (!Highlighting || !EntryInFocusExists || !HighlightingFilter(EntryInFocus)) return;


			if (EntryInFocus.Highlighted)
			{
				SelectedEntries.Remove(EntryInFocus);
				EntryInFocus.Highlighted = false;
				OldEntryUnhighlighted?.Invoke(EntryInFocus);
			}
			else
			{
				SelectedEntries.Add(EntryInFocus);
				EntryInFocus.Highlighted = true;
				NewEntryHighlighted?.Invoke(EntryInFocus);
			}
		}
	}
}
