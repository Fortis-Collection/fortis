using System.Collections.Generic;
using System.Linq;

namespace Fortis.Items
{
	public class ItemsGetter : IItemsGetter
	{
		protected readonly ISitecoreItemsGetter SitecoreItemsGetter;
		protected readonly IItemsFactory ItemsFactory;

		public ItemsGetter(
			ISitecoreItemsGetter sitecoreItemsGetter,
			IItemsFactory itemsFactory)
		{
			SitecoreItemsGetter = sitecoreItemsGetter;
			ItemsFactory = itemsFactory;
		}

		public IEnumerable<T> GetItems<T>(string query, string database)
		{
			var items = SitecoreItemsGetter.GetItems(query, database).ToList();

			return ItemsFactory.Create<T>(items);
		}
	}
}
