using Sitecore.Xml;
using System.Collections.Generic;
using System.Xml;

namespace Fortis.Configuration.Xml
{
	public abstract class XmlDictionaryConfiguration
	{
		protected abstract string KeyAttributeName { get; }
		protected abstract string ValueAttributeName { get; }
		protected Dictionary<string, string> Dictionary = new Dictionary<string, string>();

		public void Add(XmlNode node)
		{
			var key = XmlUtil.GetAttribute(KeyAttributeName, node);
			var value = XmlUtil.GetAttribute(ValueAttributeName, node);

			Dictionary.Add(key, value);
		}
	}
}
