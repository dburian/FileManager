using System;
using System.IO;

namespace MultithreadedFileSystemOperations
{
	/// <summary>
	/// Represents a file system node which is looked for.
	/// </summary>
	public class SearchTarget : IEquatable<FileInfo>, IEquatable<DirectoryInfo>
	{

		public SearchTarget(string fileSystemNode)
		{
			Name = fileSystemNode;
		}

		public string Name { get; }

		public static bool operator ==(SearchTarget target, FileInfo fileInfo)
		{
			return object.ReferenceEquals(target, null) ? false : target.Equals(fileInfo);
		}

		public static bool operator !=(SearchTarget target, FileInfo fileInfo)
		{
			return object.ReferenceEquals(target, null) ? true : !target.Equals(fileInfo);
		}

		public static bool operator ==(SearchTarget target, DirectoryInfo dirInfo)
		{
			return object.ReferenceEquals(target, null) ? false : target.Equals(dirInfo);
		}

		public static bool operator !=(SearchTarget target, DirectoryInfo dirInfo)
		{
			return object.ReferenceEquals(target, null) ? true : !target.Equals(dirInfo);
		}

		public static bool operator ==(FileInfo fileInfo, SearchTarget target)
		{
			return object.ReferenceEquals(target, null) ? false : target.Equals(fileInfo);
		}

		public static bool operator !=(FileInfo fileInfo, SearchTarget target)
		{
			return object.ReferenceEquals(target, null) ? true : !target.Equals(fileInfo);
		}

		public static bool operator ==(DirectoryInfo dirInfo, SearchTarget target)
		{
			return object.ReferenceEquals(target, null) ? false : target.Equals(dirInfo);
		}

		public static bool operator !=(DirectoryInfo dirInfo, SearchTarget target)
		{
			return object.ReferenceEquals(target, null) ? true : !target.Equals(dirInfo);
		}

		public bool Equals(FileInfo other)
		{
			if (other == null)
			{
				return false;
			}

			return other.Name.IndexOf(Name, StringComparison.OrdinalIgnoreCase) >= 0;
		}

		public bool Equals(DirectoryInfo other)
		{
			if (other == null)
			{
				return false;
			}

			return other.Name.IndexOf(Name, StringComparison.OrdinalIgnoreCase) >= 0;
		}

		public override bool Equals(object obj)
		{
			var fileI = obj as FileInfo;

			if (fileI != null)
			{
				return Equals(fileI);
			}

			var dirI = obj as DirectoryInfo;

			if (dirI != null)
			{
				return Equals(dirI);
			}

			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
