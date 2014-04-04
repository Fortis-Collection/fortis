using Fortis.Providers;
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
					var source = RenderingContext.Current.Rendering.DataSource.Split(':');
					if (source.Length > 1)
					{
						var query = source[1];
						var db = RenderingContext.Current.ContextItem.Database;
						if (db != null)
						{
							return db.SelectSingleItem(query);
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
