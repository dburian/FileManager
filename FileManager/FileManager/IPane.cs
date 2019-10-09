using System.Windows.Forms;

namespace FileManager
{
	/// <summary>
	/// Most basic and general interface to a pane.
	/// </summary>
	public interface IPane
	{
		bool InFocus { get; set; }
		Control GetControl();
	}
}
