using System.Windows.Forms;

namespace FileManager
{
	/// <summary>
	/// Represents an view of arguments of some job.
	/// </summary>
	public interface IJobArgsView
	{
		Control GetControl();
	}
}
