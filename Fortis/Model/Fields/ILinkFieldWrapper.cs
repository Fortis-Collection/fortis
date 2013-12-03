using System;

namespace Fortis.Model.Fields
{
	public interface ILinkFieldWrapper : IFieldWrapper<string>
	{
		string Url { get; }

		string Render(LinkFieldWrapperOptions options);

		T GetTarget<T>() where T : IItemWrapper;
	}
}
