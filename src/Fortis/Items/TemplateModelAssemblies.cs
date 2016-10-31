using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Fortis.Items
{
	public class TemplateModelAssemblies : ITemplateModelAssemblies
	{
		public IEnumerable<Assembly> Assemblies { get; private set; }

		public TemplateModelAssemblies()
		{
			Assemblies = ProcessAssemblies();
		}

		public IEnumerable<Assembly> ProcessAssemblies()
		{
			var assemblies = AppDomain.CurrentDomain.GetAssemblies();
			var filteredAssemblies = new List<Assembly>();

			foreach (var configurationAssembly in Configuration.Assemblies)
			{
				var assembly = assemblies.FirstOrDefault(a => a.FullName.Equals(configurationAssembly.Value));

				if (assembly == null)
				{
					throw new Exception("Forits: Unable to find item types assembly: " + configurationAssembly.Value);
				}

				filteredAssemblies.Add(assembly);
			}

			return filteredAssemblies;
		}

		public TemplateModelAssembliesConfiguration Configuration => Sitecore.Configuration.Factory.CreateObject("fortis/templates/modelConfiguration", true) as TemplateModelAssembliesConfiguration;
	}
}
