using System;
using System.Collections.Generic;
using System.Text;

namespace MultithreadedFileOperations
{
	public enum JobChangeEvent
	{
		BeforeRun,
		AfterCompleted,
		OnProgressChange,
		OnExceptionRaise,
		Canceled
	}
}
