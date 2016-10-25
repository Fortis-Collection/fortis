using Sitecore.Data.Fields;

namespace Fortis.Fields.IntegerField
{
	public class IntegerFieldFactory : TypedFieldFactory<IntegerField, IIntegerField, Field>, IIntegerFieldFactory
	{
		private const string name = nameof(IntegerFieldFactory);

		public IntegerFieldFactory(
			ITypedFieldMappingValidator mappingValidator)
			: base(mappingValidator)
		{
		}

		public override string Name => name;
	}
}
