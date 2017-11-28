using Sitecore.Links;

namespace Fortis.Items
{
	public interface IUrlItem
	{
		string GenerateUrl();
		string GenerateUrl(UrlOptions options);
	}
}
