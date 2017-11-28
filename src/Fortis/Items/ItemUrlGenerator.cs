using Sitecore.Links;

namespace Fortis.Items
{
	public class ItemUrlGenerator : IItemUrlGenerator
	{
		protected readonly ISitecoreItemUrlGenerator SitecoreItemUrlGenerator;
		protected readonly IBaseItemGetter ItemGetter;

		public ItemUrlGenerator(
			ISitecoreItemUrlGenerator sitecoreItemUrlGenerator,
			IBaseItemGetter itemGetter)
		{
			SitecoreItemUrlGenerator = sitecoreItemUrlGenerator;
			ItemGetter = itemGetter;
		}

		public string Generate(IItem item)
		{
			var baseItem = ItemGetter.GetItem(item);
			var scItem = baseItem?.Item;

			if (scItem == null)
			{
				return null;
			}

			return SitecoreItemUrlGenerator.Generate(scItem);
		}

		public string Generate(IItem item, UrlOptions options)
		{
			var baseItem = ItemGetter.GetItem(item);
			var scItem = baseItem?.Item;

			if (scItem == null)
			{
				return null;
			}

			return SitecoreItemUrlGenerator.Generate(scItem, options);
		}
	}
}
