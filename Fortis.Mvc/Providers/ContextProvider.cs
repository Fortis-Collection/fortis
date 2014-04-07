using Fortis.Providers;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using System.Collections.Generic;

namespace Fortis.Mvc.Providers
{
	public class ContextProvider : IContextProvider
	{

		public Item RenderingContextItem
		{
			get
			{
				if (RenderingContext.Current.Rendering.DataSource.StartsWith("query"))
				{
					if (RenderingContext.Current.Rendering.DataSource.Length > 1)
					{
						var contextItem = RenderingContext.Current.PageContext.Item;
						if (contextItem != null)
						{
							var item = contextItem.Axes.SelectSingleItem(RenderingContext.Current.Rendering.DataSource.Replace("query:", ""));
							return item;
						}
					}
				}
				return RenderingContext.Current.Rendering.Item;
			}
		}

		public Item PageContextItem
		{
			get { return PageContext.Current.Item; }
		}

		public Item RenderingItem
		{
			get { return RenderingContext.Current.Rendering.RenderingItem.InnerItem; }
		}

		public Dictionary<string, string> RenderingParameters
		{
			get
			{
				var renderingParameters = new Dictionary<string, string>();

				foreach (var parameter in RenderingContext.Current.Rendering.Parameters)
				{
					if (!renderingParameters.ContainsKey(parameter.Key))
					{
						renderingParameters.Add(parameter.Key, parameter.Value);
					}
				}

				return renderingParameters;
			}
		}
	}
}
