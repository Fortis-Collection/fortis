using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using Sitecore.Data.Items;
using Fortis.Providers;
using Sitecore.Web.UI.WebControls;
using System.Web;

namespace Fortis.Model
{
	public class RenderingParameterFactory : IRenderingParameterFactory
	{
		protected readonly ISpawnProvider SpawnProvider;

		public RenderingParameterFactory(ISpawnProvider spawnProvider)
		{
			SpawnProvider = spawnProvider;
		}

		private Item GetRenderingByPath(string path)
		{
			var items = Sitecore.Context.Database.SelectItems("/sitecore/layout//*");

			foreach (var item in items)
			{
				if (item["Path"].Equals(path, StringComparison.InvariantCultureIgnoreCase))
				{
					return item;
				}
			}

			return null;
		}

		public T GetParameters<T>(Control control) where T : IRenderingParameterWrapper
		{
			if (control.Parent is Sublayout)
			{
				var sublayout = (Sublayout)control.Parent;
				var renderingItem = GetRenderingByPath(sublayout.Path);

				if (renderingItem != null)
				{
					var parsedParameters = HttpUtility.ParseQueryString(sublayout.Parameters);
					var parameters = new Dictionary<string, string>();

					foreach (var parameterKey in parsedParameters.AllKeys)
					{
						if (!parameters.ContainsKey(parameterKey))
						{
							parameters.Add(parameterKey, parsedParameters[parameterKey]);
						}
					}

					var wrapper = SpawnProvider.FromRenderingParameters<T>(renderingItem, parameters);
					return (T)((wrapper is T) ? wrapper : default(T));
				}
			}

			return default(T);
		}
	}
}
