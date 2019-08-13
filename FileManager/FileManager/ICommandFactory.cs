using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
	interface ICommandFactory
	{
		ICommand GetCommandInstance();
		bool Parse(string stringInput);
	}
}
