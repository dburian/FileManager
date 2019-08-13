using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
	class SortCommandFactory : ICommandFactory
	{
		readonly string[] names = new string[] { "sort", "s" };

		bool initialized;
		SortCommand parsedCmd;

		public ICommand GetCommandInstance() => initialized ? parsedCmd : throw new InvalidOperationException();
		public bool Parse(string stringInput)
		{
			string[] cmd;

			if (!CommandParser.ParseWithStrArgs(stringInput, names, out cmd) || cmd.Length <= 1 || cmd.Length > 3)
				return false;
			
			FilesViewColumns sortBy;
			if (!Enum.TryParse(cmd[1], true, out sortBy)) return false;

			if (cmd.Length == 2 || cmd[2] == "Asc" || cmd[2] == "asc") parsedCmd = new SortCommand(CreateComparerAscending(sortBy));
			else if (cmd[2] == "Desc" || cmd[2] == "desc") parsedCmd = new SortCommand(CreateComparerDescending(sortBy));
			else return false;

			initialized = true;
			return true;

		}

		Comparer<FilesViewEntry> CreateComparerAscending(FilesViewColumns column)
		{
			return Comparer<FilesViewEntry>.Create(CreateComparisonAscending(column));
		}
		Comparer<FilesViewEntry> CreateComparerDescending(FilesViewColumns column)
		{
			return Comparer<FilesViewEntry>.Create((x, y) => (-1) * CreateComparisonAscending(column)(x, y));
		}
		Comparison<FilesViewEntry> CreateComparisonAscending(FilesViewColumns column)
		{
			switch (column)
			{
				case FilesViewColumns.Name:
					return (x, y) => String.Compare(x.EntryName, y.Name);

				case FilesViewColumns.Extension:
					return (x, y) => String.Compare(x.EntryExt, y.EntryExt);

				case FilesViewColumns.Size:
					return (x, y) => (int)((y.EntrySize - x.EntrySize) / long.MaxValue);

				case FilesViewColumns.DateTimeCreated:
					return (x, y) => DateTime.Compare(x.EntryCreated, y.EntryCreated);

				case FilesViewColumns.DateTimeModified:
					return (x, y) => DateTime.Compare(x.EntryModified, y.EntryModified);

				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}
