namespace Fortis.Model
{
	public interface IRenderingModel<TPageItem, TRenderingItem>
		where TPageItem : IItemWrapper
		where TRenderingItem : IItemWrapper
	{
		TPageItem PageItem { get; }
		TRenderingItem RenderingItem { get; }
	}
}
