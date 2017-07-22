using Sitecore.Data.Items;
using Sitecore.Links;

namespace Fortis.Items
{
	public interface ISitecoreItemUrlGenerator
	{
		string Generate(Item item);
		string Generate(Item item, UrlOptions options);
	}
}