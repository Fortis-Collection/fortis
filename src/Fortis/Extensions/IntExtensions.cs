namespace Fortis.Extensions
{
	public static class IntExtensions
	{
		public static Sitecore.Data.Version ToVersion(this int source)
		{
			return Sitecore.Data.Version.Parse(source);
		}
	}
}
