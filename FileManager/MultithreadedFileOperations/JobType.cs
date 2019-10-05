using System;
using System.Collections.Generic;
using System.Text;

namespace MultithreadedFileOperations
{
	public enum JobType
	{
		FileCopy,
		FileMove,
		Delete,
		DirCopy,
		DirMove
	}
}
