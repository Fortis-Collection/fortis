using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Resources.Media;
using System.Web;

namespace Fortis.Model.Fields
{
	public class FileFieldWrapper : FieldWrapper, IFileFieldWrapper
	{
		protected Item MediaItem
		{
			get { return ((FileField)Field).MediaItem; }
		}

		public FileFieldWrapper(Field field)
			: base(field)
		{
		}

		public override IHtmlString Render()
		{
			var returnValue = string.Empty;

			if (MediaItem != null)
			{
				returnValue = "/" + MediaManager.GetMediaUrl(MediaItem);
			}

			return new HtmlString(returnValue);
		}

		public string Value
		{
			get { return Render().ToHtmlString(); }
		}
	}
}
