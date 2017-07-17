using Fortis.Context;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;

namespace Fortis.Mvc.Context
{
	public class SitecoreItemContext : ISitecoreContextItem
	{
		public Item Item => PageContext.Current.Item;
	}
}
