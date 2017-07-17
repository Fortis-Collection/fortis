using Fortis.Extensions;
using System;
using Sitecore.Data;
using Sitecore.Data.Items;
using Fortis.Databases;

namespace Fortis.Items
{
	public class SitecoreItemGetter : ISitecoreItemGetter
	{
		protected readonly ISitecoreDatabaseFactory DatabaseFactory;

		public SitecoreItemGetter(
			ISitecoreDatabaseFactory databaseFactory)
		{
			DatabaseFactory = databaseFactory;
		}

		public Item GetItem(Guid id, Database database)
		{
			return database.GetItem(id.ToId());
		}

		public Item GetItem(Guid id, string database)
		{
			return GetItem(id, DatabaseFactory.Create(database));
		}

		public Item GetItem(Guid id, string language, Database database)
		{
			return database.GetItem(id.ToId(), language.SafeParseLanguage());
		}

		public Item GetItem(Guid id, string language, string database)
		{
			return GetItem(id, language, DatabaseFactory.Create(database));
		}

		public Item GetItem(Guid id, string language, int version, Database database)
		{
			return database.GetItem(id.ToId(), language.SafeParseLanguage(), version.ToVersion());
		}

		public Item GetItem(Guid id, string language, int version, string database)
		{
			return GetItem(id, language, version, DatabaseFactory.Create(database));
		}

		public Item GetItem(string path, Database database)
		{
			return database.GetItem(path);
		}

		public Item GetItem(string path, string database)
		{
			return GetItem(path, DatabaseFactory.Create(database));
		}

		public Item GetItem(string path, string language, Database database)
		{
			return database.GetItem(path, language.SafeParseLanguage());
		}

		public Item GetItem(string path, string language, string database)
		{
			return GetItem(path, language, DatabaseFactory.Create(database));
		}

		public Item GetItem(string path, string language, int version, Database database)
		{
			return database.GetItem(path, language.SafeParseLanguage(), version.ToVersion());
		}

		public Item GetItem(string path, string language, int version, string database)
		{
			return GetItem(path, language, version, DatabaseFactory.Create(database));
		}
	}
}
