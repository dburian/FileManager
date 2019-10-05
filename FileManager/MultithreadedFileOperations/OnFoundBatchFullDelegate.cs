using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MultithreadedFileOperations
{
	public delegate void OnFoundBatchFullDelegate(FileSystemInfo[] foundBatch);
}
