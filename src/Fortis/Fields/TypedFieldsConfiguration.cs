using Fortis.Configuration.Xml;
using Sitecore.Xml;
using System.Collections.Generic;
using System.Xml;
using System;

namespace Fortis.Fields
{
	public class TypedFieldsConfiguration : XmlDictionaryConfiguration
	{
		public Dictionary<string, string> TypedFields => Dictionary;

		protected override string KeyAttributeName => "fieldType";
		protected override string ValueAttributeName => "factoryName";
	}
}
