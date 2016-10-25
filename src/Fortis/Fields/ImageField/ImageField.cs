using Sitecore.Resources.Media;
using System;

namespace Fortis.Fields.ImageField
{
	public class ImageField : BaseField, IImageField
	{
		public new Sitecore.Data.Fields.ImageField Field => base.Field;

		public string AltText => Field.Alt;

		public Guid MediaId => Field.MediaID.Guid;

		public string Url
		{
			get
			{
				if (string.IsNullOrWhiteSpace(Field.Value))
				{
					return string.Empty;
				}

				var mediaItem = Field.MediaItem;

				if (mediaItem == null)
				{
					return string.Empty;
				}

				return MediaManager.GetMediaUrl(mediaItem); // TODO: new MediaUrlOptions() { AbsolutePath = absolute } - Attribute
			}
		}
	}
}
