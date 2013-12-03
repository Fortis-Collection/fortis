using Fortis.Model.Fields;
using Sitecore.Data.Fields;

namespace Fortis.Model.RenderingParameters.Fields
{
	public class TextFieldWrapper : FieldWrapper, ITextFieldWrapper
	{
		public TextFieldWrapper(string value)
			: base(value)
		{

		}

		public string Value
		{
			get { return ToString(); }
		}
	}
}
