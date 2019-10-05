using System;
using System.Collections.Generic;
using System.Text;

namespace HelperExtensionLibrary
{
	public static class IEnumerableExtensions
	{
		public static IEnumerable<T> AsSingleEnumerable<T>(this T item)
		{
			yield return item;
		}
	}
}
