namespace Fortis.Tests.Configuration
{
	using System.IO;
	using System.Reflection;
	using System.Xml;

	using Fortis.Configuration;

	internal class TestXmlConfigurationProvider : XmlConfigurationProvider
	{
		protected override XmlNode GetConfigurationNode()
		{
			var assembly = Assembly.GetExecutingAssembly();
			string text;

			// ReSharper disable once AssignNullToNotNullAttribute
			using (var textStreamReader = new StreamReader(assembly.GetManifestResourceStream("Fortis.Tests.Configuration.TestXmlConfiguration.xml")))
			{
				text = textStreamReader.ReadToEnd();
			}

			var doc = new XmlDocument();
			doc.LoadXml(text);

			return doc.DocumentElement;
		}
	}
}