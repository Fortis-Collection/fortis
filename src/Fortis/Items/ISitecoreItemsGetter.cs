using System.Collections.Generic;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace Fortis.Items
{
	public interface ISitecoreItemsGetter
	{
		IEnumerable<Item> GetItems(string query, Database database);
		IEnumerable<Item> GetItems(string query, string database);
	}
}