namespace Fortis.Fields.FileField
{
	public class FileField : BaseField, IFileField
	{
		public new Sitecore.Data.Fields.FileField Field => base.Field;

		public string Url => Field.Src;
	}
}
