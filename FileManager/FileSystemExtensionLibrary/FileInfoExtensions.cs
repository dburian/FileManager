using System.IO;

namespace HelperExtensionLibrary
{
	public static class FileInfoExtensions
	{
		public static string GetOnlyName(this FileInfo fileInfo)
		{
			string name = (fileInfo.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden ? "." : "";
			name += fileInfo.Name;

			return CommonMethods.ForgetExtension(name);
		}

		public static string GetOnlyExtension(this FileInfo fileInfo)
		{
			return Path.GetExtension(fileInfo.Name);
		}

		public static string GetRelativePath(this FileInfo fileInfo, string relativeTo)
		{
			return CommonMethods.GetRelativePath(fileInfo.FullName, relativeTo);
		}
	}
}
