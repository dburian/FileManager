using System.Collections.Generic;

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
