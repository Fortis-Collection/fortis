using System.Collections.Generic;
using System.Linq;

namespace Fortis.Items
{
	public class ItemsGetter : IItemsGetter
	{
		protected readonly ISitecoreItemsGetter SitecoreItemsGetter;
		protected readonly IItemFactory ItemFactory;

		public ItemsGetter(
			ISitecoreItemsGetter sitecoreItemsGetter,
			IItemFactory itemFactory)
		{
			SitecoreItemsGetter = sitecoreItemsGetter;
			ItemFactory = itemFactory;
		}

		public IEnumerable<T> GetItems<T>(string query, string database)
		{
			var items = SitecoreItemsGetter.GetItems(query, database).ToList();

			return ItemFactory.Create<T>(items);
		}
	}
}
