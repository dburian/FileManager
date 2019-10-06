﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
	struct MoveCommand : ITransferCommand
	{
		public MoveCommand(string to)
		{
			To = to;
		}

		public string To { get; }
	}
}
