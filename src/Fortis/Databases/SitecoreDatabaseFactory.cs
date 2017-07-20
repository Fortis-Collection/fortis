using Sitecore.Data;
using Sitecore.Abstractions;

namespace Fortis.Databases
{
	public class SitecoreDatabaseFactory : ISitecoreDatabaseFactory
	{
		protected readonly BaseFactory SitecoreFactory;

		public SitecoreDatabaseFactory(
			BaseFactory sitecoreFactory)
		{
			SitecoreFactory = sitecoreFactory;
		}

		public Database Create(string database)
		{
			return SitecoreFactory.GetDatabase(database);
		}
	}
}
