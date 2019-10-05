using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MultithreadedFileOperations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace MultithreadedFileOperationsTests
{
	[TestClass]
	class IOTestState
	{

		public bool ExceptionThrown { get; set; }

		public string TmpPath { get => Path.GetTempPath(); }
		public string DirWithoutRights { get => @"C:\Program Files"; }

		public FileInfo[] ExistingFiles { get; set; }
		public FileInfo[] NonExistingFiles { get; set; }
		public FileInfo NonExistentFileWithoutRights { get => new FileInfo(Path.Combine(DirWithoutRights, "fileWithoutRights.txt")); }
		public FileInfo ExistentFileWithoutRights { get => new FileInfo(Path.Combine(DirWithoutRights, @"Git\LICENSE.txt")); }

		public DirectoryInfo[] ExistingEmptyDirectories { get; set; }
		public DirectoryInfo[] ExistingNonEmptyDirectories { get; set; }
		public DirectoryInfo[] NonExistingDirectories { get; set; }
		public DirectoryInfo ExistingDirecoryWithoutReadRights { get => new DirectoryInfo(@"C:\Documents and Settings"); }
		public DirectoryInfo NonExistingDirecoryWithoutWriteRights { get => new DirectoryInfo(@"C:\Program Files\Hbbb"); }

		[ClassInitialize]
		public void Init()
		{

			ExistingFiles = new FileInfo[] {
				new FileInfo(Path.Combine(TmpPath, "existingFile1.txt")),
				new FileInfo(Path.Combine(TmpPath, "existingFile2.txt")),
				new FileInfo(Path.Combine(TmpPath, "existingFile3.txt")),
				new FileInfo(Path.Combine(TmpPath, "existingFile4.txt")),
			};


			NonExistingFiles = new FileInfo[] {
				new FileInfo(Path.Combine(TmpPath, "nonexistentFile1")),
				new FileInfo(Path.Combine(TmpPath, "nonexistentFile2")),
				new FileInfo(Path.Combine(TmpPath, "nonexistentFile3")),
				new FileInfo(Path.Combine(TmpPath, "nonexistentFile4")),
			};

			ExistingEmptyDirectories = new DirectoryInfo[] {
				new DirectoryInfo(Path.Combine(TmpPath, "existingEmptyDirectory1")),
				new DirectoryInfo(Path.Combine(TmpPath, "existingEmptyDirectory2")),
				new DirectoryInfo(Path.Combine(TmpPath, "existingEmptyDirectory3")),
				new DirectoryInfo(Path.Combine(TmpPath, "existingEmptyDirectory4")),
			};

			NonExistingDirectories = new DirectoryInfo[] {
				new DirectoryInfo(Path.Combine(TmpPath, "NonExistingDirectory1")),
				new DirectoryInfo(Path.Combine(TmpPath, "NonExistingDirectory2")),
				new DirectoryInfo(Path.Combine(TmpPath, "NonExistingDirectory3")),
				new DirectoryInfo(Path.Combine(TmpPath, "NonExistingDirectory4")),
			};

			ExistingNonEmptyDirectories = new DirectoryInfo[] {
				new DirectoryInfo(Path.Combine(TmpPath, "existingNonEmptyDirectory1")),
				new DirectoryInfo(Path.Combine(TmpPath, "existingNonEmptyDirectory2")),
				new DirectoryInfo(Path.Combine(TmpPath, "existingNonEmptyDirectory3")),
				new DirectoryInfo(Path.Combine(TmpPath, "existingNonEmptyDirectory4")),
			};

			Cleanup();

			foreach (var file in ExistingFiles)
			{
				File.WriteAllText(file.FullName, $"Contents of {file.Name}");
				file.Refresh();
				Assert.IsTrue(file.Exists);
			}

			foreach (var file in NonExistingFiles)
			{
				file.Refresh();
				Assert.IsFalse(file.Exists);
			}

			foreach (var dir in ExistingEmptyDirectories)
			{
				dir.Create();
				dir.Refresh();
				Assert.IsTrue(dir.Exists);
			}


			foreach (var dir in NonExistingDirectories)
			{
				dir.Refresh();
				Assert.IsFalse(dir.Exists);
			}


			foreach (var dir in ExistingNonEmptyDirectories)
			{
				CreateNonEmptyDir(dir);
			}

			ExceptionThrown = false;

			Assert.IsFalse(NonExistentFileWithoutRights.Exists);
			Assert.IsTrue(ExistentFileWithoutRights.Exists);

		}

		[ClassCleanup]
		public void Cleanup()
		{
			foreach (var file in ExistingFiles)
			{
				if (file == null) continue;

				file.Delete();
				file.Refresh();

				Assert.IsFalse(file.Exists);
			}

			foreach (var file in NonExistingFiles)
			{
				if (file == null) continue;

				file.Delete();
				file.Refresh();

				Assert.IsFalse(file.Exists);
			}


			foreach (var dir in ExistingEmptyDirectories)
			{
				if (dir == null) continue;

				if (dir.Exists) dir.Delete(true);
				dir.Refresh();
				Assert.IsFalse(dir.Exists);
			}

			foreach (var dir in NonExistingDirectories)
			{
				if (dir == null) continue;

				if (dir.Exists) dir.Delete(true);
				dir.Refresh();
				Assert.IsFalse(dir.Exists);
			}

			foreach (var dir in ExistingNonEmptyDirectories)
			{
				if (dir == null) continue;

				if (dir.Exists) dir.Delete(true);
				dir.Refresh();
				Assert.IsFalse(dir.Exists);
			}
		}

		public void OnExceptionDebugPrint(FileOperationException e)
		{
			Debug.WriteLine(e);
		}
		public void OnProgressDebugPrint(float progress)
		{
			Debug.WriteLine(progress);
		}
		public bool CheckDirectoryStructure(DirectoryInfo rootDir)
		{
			bool structureChecks = rootDir.Exists;
			if (!structureChecks) return false;

			foreach (var dir in ExistingEmptyDirectories)
			{
				string pathToInnerDir = Path.Combine(rootDir.FullName, dir.Name);
				structureChecks &= Directory.Exists(pathToInnerDir);
			}

			foreach (var file in ExistingFiles)
			{
				var destPath = Path.Combine(rootDir.FullName, file.Name);
				structureChecks &= File.Exists(destPath);
			}

			var destDirPath = Path.Combine(rootDir.FullName, ExistingEmptyDirectories[0].Name);
			structureChecks &= Directory.Exists(destDirPath);
			if (!structureChecks) return false;

			foreach (var file in ExistingFiles)
			{
				var destPath = Path.Combine(destDirPath, file.Name);
				structureChecks &= File.Exists(destPath);
			}

			return structureChecks;
		}

		private DirectoryInfo CreateNonEmptyDir(DirectoryInfo root)
		{
			root.Create();
			root.Refresh();
			Assert.IsTrue(root.Exists);

			foreach (var dir in ExistingEmptyDirectories)
			{
				var newDir = new DirectoryInfo(Path.Combine(root.FullName, dir.Name));
				newDir.Create();
				newDir.Refresh();
				Assert.IsTrue(newDir.Exists);
			}

			foreach (var file in ExistingFiles)
			{
				var destPath = Path.Combine(root.FullName, file.Name);
				File.Copy(file.FullName, destPath);
				Assert.IsTrue(File.Exists(destPath));
			}

			var destDirPath = Path.Combine(root.FullName, ExistingEmptyDirectories[0].Name);
			Assert.IsTrue(Directory.Exists(destDirPath));
			foreach (var file in ExistingFiles)
			{
				var destPath = Path.Combine(destDirPath, file.Name);
				File.Copy(file.FullName, destPath);

				Assert.IsTrue(File.Exists(destPath));
			}

			return root;
		}

		
	}
}
