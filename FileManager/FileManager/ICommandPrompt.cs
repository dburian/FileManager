using System.Drawing;

namespace FileManager
{
	public interface ICommandPrompt
	{
		string Command { get; set; }
		bool InFocus { get; set; }

		Font Font { get; }
		int Width { get; }
	}
}
