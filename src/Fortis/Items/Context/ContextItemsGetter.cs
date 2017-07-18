using Fortis.Context;
using System.Collections.Generic;
using System.Linq;

namespace Fortis.Items.Context
{
	public class ContextItemsGetter : IContextItemsGetter
	{
		protected readonly ISitecoreContextDatabase Context;
		protected readonly ISitecoreItemsGetter SitecoreItemsGetter;
		protected readonly IItemsFactory ItemsFactory;

		public ContextItemsGetter(
			ISitecoreContextDatabase context,
			ISitecoreItemsGetter sitecoreItemsGetter,
			IItemsFactory itemsFactory)
		{
			Context = context;
			SitecoreItemsGetter = sitecoreItemsGetter;
			ItemsFactory = itemsFactory;
		}

		public IEnumerable<T> GetItems<T>(string query)
		{
			var items = SitecoreItemsGetter.GetItems(query, Context.Database).ToList();

			return ItemsFactory.Create<T>(items);
		}
	}
}
