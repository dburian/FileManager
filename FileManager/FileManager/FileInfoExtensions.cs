using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileManager
{
	static class FileInfoExtensions
	{
		public static string GetOnlyName(this FileInfo fileInfo)
		{
			string name = (fileInfo.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden ? "." : "";
			name += fileInfo.Name;

			int lastDot = name.LastIndexOf('.');

			// if name starts with a dot and does not contain any other dot 
			// or 
			// does not contain a dot
			if (lastDot <= 0) return name;

			return name.Substring(0, lastDot);
		}

		public static string GetOnlyExtension(this FileInfo fileInfo)
		{
			string name = fileInfo.Name;

			int lastDot = name.LastIndexOf('.');

			if (lastDot <= 0 || lastDot == name.Length - 1) return "";

			return name.Substring(lastDot + 1);
		}
	}
}
