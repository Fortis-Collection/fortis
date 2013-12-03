using Sitecore.Data.Fields;

namespace Fortis.Model.Fields
{
	public class RichTextFieldWrapper : TextFieldWrapper, IRichTextFieldWrapper
	{
		public RichTextFieldWrapper(Field field)
			: base(field)
		{ }
	}
}
