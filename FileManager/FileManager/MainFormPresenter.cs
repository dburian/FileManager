using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using HelperExtensionLibrary;
using MultithreadedFileOperations;

namespace FileManager
{
	class MainFormPresenter : IDisposable
	{
		IPanePresenter _leftPresenter;
		IPanePresenter _rightPresenter;
		bool _commandPromptInFocus = false;
		PaneArea _paneInFocusArea;
		bool _fullScreenMode = false;

		IMainForm form;

		public MainFormPresenter(IMainForm mainForm)
		{
			form = mainForm;

			LeftPresenter = Config.DefaultLeftPanePresenter;
			RightPresenter = Config.DefaultRightPanePresenter;

			PaneAreaInFocus = PaneArea.Left;

			//TODO: event assignment to property
			CommandPromptPresenter = new CommandPromptPresenter(form.CommandPrompt);
			CommandPromptPresenter.ProcessCommand += ProcessCommand;

			form.ProcessKeyPressEvent += ProcessKeyPress;
		}


		// Panes
		IPanePresenter LeftPresenter
		{
			get => _leftPresenter;
			set
			{
				if (_leftPresenter != null)
				{
					_leftPresenter.ProcessComand -= ProcessCommand;
					_leftPresenter.Dispose();
				} 

				_leftPresenter = value;

				form.LeftPane = _leftPresenter.GetViewsControl();
				_leftPresenter.ProcessComand += ProcessCommand;
			}
		}
		IPanePresenter RightPresenter
		{
			get => _rightPresenter;
			set
			{
				if (_rightPresenter != null)
				{
					_rightPresenter.ProcessComand -= ProcessCommand;
					_rightPresenter.Dispose();
				}
				
				_rightPresenter = value;

				form.RightPane = _rightPresenter.GetViewsControl();
				_rightPresenter.ProcessComand += ProcessCommand;
			}
		}

		// Command prompt
		CommandPromptPresenter CommandPromptPresenter { get; set; }

		// To switch focus
		bool CommandPromptInFocus
		{
			get => _commandPromptInFocus;
			set
			{
				_commandPromptInFocus = value;

				PanePresenterInFocus.SetFocusOnView(!_commandPromptInFocus);
				CommandPromptPresenter.SetFocusOnView(_commandPromptInFocus);
			}
		}
		PaneArea PaneAreaInFocus
		{
			get => _paneInFocusArea;
			set
			{
				PanePresenterInFocus.SetFocusOnView(false);
				_paneInFocusArea = value;
				PanePresenterInFocus.SetFocusOnView(true);
			}
		}


