using Sitecore.Abstractions;
using Sitecore.Data.Items;
using Sitecore.Resources.Media;

namespace Fortis.Items
{
	public class SitecoreMediaItemUrlGenerator : ISitecoreMediaItemUrlGenerator
	{
		protected readonly BaseMediaManager MediaManager;

		public SitecoreMediaItemUrlGenerator(
			BaseMediaManager mediaManager)
		{
			MediaManager = mediaManager;
		}

		public string Generate(MediaItem item)
		{
			return MediaManager.GetMediaUrl(item);
		}

		public string Generate(MediaItem item, MediaUrlOptions options)
		{
			return MediaManager.GetMediaUrl(item, options);
		}
	}
}
