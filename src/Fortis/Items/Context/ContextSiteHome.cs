using Fortis.Context;

namespace Fortis.Items.Context
{
	public class ContextSiteHome : IContextSiteHome
	{
		protected readonly ISitecoreContextDatabase Context;
		protected readonly IItemFactory ItemFactory;

		public ContextSiteHome(
			ISitecoreContextDatabase context,
			IItemFactory itemFactory)
		{
			Context = context;
			ItemFactory = itemFactory;
		}

		public T GetSiteHome<T>()
		{
			var item = Context.Database.GetItem(Sitecore.Context.Site.StartPath);

			return ItemFactory.Create<T>(item);
		}
	}
}
