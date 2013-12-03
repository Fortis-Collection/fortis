using Sitecore.Data.Fields;

namespace Fortis.Model.Fields
{
	public class TextFieldWrapper : FieldWrapper, ITextFieldWrapper
	{
		public TextFieldWrapper(Field field)
			: base(field)
		{

		}

		public string Value
		{
			get { return ToHtmlString(); }
		}
	}
}
