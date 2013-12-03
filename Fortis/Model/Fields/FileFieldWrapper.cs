using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Resources.Media;
using System.Web;

namespace Fortis.Model.Fields
{
	public class FileFieldWrapper : FieldWrapper
	{
		protected Item MediaItem
		{
			get { return ((FileField)Field).MediaItem; }
		}

		public FileFieldWrapper(Field field)
			: base(field) { }

		public FileFieldWrapper(string key, ref ItemWrapper item, string value = null)
			: base(key, ref item, value) { }

		public override IHtmlString Render(string parameters = null, bool editing = false)
		{
			var returnValue = string.Empty;

			if (MediaItem != null)
			{
				returnValue = "/" + MediaManager.GetMediaUrl(MediaItem);
			}

			return new HtmlString(returnValue);	
		}
	}
}
