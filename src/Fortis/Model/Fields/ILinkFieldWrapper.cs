using System;
using System.Web;

namespace Fortis.Model.Fields
{
	public interface ILinkFieldWrapper : ILinkFieldWrapper<Guid>
	{
		[Obsolete("Use the Value property")]
		Guid ItemId { get; }
	}

	public interface ILinkFieldWrapper<T> : IFieldWrapper<T>
	{
		string Url { get; }
		[Obsolete("Use the Render methods with the parameters anonymous object")]
		IHtmlString Render(LinkFieldWrapperOptions options);
		TWrapper GetTarget<TWrapper>() where TWrapper : IItemWrapper;
	}
}
