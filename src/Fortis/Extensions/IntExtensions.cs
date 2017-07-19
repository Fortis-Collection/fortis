namespace Fortis.Extensions
{
	public static class IntExtensions
	{
		public static Sitecore.Data.Version SafeParseSitecoreVersion(this int source)
		{
			if (Sitecore.Data.Version.TryParse(source.ToString(), out Sitecore.Data.Version parsedVersion))
			{
				return parsedVersion;
			}

			return null;
		}
	}
}
