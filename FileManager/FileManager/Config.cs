using System.Drawing;
using System.IO;

namespace FileManager
{
	/// <summary>
	/// Main settings of the application. In the future, loading from json should be considered.
	/// </summary>
	internal static class Config
	{
		public static string DefaultPath { get; } = @"C:/";

		public static FilesPanePresenter DefaultFilesPanePresenter => new FilesPanePresenter(new FilesPane(), new DirectoryInfo(Config.DefaultPath));
		public static JobsPanePresenter DefaultJobsPanePresenter => new JobsPanePresenter(new JobsPane());

		public static IPanePresenter DefaultLeftPanePresenter => DefaultFilesPanePresenter;
		public static IPanePresenter DefaultRightPanePresenter => DefaultFilesPanePresenter;

		public static string DefaultCommandPrompt { get; } = "Type colon to enter a command";
		public static class ColorPalette
		{
			public static Color White { get; } = Color.White;
			public static Color Black { get; } = Color.Black;
			public static Color Grey { get; } = Color.FromArgb(240, 240, 240);
			public static Color HighlightedLight { get; } = Color.FromArgb(240, 255, 150);
			public static Color HighlightedDark { get; } = Color.FromArgb(240, 255, 120);
		}
	}
}
