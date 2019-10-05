using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MultithreadedFileOperations
{
	public struct SearchSettings
	{
		public SearchSettings(SearchTarget fileSystemNodeName, DirectoryInfo inDir, bool searchSubdirecotries)
		{
			Target = fileSystemNodeName;
			SearchSubdirectories = searchSubdirecotries;
			InDirectory = inDir;
		}

		public SearchTarget Target{ get; private set; }
		public bool SearchSubdirectories { get; private set; }
		public DirectoryInfo InDirectory { get; private set; }
	}
}
