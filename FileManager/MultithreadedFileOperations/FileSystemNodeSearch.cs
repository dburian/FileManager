using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Security;
using System.Diagnostics;

namespace MultithreadedFileOperations
{
	class FileSystemNodeSearch
	{

		CancellationToken ct;

		public FileSystemNodeSearch(SearchSettings settings, CancellationToken ct)
		{
			Settings = settings;
			this.ct = ct;
		}
		public FileSystemNodeSearch(
			SearchSettings settings,
			CancellationToken ct,
			OnFileSystemNodeFoundDelegate onNodeFound,
			OnExceptionRaiseDelegate exceptionRise)
			:this(settings, ct)
		{
			NodeFound += onNodeFound;
			ExceptionRise += exceptionRise;
		}

		public event OnFileSystemNodeFoundDelegate NodeFound;
		public event OnExceptionRaiseDelegate ExceptionRise;

		public SearchSettings Settings { get; }

		public void Start()
		{
			try
			{

				foreach (var file in Settings.InDirectory.EnumerateFiles())
				{
					if (Settings.Target == file) NodeFound?.Invoke(file);
				}
				ct.ThrowIfCancellationRequested();


				List<DirectoryInfo> dirsToBeSearched = null;
				if (Settings.SearchSubdirectories) dirsToBeSearched = new List<DirectoryInfo>();

				foreach (var dir in Settings.InDirectory.EnumerateDirectories())
				{
					if (Settings.Target == dir) NodeFound?.Invoke(dir);
					if (Settings.SearchSubdirectories && IsFolderReadable(dir)) dirsToBeSearched.Add(dir);
				}

				ct.ThrowIfCancellationRequested();


				foreach (var dir in dirsToBeSearched)
				{
					Factory.CreateFromParentSearchJob(this, dir).Start();
				}
			}
			catch (Exception e) when (e is SecurityException || e is UnauthorizedAccessException || e is DirectoryNotFoundException)
			{
				ExceptionRise?.Invoke(new FileSystemNodeSearchException(Settings.InDirectory, e));
			}
		}

		private bool IsFolderReadable(DirectoryInfo dir)
		{
			//TODO: try ACLs...
			try
			{
				foreach (var innerDir in dir.EnumerateDirectories())
					return true;
				
			}catch(Exception e) when (e is SecurityException || e is UnauthorizedAccessException || e is DirectoryNotFoundException)
			{
				Debug.WriteLine("Exception captured");
				return false;
			}

			return true;
		}

		public static class Factory
		{
			public static FileSystemNodeSearch CreateFromParentSearchJob(FileSystemNodeSearch parentDirSearch, DirectoryInfo dirToSearch)
			{
				var settings = new SearchSettings(parentDirSearch.Settings.Target, dirToSearch, true);
				return new FileSystemNodeSearch(settings, parentDirSearch.ct, parentDirSearch.NodeFound, parentDirSearch.ExceptionRise);
			}
		}
	}
}
