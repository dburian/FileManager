using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace HelperExtensionLibrary
{
	public static class StringExtensions
	{
		public static string GetCamelCasedPath(this string path)
		{
			var separators = new char[] { Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar};
			string[] pathSplit = path.Split(separators);
			string result = "";

			foreach (var word in pathSplit)
			{
				result += Capitalize(word);
				result += Path.DirectorySeparatorChar;
			}

			return result;
		}

		public static bool IsDirectoryPath(this string path)
		{
			return path[path.Length -1] == Path.AltDirectorySeparatorChar || path[path.Length - 1] == Path.DirectorySeparatorChar;
		}

		private static string Capitalize(string word)
		{
			if (word.Length == 0) return word;
			
			var charArr = word.ToCharArray();
			charArr[0] = char.ToUpperInvariant(charArr[0]);
			return new string(charArr);
		}
	}
}
