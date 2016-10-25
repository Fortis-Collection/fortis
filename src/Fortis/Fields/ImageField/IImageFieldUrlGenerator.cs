namespace Fortis.Fields.ImageField
{
	public interface IImageFieldUrlGenerator
	{
		string Generate(IImageField field, IImageFieldUrlOptions options);
	}
}
