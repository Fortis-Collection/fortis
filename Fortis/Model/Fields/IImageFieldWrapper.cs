using System;
using System.Web;

namespace Fortis.Model.Fields
{
	public interface IImageFieldWrapper : IFieldWrapper<string>
	{
		string AltText { get; }

		string GetSourceUri();

		string GetSourceUri(bool absolute);

		IHtmlString Render(ImageFieldWrapperOptions options);

		T GetTarget<T>() where T : IItemWrapper;
	}
}
