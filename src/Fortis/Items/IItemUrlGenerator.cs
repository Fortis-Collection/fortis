using Sitecore.Links;

namespace Fortis.Items
{
	public interface IItemUrlGenerator
	{
		string Generate(IItem item);
		string Generate(IItem item, UrlOptions options);
	}
}