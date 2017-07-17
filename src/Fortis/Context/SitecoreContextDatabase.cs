using Sitecore.Data;

namespace Fortis.Context
{
	public class SitecoreContextDatabase : ISitecoreContextDatabase
	{
		public Database Database => Sitecore.Context.Database;
	}
}
