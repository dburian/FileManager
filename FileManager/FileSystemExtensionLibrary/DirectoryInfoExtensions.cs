using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HelperExtensionLibrary
{
	public static class DirectoryInfoExtensions
	{
		public static string GetFormattedName(this DirectoryInfo directoryInfo)
		{
			return (directoryInfo.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden ? "." + directoryInfo.Name : directoryInfo.Name;
		}

		public static string GetRelativePath(this DirectoryInfo dirInfo, string relativeTo)
		{
			return CommonMethods.GetRelativePath(dirInfo.FullName, relativeTo) + Path.DirectorySeparatorChar;
		}
	}
}
