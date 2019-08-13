using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace FileManager
{
	static class Config
	{
		public static string DefaultPath { get; private set; } = @"C:/";
		public static IPanePresenter DefaultLeftPanePresenter { get; private set; } = new FilesPanePresenter(new FilesPane(), new DirectoryInfo(Config.DefaultPath));
		public static IPanePresenter DefaultRightPanePresenter { get; private set; } = new FilesPanePresenter(new FilesPane(), new DirectoryInfo(Config.DefaultPath));


		public static class ColorPalette
		{
			public static Color White { get; private set; } = Color.White;
			public static Color Black { get; private set; } = Color.Black;
			public static Color Grey { get; private set; } = Color.FromArgb(240, 240, 240);
			public static Color HighlightedLight { get; private set; } = Color.FromArgb(240, 255, 150);
			public static Color HighlightedDark { get; private set; } = Color.FromArgb(240, 255, 120);
		}
	}
}
