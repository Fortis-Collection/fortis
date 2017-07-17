using Sitecore.Data;
using Sitecore.Configuration;

namespace Fortis.Databases
{
	public class SitecoreDatabaseFactory : ISitecoreDatabaseFactory
	{
		public Database Create(string database)
		{
			return Factory.GetDatabase(database);
		}
	}
}
