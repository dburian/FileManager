using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace FileManager
{
	static class Format
	{
		public static string Size(long size)
		{
			string[] units = { "B", "KiB", "MiB", "GiB", "TiB", "PiB", "EiB" };
			int unitIndex = 0;
			while (size > (1024 * 64) && unitIndex < units.Length - 1 )
			{
				size /= 1024;
				unitIndex++;
			}

			return $"{size} {units[unitIndex]}";
		}

		public static string DateAndTime(DateTime dt) => $"{dt.Day}-{dt.Month}-{dt.Year % 100} {dt.Hour:00}:{dt.Minute:00}";
	}
}
