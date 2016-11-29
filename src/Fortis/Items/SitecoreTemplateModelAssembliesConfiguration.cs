using Fortis.Configuration.Xml;
using System.Linq;
using System.Collections.Generic;

namespace Fortis.Items
{
	public class SitecoreTemplateModelAssembliesConfiguration : XmlDictionaryConfiguration
	{
		public List<TemplateModelAssembly> Assemblies => Dictionary.Select(kvp => new TemplateModelAssembly
		{
			Assembly = kvp.Value,
			Name = kvp.Key
		}).ToList();

		protected override string KeyAttributeName => "name";
		protected override string ValueAttributeName => "assembly";
	}
}
