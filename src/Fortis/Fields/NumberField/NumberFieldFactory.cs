using Sitecore.Data.Fields;

namespace Fortis.Fields.NumberField
{
	public class NumberFieldFactory : TypedFieldFactory<NumberField, INumberField, Field>, INumberFieldFactory
	{
		private const string name = nameof(NumberFieldFactory);

		public NumberFieldFactory(
			ITypedFieldMappingValidator mappingValidator)
			: base(mappingValidator)
		{
		}

		public override string Name => name;
	}
}
