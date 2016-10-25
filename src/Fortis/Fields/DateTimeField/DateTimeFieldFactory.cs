using Sitecore.Data.Fields;

namespace Fortis.Fields.DateTimeField
{
	public class DateTimeFieldFactory : TypedFieldFactory<DateTimeField, IDateTimeField, DateField>, IDateTimeFieldFactory
	{
		private const string name = nameof(DateTimeFieldFactory);

		public DateTimeFieldFactory(
			ITypedFieldMappingValidator mappingValidator)
			: base(mappingValidator)
		{
		}

		public override string Name => name;
	}
}
