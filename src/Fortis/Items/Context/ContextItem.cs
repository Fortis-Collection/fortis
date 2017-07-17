using Fortis.Context;

namespace Fortis.Items.Context
{
	public class ContextItem : IContextItem
	{
		protected readonly ISitecoreContextItem SitecoreItemContext;
		protected readonly IItemFactory ItemFactory;

		public ContextItem(
			ISitecoreContextItem sitecoreItemContext,
			IItemFactory itemFactory)
		{
			SitecoreItemContext = sitecoreItemContext;
			ItemFactory = itemFactory;
		}

		public T GetItem<T>()
		{
			return ItemFactory.Create<T>(SitecoreItemContext.Item);
		}
	}
}
