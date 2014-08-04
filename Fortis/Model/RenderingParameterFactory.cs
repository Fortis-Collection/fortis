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
		private Dictionary<string, Guid> _renderings;
		private object _renderingsLock = new object();

		protected readonly ISpawnProvider SpawnProvider;

		public RenderingParameterFactory(ISpawnProvider spawnProvider)
		{
			SpawnProvider = spawnProvider;
		}

		private Item GetRenderingByPath(string path)
		{
			if (Renderings.Any(r => r.Key.Equals(path, StringComparison.InvariantCultureIgnoreCase)))
			{
				var rendering = Renderings.FirstOrDefault(r => r.Key.Equals(path, StringComparison.InvariantCultureIgnoreCase));

				return Sitecore.Context.Database.GetItem(Sitecore.Data.ID.Parse(rendering.Value));
			}

			return null;
		}

		public T GetParameters<T>(Control control)
			where T : IRenderingParameterWrapper
		{
			if (control.Parent is Sublayout)
			{
				var sublayout = (Sublayout)control.Parent;

				var parsedParameters = HttpUtility.ParseQueryString(sublayout.Parameters);
				var parameters = new Dictionary<string, string>();

				foreach (var parameterKey in parsedParameters.AllKeys)
				{
					if (!parameters.ContainsKey(parameterKey))
					{
						parameters.Add(parameterKey, parsedParameters[parameterKey]);
					}
				}

				return GetParameters<T>(sublayout.Path, parameters);
			}

			return default(T);
		}

		public T GetParameters<T>(string filePath, Dictionary<string, string> parameters)
			where T : IRenderingParameterWrapper
		{
			var renderingItem = GetRenderingByPath(filePath);

			if (renderingItem != null)
			{
				var wrapper = SpawnProvider.FromRenderingParameters<T>(renderingItem, parameters);
				return (T)((wrapper is T) ? wrapper : default(T));
			}

			return default(T);
		}

		public Dictionary<string, Guid> Renderings
		{
			get
			{
				if (_renderings == null)
				{
					lock (_renderingsLock)
					{
						if (_renderings == null)
						{
							_renderings = new Dictionary<string, Guid>();

							var items = Sitecore.Context.Database.SelectItems("fast:/sitecore/layout//*");

							foreach (var item in items)
							{
								var filePath = item["Path"];

								if (!string.IsNullOrWhiteSpace(filePath) && !_renderings.ContainsKey(filePath))
								{
									_renderings.Add(filePath, item.ID.Guid);
								}
							}
						}
					}
				}

				return _renderings;
			}
		}
	}
}
