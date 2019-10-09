using System;
using System.IO;

namespace FileManager
{
	/// <summary>
	/// Class that contains all format methods specific to this application.
	/// </summary>
	internal static class Format
	{
		public static string GetCamelCasedPath(string path)
		{
			bool wasDir = IsDirectoryPath(path);
			var separators = new char[] { Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar };
			string[] pathSplit = path.Split(separators, System.StringSplitOptions.RemoveEmptyEntries);
			string result = "";

			foreach (var word in pathSplit)
			{
				result += Capitalize(word);
				result += Path.DirectorySeparatorChar;
			}

			if (!wasDir)
			{
				result = result.TrimEnd(Path.DirectorySeparatorChar);
			}

			return result;
		}

		public static bool IsDirectoryPath(string path)
		{
			return path[path.Length - 1] == Path.AltDirectorySeparatorChar || path[path.Length - 1] == Path.DirectorySeparatorChar;
		}

		public static string Size(long size)
		{
			string[] units = { "B", "KiB", "MiB", "GiB", "TiB", "PiB", "EiB" };
			int unitIndex = 0;
			while (size > (1024 * 64) && unitIndex < units.Length - 1)
			{
				size /= 1024;
				unitIndex++;
			}

			return $"{size} {units[unitIndex]}";
		}

		public static string DateAndTime(DateTime dt)
		{
			return $"{dt.Day}-{dt.Month}-{dt.Year % 100} {dt.Hour:00}:{dt.Minute:00}";
		}

		private static string Capitalize(string word)
		{
			if (word.Length == 0)
			{
				return word;
			}

			var charArr = word.ToCharArray();
			charArr[0] = char.ToUpperInvariant(charArr[0]);
			return new string(charArr);
		}
	}
}
