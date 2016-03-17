using System.Web;
using Sitecore.Data;

namespace Fortis.Model
{
	public interface IFieldWrapper : IWrapper, IHtmlString
	{
		string RawValue { get; set; }
		bool Modified { get; }
		string ToString();
		bool IsLazy { get; }
		bool HasValue { get; }
		IHtmlString Render(string parameters = null, bool editing = true);
		IHtmlString Render(object parameters, bool editing = true);
		IHtmlString RenderBeginField(object parameters, bool editing = true);
		IHtmlString RenderBeginField(string parameters = null, bool editing = true);
		IHtmlString RenderEndField();
	}

	public interface IFieldWrapper<T> : IFieldWrapper
	{
		T Value { get; }
	}
}
