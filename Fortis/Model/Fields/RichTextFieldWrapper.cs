using Sitecore.Data.Fields;

namespace Fortis.Model.Fields
{
	public class RichTextFieldWrapper : TextFieldWrapper
	{
		public RichTextFieldWrapper(Field field)
			: base(field) { }

		public RichTextFieldWrapper(string key, ref ItemWrapper item, string value = null)
			: base(key, ref item, value) { }
	}
}
