using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace HelperExtensionLibrary
{
	internal static class CommonMethods
	{
		public static string GetRelativePath(string ofWhat, string relativeTo)
		{
			string[] ofWhatPath = ofWhat.Split(Path.DirectorySeparatorChar);
			if (Path.GetExtension(ofWhat) != "") ofWhatPath[ofWhatPath.Length - 1] = ForgetExtension(Path.GetFileName(ofWhat));

			string[] folderPath = relativeTo.Split(Path.DirectorySeparatorChar);

			int i;
			for (i = 0; i < folderPath.Length; i++)
			{
				if (folderPath[i] == "") break;
				if (ofWhatPath[i] != folderPath[i]) throw new ArgumentException();
			}

			string relativePath = "";
			for (int j = i; j < ofWhatPath.Length; j++)
			{
				relativePath += ofWhatPath[j];
				if (j != ofWhatPath.Length - 1) relativePath += Path.DirectorySeparatorChar;
			}

			return relativePath;
		}

		public static string ForgetExtension(string fileName)
		{
			int lastDot = fileName.LastIndexOf('.');

			// if name starts with a dot and does not contain any other dot 
			// or 
			// does not contain a dot
			if (lastDot <= 0) return fileName;

			return fileName.Substring(0, lastDot);
		}
	}
}
