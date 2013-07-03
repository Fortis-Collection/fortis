using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Resources.Media;

namespace Fortis.Model.Fields
{
	public class FileFieldWrapper : FieldWrapper
	{
		protected Item MediaItem
		{
			get { return ((FileField)Field).MediaItem; }
		}

		public FileFieldWrapper(Field field)
			: base(field)
		{
		}

		public override string Render()
		{
			if (MediaItem != null)
			{
				return "/" + MediaManager.GetMediaUrl(MediaItem);
			}
			return string.Empty;
		}
	}
}
