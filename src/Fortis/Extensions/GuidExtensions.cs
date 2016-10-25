using Sitecore.Data;
using System;

namespace Fortis.Extensions
{
	public static class GuidExtensions
	{
		public static string ToIdString(this Guid source)
		{
			return new ID(source).ToString();
		}
	}
}
