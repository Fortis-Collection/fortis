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
		private readonly TPageItem _pageItem;
		private readonly TRenderingItem _renderingItem;

		public RenderingModel(TPageItem pageItem, TRenderingItem renderingItem)
		{
			_pageItem = pageItem;
			_renderingItem = renderingItem;
		}

		public TPageItem PageItem
		{
			get { return _pageItem; }
		}

		public TRenderingItem RenderingItem
		{
			get { return _renderingItem; }
		}
	}

	public class RenderingModel<TPageItem, TRenderingItem, TRenderingParametersItem> : RenderingModel<TPageItem, TRenderingItem>, IRenderingModel<TPageItem, TRenderingItem, TRenderingParametersItem>
		where TPageItem : IItemWrapper
		where TRenderingItem : IItemWrapper
		where TRenderingParametersItem : IItemWrapper
	{
		private readonly TRenderingParametersItem _renderingParametersItem;

		public RenderingModel(TPageItem pageItem, TRenderingItem renderingItem, TRenderingParametersItem renderingParametersItem)
			: base(pageItem, renderingItem)
		{
			_renderingParametersItem = renderingParametersItem;
		}

		public TRenderingParametersItem RenderingParametersItem
		{
			get { return _renderingParametersItem; }
		}
	}
}
