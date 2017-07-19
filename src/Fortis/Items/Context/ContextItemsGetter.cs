using Fortis.Context;
using System.Collections.Generic;
using System.Linq;

namespace Fortis.Items.Context
{
	public class ContextItemsGetter : IContextItemsGetter
	{
		protected readonly ISitecoreContextDatabase Context;
		protected readonly ISitecoreItemsGetter SitecoreItemsGetter;
		protected readonly IItemFactory ItemFactory;

		public ContextItemsGetter(
			ISitecoreContextDatabase context,
			ISitecoreItemsGetter sitecoreItemsGetter,
			IItemFactory itemFactory)
		{
			Context = context;
			SitecoreItemsGetter = sitecoreItemsGetter;
			ItemFactory = itemFactory;
		}

		public IEnumerable<T> GetItems<T>(string query)
		{
			var items = SitecoreItemsGetter.GetItems(query, Context.Database).ToList();

			return ItemFactory.Create<T>(items);
		}
	}
}
