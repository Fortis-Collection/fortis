using System;
using Sitecore.Data.Fields;
using Sitecore.Resources.Media;
using Sitecore.Web.UI.WebControls;

namespace Fortis.Model.Fields
{
	public class ImageFieldWrapper : FieldWrapper, IImageFieldWrapper
	{
		public ImageFieldWrapper(Field field)
			: base(field)
		{
		}

		public string GetSourceUri()
		{
			return GetSourceUri(false);
		}

		public string GetSourceUri(bool absolute)
		{
			if (Field.Value.Length == 0)
			{
				return string.Empty;
			}
			else
			{
				return MediaManager.GetMediaUrl(((ImageField)Field).MediaItem, new MediaUrlOptions() { AbsolutePath = absolute });
			}
		}

		public string Render(ImageFieldWrapperOptions options)
		{
			string param = "";

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

			var fieldRenderer = new FieldRenderer();
			fieldRenderer.Item = Field.Item;
			fieldRenderer.FieldName = Field.Key;
			fieldRenderer.Parameters = param;

			var result = fieldRenderer.RenderField();

			return result.FirstPart + result.LastPart;
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
