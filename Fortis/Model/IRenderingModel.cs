namespace Fortis.Model
{
	using System;

	public interface IRenderingModel<TPageItem, TRenderingItem>
		where TPageItem : IItemWrapper
		where TRenderingItem : IItemWrapper
	{
		TPageItem PageItem { get; }
		TRenderingItem RenderingItem { get; }
	}
}
