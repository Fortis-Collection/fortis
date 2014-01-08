using System;
using System.Web;

namespace Fortis.Model.Fields
{
	public interface ILinkFieldWrapper : IFieldWrapper<string>
	{
		Guid ItemId { get; }
		string Url { get; }
		IHtmlString Render(LinkFieldWrapperOptions options);
		T GetTarget<T>() where T : IItemWrapper;
	}
}
