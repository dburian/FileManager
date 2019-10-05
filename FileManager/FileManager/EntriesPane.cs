using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace FileManager
{
	public abstract class EntriesPane<T> : UserControl, IPane where T: AbstractEntry
	{
		public abstract ScrollableControl ViewPanel { get; }
		public abstract bool InFocus { get; set; }

		public virtual void AddEntries(T[] entries)
		{
			ViewPanel.SuspendLayout();
			ViewPanel.Controls.AddRange(entries);

			int childIndex = 0;
			//Add to the beginning of Controls but in reversed order
			for (int i = entries.Length - 1; i >= 0; i--)
			{
				ViewPanel.Controls.SetChildIndex(entries[i], childIndex);
				childIndex++;
			}

			ViewPanel.ResumeLayout();
		}
		public virtual void AddEntry(T entry)
		{
			ViewPanel.Controls.Add(entry);
			ViewPanel.Controls.SetChildIndex(entry, ViewPanel.Controls.Count -1);
			entry.Dock = DockStyle.Top;
		}
		public virtual void RemoveEntry(T entry)
		{
			ViewPanel.Controls.Remove(entry);
		}
		public virtual int IndexOfEntry(T entry)
		{
			return ViewPanel.Controls.IndexOf(entry);
		}
		public virtual void ReplaceEntry(int i, T newValue)
		{
			ViewPanel.Controls.Remove(ViewPanel.Controls[i]);
			ViewPanel.Controls.Add(newValue);
			ViewPanel.Controls.SetChildIndex(newValue, i);
		}
		public virtual void ClearEntries()
		{
			ViewPanel.Controls.Clear();
		}
		public abstract Control GetControl();
	}
}
