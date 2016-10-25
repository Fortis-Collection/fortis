namespace Fortis.Fields.FileField
{
	public class FileFieldFactory : TypedFieldFactory<FileField, IFileField, Sitecore.Data.Fields.FileField>, IFileFieldFactory
	{
		private const string name = nameof(FileFieldFactory);

		public FileFieldFactory(
			ITypedFieldMappingValidator mappingValidator)
			: base(mappingValidator)
		{
		}

		public override string Name => name;
	}
}
