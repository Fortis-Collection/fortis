using Sitecore.Abstractions;
using Sitecore.Data.Items;
using Sitecore.Links;

namespace Fortis.Items
{
	public class SitecoreItemUrlGenerator : ISitecoreItemUrlGenerator
	{
		protected readonly BaseLinkManager LinkManager;

		public SitecoreItemUrlGenerator(
			BaseLinkManager linkManager)
		{
			LinkManager = linkManager;
		}

		public string Generate(Item item)
		{
			return LinkManager.GetItemUrl(item);
		}

		public string Generate(Item item, UrlOptions options)
		{
			return LinkManager.GetItemUrl(item, options);
		}
	}
}
