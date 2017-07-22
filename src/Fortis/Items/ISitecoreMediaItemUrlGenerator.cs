using Sitecore.Data.Items;
using Sitecore.Resources.Media;

namespace Fortis.Items
{
	public interface ISitecoreMediaItemUrlGenerator
	{
		string Generate(MediaItem item);
		string Generate(MediaItem item, MediaUrlOptions options);
	}
}