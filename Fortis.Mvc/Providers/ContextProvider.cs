namespace Fortis.Mvc.Providers
{
	using Fortis.Providers;
	using Sitecore.Data.Items;
	using Sitecore.Mvc.Presentation;
	using System.Collections.Generic;
	using System.Linq;

	public class ContextProvider : IContextProvider
	{
		public Item RenderingContextItem
		{
			get { return RenderingContext.Current.Rendering.Item; }
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
