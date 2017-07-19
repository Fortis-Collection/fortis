using Sitecore.Data;
using System;

namespace Fortis.Extensions
{
	public static class GuidExtensions
	{
		public static string ToSitecoreIdString(this Guid source)
		{
			return source.ToSitecoreId().ToString();
		}

		public static ID ToSitecoreId(this Guid source)
		{
			return new ID(source);
		}
	}
}
