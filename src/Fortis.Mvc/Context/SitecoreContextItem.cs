using Fortis.Context;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;

namespace Fortis.Mvc.Context
{
	public class SitecoreContextItem : ISitecoreContextItem
	{
		public Item Item => PageContext.Current.Item;
	}
}
