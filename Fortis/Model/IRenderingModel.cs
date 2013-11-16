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

	public interface IRenderingModel<TPageItem, TRenderingItem, TRenderingParametersItem> : IRenderingModel<TPageItem, TRenderingItem>
		where TPageItem : IItemWrapper
		where TRenderingItem : IItemWrapper
		where TRenderingParametersItem : IRenderingParameterWrapper
	{
		TRenderingParametersItem RenderingParametersItem { get; }
	}
}
