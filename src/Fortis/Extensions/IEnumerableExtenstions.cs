using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortis.Extensions
{
	public static class IEnumerableExtenstions
	{
		public static string ToDelimitedString<T>(this IEnumerable<T> enumerable, string delimiter)
		{
			return enumerable == null ? null : string.Join(delimiter, enumerable);
		}

		public static string ToDelimitedString<T>(this IEnumerable<T> enumerable, char delimiter)
		{
			return enumerable.ToDelimitedString<T>(delimiter.ToString());
		}

		public static string ToDelimitedString(this IEnumerable enumerable, string delimiter)
		{
			return enumerable == null ? null : string.Join(delimiter, enumerable);
		}

		public static string ToDelimitedString(this IEnumerable enumerable, char delimiter)
		{
			return enumerable.ToDelimitedString(delimiter.ToString());
		}
	}
}
