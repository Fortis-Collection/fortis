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
		private readonly TRenderingItem _renderingItem;

		public RenderingModel(TPageItem pageItem, TRenderingItem renderingItem, IItemFactory factory = null)
		{
			_pageItem = pageItem;
			_renderingItem = renderingItem;
			_itemFactory = factory;
		}

		public TPageItem PageItem
		{
			get { return _pageItem; }
		}

		public TRenderingItem RenderingItem
		{
			get { return _renderingItem; }
		}

		public IItemFactory Factory { get { return _itemFactory; } }
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

		public TRenderingParametersItem RenderingParametersItem
		{
			get { return _renderingParametersItem; }
		}
	}
}
