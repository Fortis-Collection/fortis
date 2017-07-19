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
			return GetItemById(id, null, null, database);
		}

		public Item GetItem(Guid id, string database)
		{
			return GetItemById(id, null, null, database);
		}

		public Item GetItem(Guid id, string language, Database database)
		{
			return GetItemById(id, language, null, database);
		}

		public Item GetItem(Guid id, string language, string database)
		{
			return GetItemById(id, language, null, database);
		}

		public Item GetItem(Guid id, string language, int version, Database database)
		{
			return GetItemById(id, language, version, database);
		}

		public Item GetItemById(Guid id, string language, int? version, string database)
		{
			return GetItemById(id, language, version, DatabaseFactory.Create(database));
		}

		public Item GetItemById(Guid id, string language, int? version, Database database)
		{
			var sitecoreId = id.ToSitecoreId();
			var parsedLanguage = language.SafeParseSitecoreLanguage();

			if (parsedLanguage != null && version != null)
			{
				var parsedVersion = version.Value.SafeParseSitecoreVersion();

				return database.GetItem(sitecoreId, parsedLanguage, parsedVersion);
			}
			else if (parsedLanguage != null)
			{
				return database.GetItem(sitecoreId, parsedLanguage);
			}

			return database.GetItem(sitecoreId);
		}

		public Item GetItem(Guid id, string language, int version, string database)
		{
			return GetItem(id, language, version, DatabaseFactory.Create(database));
		}

		public Item GetItem(string path, Database database)
		{
			return GetItemByPath(path, null, null, database);
		}

		public Item GetItem(string path, string database)
		{
			return GetItemByPath(path, null, null, database);
		}

		public Item GetItem(string path, string language, Database database)
		{
			return GetItemByPath(path, language, null, database);
		}

		public Item GetItem(string path, string language, string database)
		{
			return GetItemByPath(path, language, null, database);
		}

		public Item GetItem(string path, string language, int version, Database database)
		{
			return GetItemByPath(path, language, version, database);
		}

		public Item GetItem(string path, string language, int version, string database)
		{
			return GetItemByPath(path, language, version, database);
		}

		public Item GetItemByPath(string path, string language, int? version, string database)
		{
			return GetItemByPath(path, language, version, DatabaseFactory.Create(database));
		}

		public Item GetItemByPath(string path, string language, int? version, Database database)
		{
			var parsedLanguage = language.SafeParseSitecoreLanguage();

			if (parsedLanguage != null && version != null)
			{
				var parsedVersion = version.Value.SafeParseSitecoreVersion();

				return database.GetItem(path, parsedLanguage, parsedVersion);
			}
			else if (parsedLanguage != null)
			{
				return database.GetItem(path, parsedLanguage);
			}

			return database.GetItem(path);
		}
	}
}
