using Sitecore.Data.Items;
using System.Collections.Generic;
using System.Linq;

namespace Fortis.Items
{
	public class ItemsFactory : IItemsFactory
	{
		protected readonly IItemFactory ItemFactory;

		public ItemsFactory(
			IItemFactory itemFactory)
		{
			ItemFactory = itemFactory;
		}

		public IEnumerable<T> Create<T>(IEnumerable<Item> items)
		{
			return items.Select(i => ItemFactory.Create<T>(i))
						.Where(i => i != null).ToList();
		}
	}
}
