using System;

namespace Fortis.Model.Fields
{
	public interface ILinkFieldWrapper : IFieldWrapper<string>
	{
		Guid ItemId { get; }
		string Url { get; }
		string Render(LinkFieldWrapperOptions options);
		T GetTarget<T>() where T : IItemWrapper;
	}
}
