using System;

namespace Fortis.Model.Fields
{
	public interface IImageFieldWrapper : IFieldWrapper<string>
	{
		string GetSourceUri();

		string GetSourceUri(bool absolute);

		string Render(ImageFieldWrapperOptions options);
	}
}
