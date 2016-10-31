using Fortis.Configuration.Xml;
using System.Collections.Generic;

namespace Fortis.Fields
{
	public class TypedFieldsConfiguration : XmlDictionaryConfiguration
	{
		public Dictionary<string, string> TypedFields => Dictionary;

		protected override string KeyAttributeName => "fieldType";
		protected override string ValueAttributeName => "factoryName";
	}
}
