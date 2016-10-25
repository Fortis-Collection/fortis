namespace Fortis.Fields.LinkField
{
	public class LinkFieldFactory : TypedFieldFactory<LinkField, ILinkField, Sitecore.Data.Fields.LinkField>, ILinkFieldFactory
	{
		private const string name = nameof(LinkFieldFactory);

		public LinkFieldFactory(
			ITypedFieldMappingValidator mappingValidator)
			: base(mappingValidator)
		{
		}

		public override string Name => name;
	}
}
