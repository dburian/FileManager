using System.Drawing;
using System.Windows.Forms;

namespace FileManager
{
	/// <summary>
	/// Represents an view of arguments of some job.
	/// </summary>
	public interface IJobArgsView
	{
		Control GetControl();
		/// <summary>
		/// Sets background color of the underlying control to <paramref name="color"/>
		/// </summary>
		void SetBackgroundColor(Color color);
	}
}
