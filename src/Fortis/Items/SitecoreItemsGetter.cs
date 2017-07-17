using Fortis.Databases;
using Sitecore.Data;
using Sitecore.Data.Items;
using System.Collections.Generic;

namespace Fortis.Items
{
	public class SitecoreItemsGetter : ISitecoreItemsGetter
	{
		protected readonly ISitecoreDatabaseFactory DatabaseFactory;

		public SitecoreItemsGetter(
			ISitecoreDatabaseFactory databaseFactory)
		{
			DatabaseFactory = databaseFactory;
		}

		public IEnumerable<Item> GetItems(string query, Database database)
		{
			return database.SelectItems(query);
		}
		public IEnumerable<Item> GetItems(string query, string database)
		{
			return GetItems(query, DatabaseFactory.Create(database));
		}
	}
}
