using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelperExtensionLibrary;

namespace FileManager
{
	struct DeleteCommand : ICommand
	{
		public DeleteCommand(string targetPath)
		{
			this.TargetPath = targetPath.GetCamelCasedPath();
		}

		public string TargetPath { get; }
	}
}
