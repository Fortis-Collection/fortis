using Fortis.Items;
using Sitecore.Data.Fields;

namespace Fortis.Fields.GeneralLinkField
{
	public class GeneralLinkFieldFactory : TypedFieldFactory<GeneralLinkField, IGeneralLinkField, Sitecore.Data.Fields.LinkField>, IGeneralLinkFieldFactory
	{
		private const string name = nameof(GeneralLinkFieldFactory);

		protected readonly ISitecoreItemUrlGenerator ItemUrlGenerator;
		protected readonly ISitecoreMediaItemUrlGenerator MediaItemUrlGenerator;

		public GeneralLinkFieldFactory(
			ITypedFieldMappingValidator mappingValidator,
			ISitecoreItemUrlGenerator itemUrlGenerator,
			ISitecoreMediaItemUrlGenerator mediaItemUrlGenerator)
			: base(mappingValidator)
		{
			ItemUrlGenerator = itemUrlGenerator;
			MediaItemUrlGenerator = mediaItemUrlGenerator;
		}

		public override GeneralLinkField Create(Field field)
		{
			var generalLinkField = base.Create(field);

			generalLinkField.ItemUrlGenerator = ItemUrlGenerator;
			generalLinkField.MediaItemUrlGenerator = MediaItemUrlGenerator;

			return generalLinkField;
		}

		public override string Name => name;
	}
}
