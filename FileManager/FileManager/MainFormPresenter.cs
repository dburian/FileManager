using HelperExtensionLibrary;
using MultithreadedFileOperations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager
{
	/// <summary>
	/// Controls the main form through IMainForm interface.
	/// </summary>
	internal class MainFormPresenter : IDisposable
	{
		private IPanePresenter _leftPresenter;
		private IPanePresenter _rightPresenter;
		private CommandPromptPresenter _commandPresenter;
		private bool _commandPromptInFocus = false;
		private PaneArea _paneInFocusArea;
		private bool _fullScreenMode = false;
		private readonly IMainForm form;

		public MainFormPresenter(IMainForm mainForm)
		{
			form = mainForm;

			LeftPresenter = Config.DefaultLeftPanePresenter;
			RightPresenter = Config.DefaultRightPanePresenter;

			PaneAreaInFocus = PaneArea.Left;

			CommandPromptPresenter = new CommandPromptPresenter(form.CommandPrompt);

			form.ProcessKeyPressEvent += ProcessKeyPress;
			form.FormClosing += OnFormClosing;
		}

		/// <summary>
		/// Sets left pane and disposes old one.
		/// </summary>
		private IPanePresenter LeftPresenter
		{
			get => _leftPresenter;
			set
			{
				if (_leftPresenter != null)
				{
					_leftPresenter.InvokeCommand -= ProcessCommand;
					_leftPresenter.Dispose();
				}

				_leftPresenter = value;

				form.LeftPane = _leftPresenter.GetViewsControl();
				_leftPresenter.InvokeCommand += ProcessCommand;
			}
		}

		/// <summary>
		/// Sets right pane and disposes old one.
		/// </summary>
		private IPanePresenter RightPresenter
		{
			get => _rightPresenter;
			set
			{
				if (_rightPresenter != null)
				{
					_rightPresenter.InvokeCommand -= ProcessCommand;
					_rightPresenter.Dispose();
				}

				_rightPresenter = value;

				form.RightPane = _rightPresenter.GetViewsControl();
				_rightPresenter.InvokeCommand += ProcessCommand;
			}
		}

		/// <summary>
		/// Sets the command prompt.
		/// </summary>
		private CommandPromptPresenter CommandPromptPresenter
		{
			get => _commandPresenter;
			set
			{
				_commandPresenter = value;
				_commandPresenter.ProcessCommand += ProcessCommand;
			}
		}

		/// <summary>
		/// Switches focus between command prompt and panes.
		/// </summary>
		private bool CommandPromptInFocus
		{
			get => _commandPromptInFocus;
			set
			{
				_commandPromptInFocus = value;

				PanePresenterInFocus.SetFocusOnView(!_commandPromptInFocus);
				CommandPromptPresenter.SetFocusOnView(_commandPromptInFocus);
			}
		}

		/// <summary>
		/// Helper property to easily switch focus between the two panes.
		/// </summary>
		private PaneArea PaneAreaInFocus
		{
			get => _paneInFocusArea;
			set
			{
				PanePresenterInFocus.SetFocusOnView(false);
				_paneInFocusArea = value;
				PanePresenterInFocus.SetFocusOnView(true);
			}
		}


		/// <summary>
		/// Changes arrangement of the main form, making the current pane in focus span across the whole window.
		/// </summary>
		private bool FullScreenMode
		{
			get => _fullScreenMode;
			set
			{
				_fullScreenMode = value;

				switch (PaneAreaInFocus)
				{
					case PaneArea.Left:
						form.FullScreenLeft = _fullScreenMode;
						break;
					case PaneArea.Right:
						form.FullScreenRight = _fullScreenMode;
						break;

					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}

		/// <summary>
		/// Pointer to the current presenter in focus
		/// </summary>
		private IPanePresenter PanePresenterInFocus
		{
			get
			{
				switch (PaneAreaInFocus)
				{
					case PaneArea.Left:
						return LeftPresenter;
					case PaneArea.Right:
						return RightPresenter;

					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}

		/// <summary>
		/// Pointer to the currently inactive presenter (not in focus)
		/// </summary>
		private IPanePresenter OtherPanePresenter
		{
			get
			{
				switch (PaneAreaInFocus)
				{
					case PaneArea.Left:
						return RightPresenter;
					case PaneArea.Right:
						return LeftPresenter;

					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}
		public void Dispose()
		{
			_rightPresenter.Dispose();
			_leftPresenter.Dispose();
		}

		private void OnFormClosing(object sender, FormClosingEventArgs e)
		{
			JobsPool.CancelAllAndDispose();
			Dispose();
		}

		/// <summary>
		/// Causes to switch focus from the pane currently in focus to that specified by <paramref name="area"/>.
		/// </summary>
		/// <param name="area">Area of the pane to which the focus should be switched.</param>
		private void SwitchPaneInFocus(PaneArea area)
		{
			if (FullScreenMode)
			{
				FullScreenMode = false;
				PaneAreaInFocus = area;
				FullScreenMode = true;
			}
			else
			{
				PaneAreaInFocus = area;
			}
		}

		/// <summary>
		/// Causes to switch focus to the other pane.
		/// </summary>
		private void SwitchPaneInFocus()
		{
			SwitchPaneInFocus(PaneAreaInFocus == PaneArea.Left ? PaneArea.Right : PaneArea.Left);
		}

		/// <summary>
		/// Processes key press.
		/// </summary>
		/// <param name="keyChar">Key that was pressd</param>
		/// <returns>True if the event was handled, false otherwise.</returns>
		private bool ProcessKeyPress(InputKey keyChar)
		{
			if (keyChar == ':' || keyChar == '/')
			{
				if (!CommandPromptInFocus)
				{
					CommandPromptInFocus = true;
				}
			}

			if (CommandPromptInFocus && CommandPromptPresenter.ProcessKeyPress(keyChar))
			{
				return true;
			}

			if (!CommandPromptInFocus && PanePresenterInFocus.ProcessKeyPress(keyChar))
			{
				return true;
			}

			// Focus switches
			if (keyChar == Keys.Tab)
			{
				SwitchPaneInFocus();
				return true;
			}
			if (keyChar == Keys.Escape)
			{
				if (CommandPromptInFocus)
				{
					CommandPromptInFocus = false;
					return true;
				}
				else if (PanePresenterInFocus.GetType() == typeof(SearchResultPanePresenter))
				{
					switch (PaneAreaInFocus)
					{
						case PaneArea.Left:
							LeftPresenter = Config.DefaultLeftPanePresenter;
							break;
						case PaneArea.Right:
							RightPresenter = Config.DefaultRightPanePresenter;
							break;

						default:
							throw new ArgumentOutOfRangeException();
					}

					PanePresenterInFocus.SetFocusOnView(true);
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Processes command.
		/// </summary>
		/// <param name="cmd">Command to be processed.</param>
		/// <returns>True if the event was handled, false otherwise.</returns>
		private bool ProcessCommand(ICommand cmd)
		{
			if (cmd.GetType() == typeof(CopyCommand))
			{
				HandleTransferCommand((CopyCommand)cmd);
				return true;
			}
			else if (cmd.GetType() == typeof(MoveCommand))
			{
				HandleTransferCommand((MoveCommand)cmd);
				return true;
			}
			else if (cmd.GetType() == typeof(DeleteCommand))
			{
				HandleDeleteCommand((DeleteCommand)cmd);
				return true;
			}
			else if (cmd.GetType() == typeof(SearchCommand))
			{
				HandleSearchCommand((SearchCommand)cmd);
				return true;
			}
			else if (cmd.GetType() == typeof(ChangeDirectoryCommand))
			{
				HandleChangeDirectoryCommand((ChangeDirectoryCommand)cmd);
				return true;
			}
			else if (cmd.GetType() == typeof(SortCommand))
			{
				HandleSortCommand((SortCommand)cmd);
				return true;
			}
			else if (cmd.GetType() == typeof(LeftCommand))
			{
				HandleLeftCommand((LeftCommand)cmd);
				return true;
			}
			else if (cmd.GetType() == typeof(RightCommand))
			{
				HandleRightCommand((RightCommand)cmd);
				return true;
			}
			else if (cmd.GetType() == typeof(FullScreenCommand))
			{
				FullScreenMode ^= true; //toggle FullScreenMode
				ResetCommandPromptIfNecessary();
				return true;
			}
			else if (cmd.GetType() == typeof(UnknownCommand))
			{
				ErrorFormFactory.CreateFromException(new UnknownCommandException((UnknownCommand)cmd)).ShowAsDialog();
				return true;
			}
			else
			{
				return false;
			}
		}

		#region CommandHanlders
		private void HandleTransferCommand(ITransferCommand cmd)
		{
			if (!FilesPaneActiveCheck(cmd, out FilesPanePresenter activeFilesPresenter))
			{
				return;
			}

			var selectedInfos = activeFilesPresenter.GetSelectedFileSystemInfos();
			if (selectedInfos == null)
			{
				ErrorFormFactory.CreateFromException(new FileOrDirectoryMustBeSelectedException(cmd)).ShowAsDialog();
				return;
			}

			var transferSettings = cmd.GetType() == typeof(CopyCommand) ? TransferSettings.None : TransferSettings.DeleteOriginal;

			if (cmd.To == null)
			{
				if (!BothPanesAreFilePanesCheck(cmd, out activeFilesPresenter, out FilesPanePresenter inactiveFilesPresenter))
				{
					return;
				}

				WithoutTargetName(selectedInfos, inactiveFilesPresenter.CurrentDir);
				return;
			}

			SeparateFileSystemInfos(selectedInfos, out List<FileInfo> files, out List<DirectoryInfo> dirs);

			var destPath = RootIfIsnt(cmd.To, activeFilesPresenter.CurrentDir.FullName);
			if (Format.IsDirectoryPath(destPath))
			{
				var destDir = new DirectoryInfo(destPath);
				if (files.Count == 0)
				{
					if (destDir.Exists)
					{
						Task.Run(() => Operations.TransferDirectories(dirs, destDir, transferSettings));
					}
					else if (dirs.Count == 1)
					{
						Task.Run(() => Operations.TransferDirectories((dirs[0], destDir).AsSingleEnumerable(), transferSettings));
					}
					else
					{
						Task.Run(() => Operations.TransferDirectories(dirs, destDir.Parent, destDir.Name, transferSettings));
					}

					ResetCommandPromptIfNecessary();
					return;
				}
				else
				{
					WithoutTargetName(selectedInfos, new DirectoryInfo(destPath));
					return;
				}
			}

			if (dirs.Count > 0)
			{
				ErrorFormFactory.CreateFromException(new TransferDirectoryIntoFileException(cmd, dirs[0].FullName, destPath)).ShowAsDialog();
				return;
			}

			var destFile = new FileInfo(destPath);
			if (files.Count == 1)
			{
				Task.Run(() => Operations.TransferFiles((files[0], destFile).AsSingleEnumerable(), transferSettings));
			}
			else
			{
				Task.Run(() => Operations.TransferFiles(files, destFile.Directory, destFile.Name, transferSettings));
			}

			ResetCommandPromptIfNecessary();


			void WithoutTargetName(IEnumerable<FileSystemInfo> infos, DirectoryInfo targetDir)
			{
				SeparateFileSystemInfos(infos, out List<FileInfo> fileInfos, out List<DirectoryInfo> dirInfos);

				Task.Run(() => Operations.TransferFiles(fileInfos, targetDir, transferSettings));
				Task.Run(() => Operations.TransferDirectories(dirInfos, targetDir, transferSettings));

				ResetCommandPromptIfNecessary();
			}
		}

		private void HandleDeleteCommand(DeleteCommand cmd)
		{
			IEnumerable<FileSystemInfo> targets;
			if (cmd.TargetPath == null)
			{
				if (!FilesPaneActiveCheck(cmd, out FilesPanePresenter activeFilesPresenter))
				{
					return;
				}

				targets = activeFilesPresenter.GetSelectedFileSystemInfos();
				if (targets == null)
				{
					ErrorFormFactory.CreateFromException(new FileOrDirectoryMustBeSelectedException(cmd)).ShowAsDialog();
					return;
				}
			}
			else
			{
				var root = (PanePresenterInFocus as FilesPanePresenter)?.CurrentDir?.FullName;
				if (root == null)
				{
					root = @"C:\";
				}

				var path = RootIfIsnt(cmd.TargetPath, root);
				if (Format.IsDirectoryPath(path))
				{
					targets = new DirectoryInfo(path).AsSingleEnumerable();
				}
				else
				{
					targets = new FileInfo(path).AsSingleEnumerable();
				}
			}

			Task.Run(() => Operations.DeleteFileSystemNodes(targets));

			ResetCommandPromptIfNecessary();
		}

		private void HandleSearchCommand(SearchCommand cmd)
		{
			var root = (PanePresenterInFocus as FilesPanePresenter)?.CurrentDir?.FullName;
			if (root == null)
			{
				root = @"C:\";
			}

			DirectoryInfo searchedDir = new DirectoryInfo(RootIfIsnt(cmd.SearchedDirectory, root));

			if (!searchedDir.Exists)
			{
				ErrorFormFactory.CreateFromException(new SearchedDirectoryDoesNotExist(searchedDir.FullName)).ShowAsDialog();
				return;
			}

			var searchSettings = new SearchSettings(new SearchTarget(cmd.SearchedName), searchedDir, true);
			var searchView = Operations.SearchFor(searchSettings);

			ISearchResultPane pane = new SearchResultPane();
			var presenter = new SearchResultPanePresenter(pane, searchView);

			switch (PaneAreaInFocus)
			{
				case PaneArea.Left:
					LeftPresenter = presenter;
					break;
				case PaneArea.Right:
					RightPresenter = presenter;
					break;
			}

			ResetCommandPromptIfNecessary();
		}

		private void HandleChangeDirectoryCommand(ChangeDirectoryCommand cmd)
		{
			FilesPanePresenter activePresenter;
			if (PanePresenterInFocus.GetType() != typeof(FilesPanePresenter))
			{
				activePresenter = new FilesPanePresenter(new FilesPane(), new DirectoryInfo(Config.DefaultPath));
				activePresenter.SetFocusOnView(true);

				switch (PaneAreaInFocus)
				{
					case PaneArea.Left:
						LeftPresenter = activePresenter;
						break;
					case PaneArea.Right:
						RightPresenter = activePresenter;
						break;
				}
			}
			else
			{
				activePresenter = (FilesPanePresenter)PanePresenterInFocus;
			}

			var dirInfo = new DirectoryInfo(RootIfIsnt(cmd.TargetPath, activePresenter.CurrentDir.FullName));

			if (!dirInfo.Exists)
			{
				ErrorFormFactory.CreateFromException(new FileOrDirectoryNotFound(dirInfo)).ShowAsDialog();
				return;
			}

			activePresenter.ChangeDirectory(dirInfo);
			ResetCommandPromptIfNecessary();
		}

		private void HandleSortCommand(SortCommand cmd)
		{
			if (!FilesPaneActiveCheck(cmd, out FilesPanePresenter activeFilesPresenter))
			{
				return;
			}

			activeFilesPresenter.SetEntrySortOrder(cmd.Comparer);
			ResetCommandPromptIfNecessary();
		}

		private void HandleLeftCommand(LeftCommand cmd)
		{
			switch (cmd.Pane)
			{
				case Panes.Files:
					LeftPresenter = Config.DefaultFilesPanePresenter;
					break;
				case Panes.Jobs:
					LeftPresenter = Config.DefaultJobsPanePresenter;
					break;
				default:
					throw new InvalidDataException("MainFormPresenter.ProcessCommand....LeftCommand");
			}

			ResetCommandPromptIfNecessary();
		}

		private void HandleRightCommand(RightCommand cmd)
		{
			RightCommand rightCommand = cmd;

			switch (rightCommand.Pane)
			{
				case Panes.Files:
					RightPresenter = Config.DefaultFilesPanePresenter;
					break;
				case Panes.Jobs:
					RightPresenter = Config.DefaultJobsPanePresenter;

					break;
				default:
					throw new InvalidDataException("MainFormPresenter.ProcessCommand....RightCommand");
			}

			ResetCommandPromptIfNecessary();
		}

		private void ResetCommandPromptIfNecessary()
		{
			if (!CommandPromptInFocus)
			{
				return;
			}

			CommandPromptPresenter.ResetCommandPrompt();
			CommandPromptInFocus = false;
		}

		private bool FilesPaneActiveCheck(ICommand cmd, out FilesPanePresenter activeFilesPresenter)
		{
			activeFilesPresenter = PanePresenterInFocus as FilesPanePresenter;
			if (activeFilesPresenter == null)
			{
				ErrorFormFactory.CreateFromException(new FilesPaneWasNotActiveException(cmd)).ShowAsDialog();
				return false;
			}

			return true;
		}

		private bool BothPanesAreFilePanesCheck(ICommand cmd, out FilesPanePresenter activeFilesPresenter, out FilesPanePresenter inactiveFilesPresenter)
		{
			activeFilesPresenter = PanePresenterInFocus as FilesPanePresenter;
			inactiveFilesPresenter = OtherPanePresenter as FilesPanePresenter;
			if (activeFilesPresenter == null || inactiveFilesPresenter == null)
			{
				ErrorFormFactory.CreateFromException(new BothPanesNeedToBeFilesPaneException(cmd)).ShowAsDialog();
				return false;
			}

			return true;
		}

		private static void SeparateFileSystemInfos(IEnumerable<FileSystemInfo> all, out List<FileInfo> files, out List<DirectoryInfo> dirs)
		{
			files = new List<FileInfo>();
			dirs = new List<DirectoryInfo>();

			foreach (var info in all)
			{
				if (info.GetType() == typeof(DirectoryInfo))
				{
					dirs.Add((DirectoryInfo)info);
				}
				else
				{
					files.Add((FileInfo)info);
				}
			}
		}

		private static string RootIfIsnt(string path, string root)
		{
			if (!Path.IsPathRooted(path))
			{
				return Path.Combine(root, path);
			}

			return path;
		}
		#endregion
	}

}
