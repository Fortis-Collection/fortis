using System;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Resources.Media;
using Sitecore.Web.UI.WebControls;
using System.Web;
using Fortis.Providers;

namespace Fortis.Model.Fields
{
	public class ImageFieldWrapper : FieldWrapper, IImageFieldWrapper
	{
		protected ImageField ImageField { get { return (ImageField)Field; } }

		public ImageFieldWrapper(Field field, ISpawnProvider spawnProvider)
			: base(field, spawnProvider) { }

		public ImageFieldWrapper(string key, ref ItemWrapper item, ISpawnProvider spawnProvider, string value = null)
			: base(key, ref item, value, spawnProvider) { }

		public string AltText
		{
			get { return ImageField.Alt; }
		}

		public string GetSourceUri()
		{
			return GetSourceUri(false);
		}

		public string GetSourceUri(bool absolute)
		{
			if (string.IsNullOrWhiteSpace(Field.Value))
			{
				return string.Empty;
			}

			var mediaItem = ((ImageField)Field).MediaItem;

			if (mediaItem == null)
			{
				return string.Empty;
			}

			return MediaManager.GetMediaUrl(mediaItem, new MediaUrlOptions() { AbsolutePath = absolute });
		}

		public IHtmlString Render(ImageFieldWrapperOptions options)
		{
			string param = "";

			if (options != null)
			{
				param += (options.Width > 0) ? "&width=" + options.Width : string.Empty;
				param += (options.Height > 0) ? "&height=" + options.Height : string.Empty;
				param += (options.Crop) ? "&crop=1" : string.Empty;

				if (options.CornerRadii.Length > 0)
				{
					if (options.CornerRadii.Length == 4)
					{
						param += "&rc=" + options.CornerRadii[0] + "," + options.CornerRadii[1] + "," + options.CornerRadii[2] + "," + options.CornerRadii[3];
					}
					else if (options.CornerRadii.Length == 1 && options.CornerRadii[0] > 0)
					{
						param += "&rc=" + options.CornerRadii[0];
					}
				}
				if (param.StartsWith("&"))
				{
					param = param.Substring(1);
				}
			}

			var fieldRenderer = new FieldRenderer();
			fieldRenderer.Item = Field.Item;
			fieldRenderer.FieldName = Field.Key;
			fieldRenderer.Parameters = param;

			var result = fieldRenderer.RenderField();

			return new HtmlString(result.FirstPart + result.LastPart);
		}

		public T GetTarget<T>() where T : IItemWrapper
		{
			if (Field == null || Field.Value.Length == 0)
			{
				return default(T);
			}
			var mediaItem = ImageField.MediaItem;
			if (mediaItem != null)
			{
				var target = SpawnProvider.FromItem<T>(new Item(mediaItem.ID, mediaItem.InnerData, mediaItem.Database));
				return (T)((target is T) ? target : null);
			}
			return default(T);
		}

		public static implicit operator string(ImageFieldWrapper field)
		{
			return field.GetSourceUri();
		}

		public string Value
		{
			get { return GetSourceUri(); }
		}
	}
}
