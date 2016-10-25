namespace Fortis.Fields.GeneralLinkField
{
	public class GeneralLinkFieldFactory : TypedFieldFactory<GeneralLinkField, IGeneralLinkField, Sitecore.Data.Fields.LinkField>, IGeneralLinkFieldFactory
	{
		private const string name = nameof(GeneralLinkFieldFactory);

		public GeneralLinkFieldFactory(
			ITypedFieldMappingValidator mappingValidator)
			: base(mappingValidator)
		{
		}

		public override string Name => name;
	}
}
