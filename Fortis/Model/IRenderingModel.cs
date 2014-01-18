namespace Fortis.Model
{
	using System;

	public interface IRenderingModel<TPageItem, TRenderingItem>
		where TPageItem : IItemWrapper
		where TRenderingItem : IItemWrapper
	{
		TPageItem PageItem { get; }
		IRenderingContext PageContext { get; }
		TRenderingItem RenderingItem { get; }
		IRenderingContext RenderingItemContext { get; }
		IItemFactory Factory { get; }
	}

	public interface IRenderingModel<TPageItem, TRenderingItem, TRenderingParametersItem> : IRenderingModel<TPageItem, TRenderingItem>
		where TPageItem : IItemWrapper
		where TRenderingItem : IItemWrapper
		where TRenderingParametersItem : IRenderingParameterWrapper
	{
		TRenderingParametersItem RenderingParametersItem { get; }
	}
}
