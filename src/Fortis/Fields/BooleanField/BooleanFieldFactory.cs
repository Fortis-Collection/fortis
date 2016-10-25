using Sitecore.Data.Fields;

namespace Fortis.Fields.BooleanField
{
	public class BooleanFieldFactory : TypedFieldFactory<BooleanField, IBooleanField, CheckboxField>, IBooleanFieldFactory
	{
		private const string name = nameof(BooleanFieldFactory);

		public BooleanFieldFactory(
			ITypedFieldMappingValidator mappingValidator)
			: base(mappingValidator)
		{
		}

		public override string Name => name;
	}
}
