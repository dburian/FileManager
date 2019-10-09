using System.Windows.Forms;

namespace FileManager
{
	public interface IPane
	{
		bool InFocus { get; set; }
		Control GetControl();
	}
}
