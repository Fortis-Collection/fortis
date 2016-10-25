namespace Fortis.Fields.NameValueListField
{
	public class NameValueListFieldFactory : TypedFieldFactory<NameValueListField, INameValueListField, Sitecore.Data.Fields.NameValueListField>, INameValueListFieldFactory
	{
		private const string name = nameof(NameValueListFieldFactory);

		public NameValueListFieldFactory(
			ITypedFieldMappingValidator mappingValidator)
			: base(mappingValidator)
		{
		}

		public override string Name => name;
	}
}
