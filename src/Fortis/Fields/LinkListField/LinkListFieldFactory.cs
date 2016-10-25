using Sitecore.Data.Fields;

namespace Fortis.Fields.LinkListField
{
	public class LinkListFieldFactory : TypedFieldFactory<LinkListField, ILinkListField, MultilistField>, ILinkListFieldFactory
	{
		private const string name = nameof(LinkListFieldFactory);

		public LinkListFieldFactory(
			ITypedFieldMappingValidator mappingValidator)
			: base(mappingValidator)
		{
		}

		public override string Name => name;
	}
}
