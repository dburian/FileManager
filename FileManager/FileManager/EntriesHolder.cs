using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FileManager
{
	/// <summary>
	/// Class that manages displayable entries.
	/// </summary>
	/// <typeparam name="EntryType">Entry type.</typeparam>
	internal abstract class EntriesHolder<EntryType> where EntryType : AbstractEntry
	{
		protected int _entryInFocusIndex;
		private bool _highlighting;
		private bool _inFocus;

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


		public virtual bool InFocus
		{
			get => _inFocus;
			set
			{
				_inFocus = value;
				if (EntryInFocusExists)
				{
					EntryInFocus.InFocus = _inFocus;
				}
			}
		}

		/// <summary>
		/// Returns the list of currently selected entries.
		/// </summary>
		public List<EntryType> SelectedEntries { get; protected set; }

		/// <summary>
		/// "Pointer" to the entry currently in focus.
		/// </summary>
		public EntryType EntryInFocus => EntryInFocusExists ? Entries[EntryInFocusIndex] : null;
		public bool Highlighting
		{
			get => _highlighting;
			protected set
			{
				_highlighting = value;

				HighlightEntryInFocus();
			}
		}

		/// <summary>
		/// Index of the entry currently in focus. Could be out of range, in which case, there is no entry in focus.
		/// </summary>
		public int EntryInFocusIndex
		{
			get => _entryInFocusIndex;
			protected set
			{
				if (EntryInFocusExists)
				{
					EntryInFocus.InFocus = false;
				}

				_entryInFocusIndex = value;     //Switch focus to new entry

				if (EntryInFocusExists && InFocus)
				{
					EntryInFocus.InFocus = true;
				}

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

		/// <summary>
		/// Filter that decides which entries should be highlighted.
		/// </summary>
		public Predicate<EntryType> HighlightingFilter { get; set; }

		protected int FilesViewWindowTop
		{
			get => pane.ViewPanel.VerticalScroll.Value;
			set => pane.ViewPanel.VerticalScroll.Value = value;
		}
		protected List<EntryType> Entries { get; set; }
		protected bool EntryInFocusExists => EntryInFocusIndex < Entries.Count && EntryInFocusIndex >= 0;


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

			if (index == -1)
			{
				return false;
			}

			Entries[index] = updatedEntry;
			return true;
		}
		public virtual bool Remove(Predicate<EntryType> filter)
		{
			var item = Entries.Find(filter);
			return item != null && Entries.Remove(item);
		}
		public int FindIndex(Predicate<EntryType> filter)
		{
			return Entries.FindIndex(filter);
		}
		public EntryType Find(Predicate<EntryType> filter)
		{
			return Entries.Find(filter);
		}

		/// <summary>
		/// Processes key press.
		/// </summary>
		/// <param name="pressedKey">Pressed key.</param>
		/// <returns>True if the event was handled, false if otherwise.</returns>
		public virtual bool ProcessKeyPress(InputKey pressedKey)
		{
			if (!EntryInFocusExists)
			{
				return false;
			}

			if (pressedKey == 'j' || pressedKey == Keys.Down)
			{
				if (EntryInFocusIndex < Entries.Count - 1)
				{
					EntryInFocusIndex++;
				}

				return true;
			}

			if (pressedKey == 'k' || pressedKey == Keys.Up)
			{
				if (EntryInFocusIndex > 0)
				{
					EntryInFocusIndex--;
				}

				return true;
			}

			if (pressedKey == 'g' || pressedKey == Keys.Home)
			{
				if (Highlighting)
				{
					EntryInFocus.InFocus = false;
					_entryInFocusIndex--;
					while (_entryInFocusIndex > 0)
					{
						HighlightEntryInFocus();
						_entryInFocusIndex--;
					}
				}

				EntryInFocusIndex = 0;
				return true;
			}

			if (pressedKey == 'G' || pressedKey == Keys.End)
			{
				if (Highlighting)
				{
					EntryInFocus.InFocus = false;
					_entryInFocusIndex++;
					while (_entryInFocusIndex < Entries.Count - 1)
					{
						HighlightEntryInFocus();
						_entryInFocusIndex++;
					}
				}
				EntryInFocusIndex = Entries.Count - 1;
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
			if (!EntryInFocusExists)
			{
				return;
			}

			var entryInFocusTop = EntryInFocusIndex * EntryInFocus.Height;

			while (entryInFocusTop < FilesViewWindowTop)
			{
				FilesViewWindowTop -= Math.Min(EntryInFocus.Height, FilesViewWindowTop);
			}

			while (entryInFocusTop + EntryInFocus.Height > (FilesViewWindowTop + pane.ViewPanel.Height))
			{
				FilesViewWindowTop += Math.Min(EntryInFocus.Height, pane.ViewPanel.VerticalScroll.Maximum - (FilesViewWindowTop + pane.ViewPanel.Height - 1));
			}
		}
		protected void HighlightEntryInFocus()
		{
			if (!Highlighting || !EntryInFocusExists || !HighlightingFilter(EntryInFocus))
			{
				return;
			}

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