		// View change properties
		bool FullScreenMode
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
				}
			}
		}

		// Helper properties
		IPanePresenter PanePresenterInFocus
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
						return LeftPresenter;
				}
			}
		}
		IPanePresenter OtherPresenter {
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

		/// <summary>
		/// Causes to switch focus from the pane currently in focus to that specified by argument area.
		/// </summary>
		/// <param name="area">Area of the pane to which the focus is swithed.</param>
		void SwitchPaneInFocus(PaneArea area)
		{
			if (FullScreenMode)
			{
				FullScreenMode = false;
				PaneAreaInFocus = area;
				FullScreenMode = true;
			} else
			{
				PaneAreaInFocus = area;
			}
		}

		/// <summary>
		/// Causes to switch focus to the next pane.
		/// </summary>
		void SwitchPaneInFocus() => SwitchPaneInFocus(PaneAreaInFocus == PaneArea.Left ? PaneArea.Right : PaneArea.Left);
		
		bool ProcessKeyPress(InputKey keyChar)
		{
			if (CommandPromptPresenter.ProcessKeyPress(keyChar) || PanePresenterInFocus.ProcessKeyPress(keyChar)) return true;

			if (keyChar == ':' || keyChar == '/')
			{
				if (!CommandPromptInFocus)
					CommandPromptInFocus = true;
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
					CommandPromptInFocus = false;

				//TODO: maybe remember the previous filepresenter?
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
					}

					PanePresenterInFocus.SetFocusOnView(true);
				}

				return true;
			}

			return false;
		}
		bool ProcessCommand(ICommand cmd)
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
			else return false;
		}

		#region CommandHanlders
		void HandleTransferCommand(ITransferCommand cmd)
		{
			void WithoutTargetName(IEnumerable<FileSystemInfo> infos, DirectoryInfo targetDir)
			{
				List<FileInfo> fileInfos;
				List<DirectoryInfo> dirInfos;
				SeparateFileSystemInfos(infos, out fileInfos, out dirInfos);

				Operations.TransferFiles(fileInfos, targetDir, TransferSettings.None);
				Operations.TransferDirectories(dirInfos, targetDir, TransferSettings.None);

				ResetCommandPromptIfNecessary();
			}

			FilesPanePresenter activeFilesPresenter;

			if (cmd.To == null)
			{
				FilesPanePresenter inactiveFilesPresenter;
				if (!BothPanesAreFilePanesCheck(cmd, out activeFilesPresenter, out inactiveFilesPresenter)) return;

				WithoutTargetName(activeFilesPresenter.GetSelectedFileSystemInfos(), inactiveFilesPresenter.CurrentDir);
				return;
			}

			if (!FilesPaneActiveCheck(cmd, out activeFilesPresenter)) return;

			if (cmd.To.IsDirectoryPath())
			{
				WithoutTargetName(activeFilesPresenter.GetSelectedFileSystemInfos(), new DirectoryInfo(cmd.To));
				return;
			}

			var selectedInfos = activeFilesPresenter.GetSelectedFileSystemInfos();
			var files = new List<FileInfo>();

			foreach (var info in selectedInfos)
			{
				if (info.GetType() == typeof(FileInfo))
					files.Add((FileInfo)info);
				else
				{
					ErrorFormFactory.CreateFromException(new TransferDirectoryIntoFileException(cmd, info.FullName, cmd.To)).ShowAsDialog();
					return;
				}
			}

			var transferSettings = cmd.GetType() == typeof(CopyCommand) ? TransferSettings.None : TransferSettings.DeleteOriginal;

			var dest = new FileInfo(cmd.To);
			if (files.Count == 1)
				Operations.TransferFiles((files[0], dest).AsSingleEnumerable(), transferSettings);
			else
				Operations.TransferFiles(files, dest.Directory, dest.Name, transferSettings);

			ResetCommandPromptIfNecessary();
		}
		void HandleDeleteCommand(DeleteCommand cmd)
		{
			IEnumerable<FileSystemInfo> targets = null;
			if (cmd.TargetPath == null)
			{
				FilesPanePresenter activeFilesPresenter;
				if (!FilesPaneActiveCheck(cmd, out activeFilesPresenter)) return;

				targets = activeFilesPresenter.GetSelectedFileSystemInfos();
			}else
			{
				if (cmd.TargetPath.IsDirectoryPath())
					targets = new DirectoryInfo(cmd.TargetPath).AsSingleEnumerable();
				else
					targets = new FileInfo(cmd.TargetPath).AsSingleEnumerable();
			}

			Operations.DeleteFileSystemNodes(targets);

			ResetCommandPromptIfNecessary();
		}
		void HandleSearchCommand(SearchCommand cmd)
		{
			string currentPath;
			var activeFilesPane = PanePresenterInFocus as FilesPanePresenter;
			if (activeFilesPane == null)
				currentPath = @"C:\";
			else
				currentPath = activeFilesPane.CurrentDir.FullName;

			DirectoryInfo searchedDir = Path.IsPathRooted(cmd.SearchedDirectory) ?
				new DirectoryInfo(cmd.SearchedDirectory) :
				new DirectoryInfo(Path.Combine(currentPath, cmd.SearchedDirectory));

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
		void HandleChangeDirectoryCommand(ChangeDirectoryCommand cmd)
		{
			FilesPanePresenter activePresenter = null;
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
				activePresenter = (FilesPanePresenter)PanePresenterInFocus;

			DirectoryInfo dirInfo;

			if (!Path.IsPathRooted(cmd.TargetPath))
				dirInfo = new DirectoryInfo(Path.Combine(activePresenter.CurrentDir.FullName, cmd.TargetPath));
			else
				dirInfo = new DirectoryInfo(cmd.TargetPath);

			if (!dirInfo.Exists)
			{
				ErrorFormFactory.CreateFromException(new FileOrDirectoryNotFound(dirInfo)).ShowAsDialog();
				return;
			}

			activePresenter.ChangeDirectory(dirInfo);
			ResetCommandPromptIfNecessary();
		}
		void HandleSortCommand(SortCommand cmd)
		{
			FilesPanePresenter activeFilesPresenter;
			if (!FilesPaneActiveCheck(cmd, out activeFilesPresenter)) return;

			var sortCmd = (SortCommand)cmd;

			activeFilesPresenter.SetEntrySortOrder(sortCmd.Comparer);
			ResetCommandPromptIfNecessary();
		}
		void HandleLeftCommand(LeftCommand cmd)
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
		void HandleRightCommand(RightCommand cmd)
		{
			RightCommand rightCommand = (RightCommand)cmd;

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
  
		void ResetCommandPromptIfNecessary()
		{
			if (!CommandPromptInFocus) return;

			CommandPromptPresenter.ResetCommandPrompt();
			CommandPromptInFocus = false;
		}
		bool FilesPaneActiveCheck(ICommand cmd, out FilesPanePresenter activeFilesPresenter)
		{
			activeFilesPresenter = PanePresenterInFocus as FilesPanePresenter;
			if (activeFilesPresenter == null)
			{
				ErrorFormFactory.CreateFromException(new FilesPaneWasNotActiveException(cmd)).ShowAsDialog();
				return false;
			}

			return true;
		}
		bool BothPanesAreFilePanesCheck(ICommand cmd, out FilesPanePresenter activeFilesPresenter, out FilesPanePresenter inactiveFilesPresenter)
		{
			activeFilesPresenter = PanePresenterInFocus as FilesPanePresenter;
			inactiveFilesPresenter = OtherPresenter as FilesPanePresenter;
			if (activeFilesPresenter == null || inactiveFilesPresenter == null)
			{
				ErrorFormFactory.CreateFromException(new BothPanesNeedToBeFilesPaneException(cmd)).ShowAsDialog();
				return false;
			}

			return true;
		}
		void SeparateFileSystemInfos(IEnumerable<FileSystemInfo> all, out List<FileInfo> files, out List<DirectoryInfo> dirs)
		{
			files = new List<FileInfo>();
			dirs = new List<DirectoryInfo>();

			foreach (var info in all)
			{
				if (info.GetType() == typeof(DirectoryInfo))
					dirs.Add((DirectoryInfo)info);
				else
					files.Add((FileInfo)info);
			}
		}
		#endregion
	}

}
