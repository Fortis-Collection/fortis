using Sitecore.Data;
using Sitecore.Data.Items;
using System;

namespace Fortis.Items
{
	public interface ISitecoreItemGetter
	{
		Item GetItem(Guid id, Database database);
		Item GetItem(Guid id, string database);
		Item GetItem(Guid id, string language, Database database);
		Item GetItem(Guid id, string language, string database);
		Item GetItem(Guid id, string language, int version, Database database);
		Item GetItem(Guid id, string language, int version, string database);
		Item GetItem(string path, Database database);
		Item GetItem(string path, string database);
		Item GetItem(string path, string language, Database database);
		Item GetItem(string path, string language, string database);
		Item GetItem(string path, string language, int version, Database database);
		Item GetItem(string path, string language, int version, string database);
	}
}
