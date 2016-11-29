using Fortis.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Fortis.Items
{
	public class TemplateModelAssemblies : ITemplateModelAssemblies
	{
		protected readonly IApplicationAssemblies ApplicationAssemblies;
		protected readonly ITemplateModelAssembliesConfiguration Configuration;

		public IEnumerable<Assembly> Assemblies { get; private set; }

		public TemplateModelAssemblies(
			IApplicationAssemblies applicationAssemblies,
			ITemplateModelAssembliesConfiguration configuration)
		{
			ApplicationAssemblies = applicationAssemblies;
			Configuration = configuration;

			Assemblies = AggregateAssemblies();
		}

		public IEnumerable<Assembly> AggregateAssemblies()
		{
			var assemblies = ApplicationAssemblies.Assemblies;
			var filteredAssemblies = new List<Assembly>();

			foreach (var configurationAssembly in Configuration.Assemblies)
			{
				var assembly = assemblies.FirstOrDefault(a => a.FullName.Equals(configurationAssembly.Assembly));

				if (assembly == null)
				{
					throw new Exception("Forits: Unable to find item types assembly: " + configurationAssembly.Assembly);
				}

				filteredAssemblies.Add(assembly);
			}

			return filteredAssemblies;
		}
	}
}
