using System;

namespace FileManager
{
	internal class SortCommandFactory : ICommandFactory
	{
		private readonly string[] names;
		public SortCommandFactory()
		{
			names = new string[] { "sort", "s" };
		}
		public SortCommandFactory(string[] cmdNames)
		{
			names = cmdNames;
		}

		public bool Parse(string stringInput, out ICommand parsedCmd)
		{
			parsedCmd = null;


			if (!CommandParser.ParseWithStrArgs(stringInput, names, out string[] cmd) || cmd.Length <= 1 || cmd.Length > 3)
			{
				return false;
			}

			if (!Enum.TryParse(cmd[1], true, out FilesViewColumns sortBy))
			{
				return false;
			}

			if (cmd.Length == 2 || cmd[2] == "Asc" || cmd[2] == "asc")
			{
				parsedCmd = new SortCommand(CreateComparisonAscending(sortBy));
			}
			else if (cmd[2] == "Desc" || cmd[2] == "desc")
			{
				parsedCmd = new SortCommand(CreateComparisonDescending(sortBy));
			}
			else
			{
				return false;
			}

			return true;

		}

		private Comparison<FileSystemNodeEntry> CreateComparisonAscending(FilesViewColumns column)
		{
			switch (column)
			{
				case FilesViewColumns.Name:
					return (x, y) => String.Compare(x.EntryName, y.EntryName, StringComparison.OrdinalIgnoreCase);

				case FilesViewColumns.Extension:
					return (x, y) => String.Compare(x.EntryExt, y.EntryExt, StringComparison.OrdinalIgnoreCase);

				case FilesViewColumns.Size:
					return (x, y) => x.EntrySize.CompareTo(y.EntrySize);

				case FilesViewColumns.Created:
					return (x, y) => DateTime.Compare(x.EntryCreated, y.EntryCreated);

				case FilesViewColumns.Modified:
					return (x, y) => DateTime.Compare(x.EntryModified, y.EntryModified);

				default:
					throw new ArgumentOutOfRangeException("New FilesViewColumn?");
			}
		}

		private Comparison<FileSystemNodeEntry> CreateComparisonDescending(FilesViewColumns column)
		{
			return (x, y) => (-1) * CreateComparisonAscending(column)(x, y);
		}
	}
}
