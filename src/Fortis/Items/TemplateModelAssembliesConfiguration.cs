using Fortis.Configuration.Xml;
using System.Collections.Generic;

namespace Fortis.Items
{
	public class TemplateModelAssembliesConfiguration : XmlDictionaryConfiguration
	{
		public Dictionary<string, string> Assemblies => Dictionary;

		protected override string KeyAttributeName => "name";
		protected override string ValueAttributeName => "assembly";
	}
}
