using Sitecore.Data.Fields;

namespace Fortis.Fields.TextField
{
	public class TextFieldFactory : TypedFieldFactory<TextField, ITextField, Field>, ITextFieldFactory
	{
		private const string name = nameof(TextFieldFactory);

		public TextFieldFactory(
			ITypedFieldMappingValidator mappingValidator)
			: base(mappingValidator)
		{

		}

		public override string Name => name;
	}
}
