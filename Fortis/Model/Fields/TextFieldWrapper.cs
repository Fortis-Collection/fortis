using Sitecore.Data.Fields;

namespace Fortis.Model.Fields
{
	public class TextFieldWrapper : FieldWrapper
	{
		public TextFieldWrapper(Field field)
			: base(field) { }

		public TextFieldWrapper(string key, ref ItemWrapper item, string value = null)
			: base(key, ref item, value) { }
	}
}
