using MultithreadedFileSystemOperations;
using System;
using System.IO;


namespace FileManager
{
	public static class IJobViewExtensions
	{
		public static JobTypeDescription GetJobTypeDescription(this IJobView jobView)
		{
			if (jobView.Type == JobType.Delete)
			{
				if (jobView.GetArgumentsView().DeleteArguments.Target.GetType() == typeof(DirectoryInfo))
				{
					return JobTypeDescription.DirDelete;
				}
				else
				{
					return JobTypeDescription.FileDelete;
				}
			}

			if (jobView.Type == JobType.DirTransfer)
			{
				if (jobView.GetArgumentsView().DirectoryTransferArguments.Settings == TransferSettings.DeleteOriginal)
				{
					return JobTypeDescription.DirMove;
				}
				else
				{
					return JobTypeDescription.DirCopy;
				}
			}

			if (jobView.Type == JobType.FileTransfer)
			{
				if (jobView.GetArgumentsView().FileTransferArguments.Settings == TransferSettings.DeleteOriginal)
				{
					return JobTypeDescription.FileMove;
				}
				else
				{
					return JobTypeDescription.FileCopy;
				}
			}

			throw new ArgumentOutOfRangeException("New JobTypeDescription?");
		}
	}
}
