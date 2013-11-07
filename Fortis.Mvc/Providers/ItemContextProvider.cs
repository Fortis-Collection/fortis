namespace Fortis.Mvc.Providers
{
	using Fortis.Providers;
	using Sitecore.Data.Items;
	using Sitecore.Mvc.Presentation;

	public class ItemContextProvider : IItemContextProvider
	{
		public Item RenderingItem
		{
			get { return RenderingContext.Current.Rendering.Item; }
		}

		public Item PageItem
		{
			get { return PageContext.Current.Item; }
		}
	}
}
