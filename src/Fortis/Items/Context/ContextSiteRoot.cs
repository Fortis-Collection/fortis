using Fortis.Context;

namespace Fortis.Items.Context
{
	public class ContextSiteRoot : IContextSiteRoot
	{
		protected readonly ISitecoreContextDatabase Context;
		protected readonly IItemFactory ItemFactory;

		public ContextSiteRoot(
			ISitecoreContextDatabase context,
			IItemFactory itemFactory)
		{
			Context = context;
			ItemFactory = itemFactory;
		}

		public T GetSiteRoot<T>()
		{
			var item = Context.Database.GetItem(Sitecore.Context.Site.RootPath);

			return ItemFactory.Create<T>(item);
		}
	}
}
