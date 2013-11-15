using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Sitecore.Data.Items;
using Sitecore.Web.UI.WebControls;

namespace Fortis.Model.RenderingParameters.Fields
{
	public class RenderingParameterFactory : IRenderingParameterFactory
	{
		private Dictionary<Guid, Type> _templateMap;
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
								if (!_templateMap.ContainsKey(templateAttribute.Id))
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

		private Item GetRenderingByPath(string path)
		{
			var items = Sitecore.Context.Database.SelectItems("/sitecore/layout//*");
			return items.FirstOrDefault(item => item["Path"].Equals(path, StringComparison.InvariantCultureIgnoreCase));
		}

		private IRenderingParameterWrapper SpawnTypeFromItem(Guid id, string queryString)
		{
			if (TemplateMap.ContainsKey(id))
			{
				// Get type information
				var type = TemplateMap[id];
				// Get public constructors
				var ctors = type.GetConstructors();
				// Invoke the first public constructor with no parameters.
				return (IRenderingParameterWrapper)ctors[0].Invoke(new object[] { queryString });
			}
			return new RenderingParameterWrapper(queryString);
		}

		public T GetParameters<T>(Control control) where T : IRenderingParameterWrapper
		{
			var parent = control.Parent as Sublayout;
			if (parent != null)
			{
				var queryString = parent.Parameters;
				var path = parent.Path;
				//string renderingId = ((Sitecore.Web.UI.WebControls.Sublayout)control.Parent).RenderingID;
				var renderingItem = GetRenderingByPath(path); //Sitecore.Context.Database.GetItem(renderingId);
				if (renderingItem != null)
				{
					Guid parametersTemplateId;
					if (Guid.TryParse(renderingItem["Parameters Template"], out parametersTemplateId))
					{
						var wrapper = SpawnTypeFromItem(parametersTemplateId, queryString);
						return (T) ((wrapper is T) ? wrapper : default(T));
					}
				}
			}

			return default(T);
		}
	}
}
