using Sitecore.Configuration;
using System.Collections.Generic;

namespace Fortis.Items
{
	public class TemplateModelAssembliesConfiguration : ITemplateModelAssembliesConfiguration
	{
		public IEnumerable<ITemplateModelAssembly> Assemblies => Configuration.Assemblies;

		public SitecoreTemplateModelAssembliesConfiguration Configuration => Factory.CreateObject("fortis/templates/modelConfiguration", true) as SitecoreTemplateModelAssembliesConfiguration;
	}
}
