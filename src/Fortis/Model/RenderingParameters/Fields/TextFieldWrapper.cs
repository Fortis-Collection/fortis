using Fortis.Model.Fields;
using Fortis.Providers;
using Sitecore.Data.Fields;

namespace Fortis.Model.RenderingParameters.Fields
{
	public class TextFieldWrapper : FieldWrapper, ITextFieldWrapper
	{
		public TextFieldWrapper(string value, ISpawnProvider spawnProvider)
			: base(value, spawnProvider)
		{

		}

		public string Value
		{
			get { return ToString(); }
		}
	}
}
