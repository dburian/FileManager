using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileManager
{
	public static class DirectoryInfoExtensions
	{
		public static string GetOnlyName(this DirectoryInfo directoryInfo)
		{
			return (directoryInfo.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden ? "." + directoryInfo.Name : directoryInfo.Name;
		}
	}
}
