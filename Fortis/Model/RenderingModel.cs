using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortis.Model
{
	public class RenderingModel<TPageItem, TRenderingItem> : IRenderingModel<TPageItem, TRenderingItem>
		where TPageItem : IItemWrapper
		where TRenderingItem : IItemWrapper
	{
		private readonly IItemFactory _itemFactory;
		private readonly TPageItem _pageItem;
		private readonly IRenderingContext _pageContext;
		private readonly TRenderingItem _renderingItem;
		private readonly IRenderingContext _renderingItemContext;

		public RenderingModel(TPageItem pageItem, TRenderingItem renderingItem, IItemFactory factory = null)
		{
			_pageItem = pageItem;
			_pageContext = new RenderingContext(pageItem);
			_renderingItem = renderingItem;
			_renderingItemContext = new RenderingContext(renderingItem);
			_itemFactory = factory;
		}

		public virtual TPageItem PageItem
		{
			get { return _pageItem; }
		}

		public virtual IRenderingContext PageContext { get { return _pageContext; } }

		public virtual TRenderingItem RenderingItem
		{
			get { return _renderingItem; }
		}

		public virtual IRenderingContext RenderingItemContext { get { return _pageContext; } }

		public virtual IItemFactory Factory { get { return _itemFactory; } }
	}

	public class RenderingModel<TPageItem, TRenderingItem, TRenderingParametersItem> : RenderingModel<TPageItem, TRenderingItem>, IRenderingModel<TPageItem, TRenderingItem, TRenderingParametersItem>
		where TPageItem : IItemWrapper
		where TRenderingItem : IItemWrapper
		where TRenderingParametersItem : IRenderingParameterWrapper
	{
		private readonly TRenderingParametersItem _renderingParametersItem;

		public RenderingModel(TPageItem pageItem, TRenderingItem renderingItem, TRenderingParametersItem renderingParametersItem, IItemFactory factory = null)
			: base(pageItem, renderingItem, factory)
		{
			_renderingParametersItem = renderingParametersItem;
		}

		public virtual TRenderingParametersItem RenderingParametersItem
		{
			get { return _renderingParametersItem; }
		}
	}
}
