using System;
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
				// Check for a sitecore query datasource
				var query = RenderingContext.Current.Rendering.DataSource;
				if (query.StartsWith("./", StringComparison.InvariantCulture))
				{
					// Relative to the current context item
					var contextItem = RenderingContext.Current.PageContext.Item;
					if (contextItem != null)
					{
						var item = contextItem.Axes.SelectSingleItem(query);
						return item;
					}
				}
				else if (!string.IsNullOrEmpty(query))
				{
					// Straight sitecore query
					return RenderingContext.Current.ContextItem.Database.SelectSingleItem(query);
				}
				// Item Id set in the datasource
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
