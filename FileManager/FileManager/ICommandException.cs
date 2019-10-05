using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
	interface ICommandException
	{
		string Message { get; }
		string Type { get; }
	}
}
