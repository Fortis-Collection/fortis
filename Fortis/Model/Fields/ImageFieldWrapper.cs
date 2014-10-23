using System.Text;
using Sitecore.Data.Fields;
using Sitecore.Resources.Media;
using Sitecore.Web.UI.WebControls;

namespace Fortis.Model.Fields
{
	public class ImageFieldWrapper : FieldWrapper
	{
		public ImageFieldWrapper(Field field)
			: base(field)
		{
		}

		public string GetSourceURI()
		{
			return GetSourceURI(false);
		}

		public string GetSourceURI(bool absolute)
		{
			return Field.Value.Length == 0 
				? string.Empty 
				: MediaManager.GetMediaUrl(((ImageField)Field).MediaItem, new MediaUrlOptions { AbsolutePath = absolute });
		}

		public string Render(ImageFieldWrapperOptions options)
		{
			var param = new StringBuilder();

			param.Append(options.MaxWidth > 0 ? "&mw=" + options.Width : string.Empty);
			param.Append(options.Width > 0 ? "&w=" + options.Width : string.Empty);
			param.Append(options.MaxHeight > 0 ? "&mh=" + options.Height : string.Empty);
			param.Append(options.Height > 0 ? "&h=" + options.Height : string.Empty);
			param.Append(options.Crop ? "&crop=1" : string.Empty);

			if (options.CornerRadii.Length > 0)
			{
				if (options.CornerRadii.Length == 4)
				{
					param.Append("&rc=" + options.CornerRadii[0] + "," + options.CornerRadii[1] + "," + options.CornerRadii[2] + "," + options.CornerRadii[3]);
				}
				else if (options.CornerRadii.Length == 1 && options.CornerRadii[0] > 0)
				{
					param.Append("&rc=" + options.CornerRadii[0]);
				}
			}

			var parameters = param.ToString();

			if (parameters.StartsWith("&"))
			{
				parameters = parameters.Substring(1);
			}

			var fieldRenderer = new FieldRenderer();
			fieldRenderer.Item = Field.Item;
			fieldRenderer.FieldName = Field.Key;
			fieldRenderer.Parameters = parameters;

			var result = fieldRenderer.RenderField();

			return result.FirstPart + result.LastPart;
		}

		public static implicit operator string(ImageFieldWrapper field)
		{
			return field.GetSourceURI();
		}
	}
}
