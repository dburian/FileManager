using System;
using System.Windows.Forms;

namespace FileManager
{


	/// <summary>
	/// Represents key pressed.
	/// </summary>
	public struct InputKey : IEquatable<char>, IEquatable<Keys>, IEquatable<InputKey>
	{
		public InputKey(char inputChar)
		{
			IsChar = true;
			Character = inputChar;
			Key = Keys.None;
		}

		public InputKey(Keys inputKey)
		{
			IsChar = false;
			Character = (char)0;
			Key = inputKey;
		}

		public bool IsChar { get; }
		public bool IsCommandKey => !IsChar;

		public char Character { get; }

		public Keys Key { get; }

		public static explicit operator char(InputKey key)
		{
			if (key.IsChar)
			{
				return key.Character;
			}
			else
			{
				return (char)key.Key;
			}
		}
		public static explicit operator string(InputKey key)
		{
			if (key.IsChar)
			{
				return key.Character.ToString();
			}
			else
			{
				return ((char)key.Key).ToString();
			}
		}

		public static bool operator ==(InputKey key, char other)
		{
			return key.Equals(other);
		}
		public static bool operator !=(InputKey key, char other)
		{
			return !key.Equals(other);
		}

		public static bool operator ==(InputKey key, Keys other)
		{
			return key.Equals(other);
		}
		public static bool operator !=(InputKey key, Keys other)
		{
			return !key.Equals(other);
		}

		public bool EqualsAny<T1, T2>(T1 other1, T2 other2)
		{
			return Equals(other1) || Equals(other2);
		}
		public bool EqualsAny<T1, T2, T3>(T1 other1, T2 other2, T3 other3)
		{
			return Equals(other1) || Equals(other2) || Equals(other3);
		}
		public bool Equals(char other)
		{
			return IsChar && other == Character;
		}
		public bool Equals(Keys other)
		{
			return !IsChar && other == Key;
		}

		//Overriding the ValueType.Equals method. When comparing to other than the types specified, InputKey.Equals return false
		public override bool Equals(object other)
		{
			return false;
		}
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override string ToString()
		{
			if (IsChar)
			{
				return Character.ToString();
			}
			else
			{
				return ((char)Key).ToString();
			}
		}

		public bool Equals(InputKey other)
		{
			if (other.IsChar && IsChar)
			{
				return other.Character == Character;
			}
			else if (other.IsCommandKey && IsCommandKey)
			{
				return other.Key == Key;
			}

			return false;
		}
	}
}
