using Fortis.Providers;
using Sitecore.Data.Fields;

namespace Fortis.Model.Fields
{
	using System.Web;

	public class RichTextFieldWrapper : TextFieldWrapper, IRichTextFieldWrapper
	{
		public RichTextFieldWrapper(Field field, ISpawnProvider spawnProvider)
			: base(field, spawnProvider) { }

		public RichTextFieldWrapper(string key, ref ItemWrapper item, ISpawnProvider spawnProvider, string value = null)
			: base(key, ref item, spawnProvider, value) { }

		public IHtmlString Value
		{
			get { return Render(editing: false); }
		}
	}
}
