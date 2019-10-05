using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace FileManager
{
	public struct InputKey : IEquatable<char>, IEquatable<Keys>
	{
		public readonly char _char;
		public readonly Keys _key;
		readonly bool isChar;

		public InputKey(char inputChar)
		{
			isChar = true;
			_char = inputChar;
			_key = Keys.None;
		}

		public InputKey(Keys inputKey)
		{
			isChar = false;
			_char = (char)0;
			_key = inputKey;
		}

		public bool IsChar() => isChar;
		public bool IsCommandKey() => !isChar;

		public static explicit operator char(InputKey key) 
		{
			if (key.IsChar()) return key._char;
			else return (char)key._key;
		}
		public static explicit operator string(InputKey key)
		{
			if (key.IsChar()) return key._char.ToString();
			else return ((char)key._key).ToString();
		}

		public static bool operator==(InputKey key, char other)
		{
			return key.Equals(other);
		}
		public static bool operator!=(InputKey key, char other)
		{
			return !key.Equals(other);
		}

		public static bool operator==(InputKey key, Keys other)
		{
			return key.Equals(other);
		}
		public static bool operator!=(InputKey key, Keys other)
		{
			return !key.Equals(other);
		}

		public bool Equals<T1, T2>(T1 other1, T2 other2)
		{
			return Equals(other1) || Equals(other2);
		}
		public bool Equals<T1, T2, T3>(T1 other1, T2 other2, T3 other3)
		{
			return Equals(other1) || Equals(other2) || Equals(other3);
		}
		public bool Equals(char other)
		{
			return isChar && other == _char;
		}
		public bool Equals(Keys other)
		{
			return !isChar && other == _key;
		}
		
		//Overriding the ValueType.Equals method. When comparing to other than the types specified, InputKey.Equals return false
		override public bool Equals(object other)
		{
			return false;
		}
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override string ToString()
		{
			if (isChar) return _char.ToString();
			else return ((char)_key).ToString();
		}

	}
}
