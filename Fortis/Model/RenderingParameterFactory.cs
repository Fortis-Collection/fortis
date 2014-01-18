using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using Sitecore.Data.Items;
using Fortis.Providers;

namespace Fortis.Model
{
	public class RenderingParameterFactory : IRenderingParameterFactory
	{
		protected readonly ISpawnProvider SpawnProvider;

		private Dictionary<Guid, Type> _templateMap = null;

		private Dictionary<Guid, Type> TemplateMap
		{
			get
			{
				if (_templateMap == null)
				{
					_templateMap = new Dictionary<Guid, Type>();
					var assembly = System.Reflection.Assembly.GetCallingAssembly();
					foreach (Type t in assembly.GetTypes())
					{
						foreach (TemplateMappingAttribute templateAttribute in t.GetCustomAttributes(typeof(TemplateMappingAttribute), false))
						{
							if (templateAttribute.Type == "RenderingParameter")
							{
								if (!_templateMap.Keys.Contains(templateAttribute.Id))
								{
									_templateMap.Add(templateAttribute.Id, t);
								}
							}
						}
					}
				}
				return _templateMap;
			}
		}

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

		private IRenderingParameterWrapper SpawnTypeFromItem(Guid id, string queryString)
		{
			if (TemplateMap.Keys.Contains(id))
			{
				// Get type information
				var type = TemplateMap[id];
				// Get public constructors
				var ctors = type.GetConstructors();
				// Invoke the first public constructor with no parameters.
				return (IRenderingParameterWrapper)ctors[0].Invoke(new object[] { queryString, SpawnProvider });
			}

			var parameters = new Dictionary<string, string>();
			var queryStringParameters = Sitecore.Web.WebUtil.ParseUrlParameters(queryString);

			foreach (var parameterKey in queryStringParameters.AllKeys)
			{
				if (!parameters.ContainsKey(parameterKey))
				{
					parameters.Add(parameterKey, queryStringParameters[parameterKey]);
				}
			}

			return new RenderingParameterWrapper(parameters, SpawnProvider);
		}

		private IEnumerable<T> FilterWrapperTypes<T>(IEnumerable<IItemWrapper> wrappers)
		{
			return (IEnumerable<T>)(wrappers.Where(w => w is T));
		}

		public T GetParameters<T>(Control control) where T : IRenderingParameterWrapper
		{
			if (control.Parent is Sitecore.Web.UI.WebControls.Sublayout)
			{
				string queryString = ((Sitecore.Web.UI.WebControls.Sublayout)control.Parent).Parameters;
				string path = ((Sitecore.Web.UI.WebControls.Sublayout)control.Parent).Path;
				var renderingItem = GetRenderingByPath(path);
				if (renderingItem != null)
				{
					Guid parametersTemplateId;
					if (Guid.TryParse(renderingItem["Parameters Template"], out parametersTemplateId))
					{
						var wrapper = SpawnTypeFromItem(parametersTemplateId, queryString);
						return (T)((wrapper is T) ? wrapper : default(T));
					}
				}
			}

			return default(T);
		}
	}
}
