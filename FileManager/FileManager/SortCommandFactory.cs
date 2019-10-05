using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FileManager
{
	class SortCommandFactory : ICommandFactory
	{
		readonly string[] names;
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

			string[] cmd;

			if (!CommandParser.ParseWithStrArgs(stringInput, names, out cmd) || cmd.Length <= 1 || cmd.Length > 3)
				return false;
			
			FilesViewColumns sortBy;
			if (!Enum.TryParse(cmd[1], true, out sortBy)) return false;

			if (cmd.Length == 2 || cmd[2] == "Asc" || cmd[2] == "asc") parsedCmd = new SortCommand(CreateComparisonAscending(sortBy));
			else if (cmd[2] == "Desc" || cmd[2] == "desc") parsedCmd = new SortCommand(CreateComparisonDescending(sortBy));
			else return false;

			return true;

		}

		Comparison<FileSystemNodeEntry> CreateComparisonAscending(FilesViewColumns column)
		{
			switch (column)
			{
				case FilesViewColumns.Name:
					return (x, y) => String.Compare(x.EntryName, y.EntryName);

				case FilesViewColumns.Extension:
					return (x, y) => String.Compare(x.EntryExt, y.EntryExt);

				case FilesViewColumns.Size:
					return (x, y) => x.EntrySize.CompareTo(y.EntrySize);

				case FilesViewColumns.Created:
					return (x, y) => DateTime.Compare(x.EntryCreated, y.EntryCreated);

				case FilesViewColumns.Modified:
					return (x, y) => DateTime.Compare(x.EntryModified, y.EntryModified);

				default:
					throw new ArgumentOutOfRangeException();
			}
		}
		Comparison<FileSystemNodeEntry> CreateComparisonDescending(FilesViewColumns column)
		{
			return (x, y) => (-1) * CreateComparisonAscending(column)(x, y);
		}
	}
}
