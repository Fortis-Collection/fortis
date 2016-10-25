namespace Fortis.Fields.ImageField
{
	public class ImageFieldFactory : TypedFieldFactory<ImageField, IImageField, Sitecore.Data.Fields.ImageField>, IImageFieldFactory
	{
		private const string name = nameof(ImageFieldFactory);

		public ImageFieldFactory(
			ITypedFieldMappingValidator mappingValidator)
			: base(mappingValidator)
		{
		}

		public override string Name => name;
	}
}
