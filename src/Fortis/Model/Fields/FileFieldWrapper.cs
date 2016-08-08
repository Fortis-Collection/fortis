using Fortis.Providers;
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

		public FileFieldWrapper(Field field, ISpawnProvider spawnProvider)
			: base(field, spawnProvider) { }

		public FileFieldWrapper(string key, ref ItemWrapper item, ISpawnProvider spawnProvider, string value = null)
			: base(key, ref item, value, spawnProvider) { }

		public override IHtmlString Render(string parameters = null, bool editing = false)
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
