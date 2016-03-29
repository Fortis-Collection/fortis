using Fortis.Providers;
using Sitecore.Data.Fields;

namespace Fortis.Model.Fields
{
	public class TextFieldWrapper : FieldWrapper, ITextFieldWrapper
	{
		public TextFieldWrapper(Field field, ISpawnProvider spawnProvider)
			: base(field, spawnProvider) { }

		public TextFieldWrapper(string key, ref ItemWrapper item, ISpawnProvider spawnProvider, string value = null)
			: base(key, ref item, value, spawnProvider) { }

		public string Value
		{
			get { return RawValue; }
		}
	}
}
