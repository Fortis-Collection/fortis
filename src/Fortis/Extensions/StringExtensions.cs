using Sitecore.Data;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Fortis.Extensions
{
	public static class StringExtensions
	{
		public static ID SafeParseId(this string source)
		{
			if (string.IsNullOrWhiteSpace(source))
			{
				return new ID(Guid.Empty);
			}
			else if(ShortID.IsShortID(source))
			{
				return ShortID.Parse(source).ToID();
			}
			else if(ID.IsID(source))
			{
				return ID.Parse(source);
			}

			return new ID(Guid.Empty);
		}

		public static Guid SafeParseIdGuid(this string source)
		{
			return source.SafeParseId().Guid;
		}

		public static string SeparatePascal(this string source, string separator)
		{
			var regexPattern = new Regex(@"(?<=[A-Z])(?=[A-Z][a-z])|(?<=[^A-Z])(?=[A-Z])|(?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.Compiled);

			return regexPattern.Replace(source, separator);
		}

		public static string SpaceSeparatePascal(this string source)
		{
			return source.SeparatePascal(" ");
		}
	}
}
