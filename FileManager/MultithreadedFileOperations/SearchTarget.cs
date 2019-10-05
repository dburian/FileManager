using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using HelperExtensionLibrary;

namespace MultithreadedFileOperations
{
	public class SearchTarget : IEquatable<FileInfo>, IEquatable<DirectoryInfo>
	{

		public SearchTarget(string fileSystemNode)
		{
			Name = fileSystemNode;
		}

		public string Name { get; private set; }

		public static bool operator==(SearchTarget target, FileInfo fileInfo) => target.Equals(fileInfo);
		public static bool operator !=(SearchTarget target, FileInfo fileInfo) => !target.Equals(fileInfo);

		public static bool operator ==(SearchTarget target, DirectoryInfo dirInfo) => target.Equals(dirInfo);
		public static bool operator !=(SearchTarget target, DirectoryInfo dirInfo) => !target.Equals(dirInfo);

		public static bool operator ==(FileInfo fileInfo, SearchTarget target) => target.Equals(fileInfo);
		public static bool operator !=(FileInfo fileInfo, SearchTarget target) => !target.Equals(fileInfo);

		public static bool operator ==(DirectoryInfo dirInfo, SearchTarget target) => target.Equals(dirInfo);
		public static bool operator !=(DirectoryInfo dirInfo, SearchTarget target) => !target.Equals(dirInfo);

		public bool Equals(FileInfo other)
		{
			return other.Name.IndexOf(Name, StringComparison.OrdinalIgnoreCase) >= 0;
		}

		public bool Equals(DirectoryInfo other)
		{
			return other.Name.IndexOf(Name, StringComparison.OrdinalIgnoreCase) >= 0;
		}

		public override bool Equals(object obj)
		{
			var fileI = obj as FileInfo;

			if (fileI != null) return Equals(fileI);

			var dirI = obj as DirectoryInfo;

			if (dirI != null) return Equals(dirI);

			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
