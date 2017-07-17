using Sitecore.Data.Items;

namespace Fortis.Context
{
	public class SitecoreContextItem : ISitecoreContextItem
	{
		public Item Item => Sitecore.Context.Item;
	}
}
