namespace Fortis.Configuration
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Xml;

	using Sitecore.Configuration;
	using Sitecore.Diagnostics;

	/// <summary>
	/// Reads Unicorn dependency configurations from XML (e.g. Sitecore web.config section sitecore/unicorn)
	/// </summary>
	public class XmlConfigurationProvider : IConfigurationProvider
	{
		private IFortisConfiguration _defaultConfiguration;

		public IFortisConfiguration DefaultConfiguration
		{
			get
			{
				if (_defaultConfiguration == null)
				{
					LoadConfiguration();
				}

				return _defaultConfiguration;
			}
		}

		protected virtual XmlNode GetConfigurationNode()
		{
			return Factory.GetConfigNode("/sitecore/fortis");
		}

		protected virtual void LoadConfiguration()
		{
			var configNode = GetConfigurationNode();
			Assert.IsNotNull(configNode, "Root Fortis config node not found. Missing Fortis.config?");

			var modelNodes = configNode.SelectNodes("./models/model");
			_defaultConfiguration = new FortisConfiguration();

			// no model assemblies defined let's get outta here
			if (modelNodes == null || modelNodes.Count == 0)
			{
				_defaultConfiguration.Models = new List<IFortisModelConfiguration>();
				return;
			}

			var nameChecker = new List<IFortisModelConfiguration>();
			foreach (XmlElement element in modelNodes)
			{
				var model = LoadModelConfiguration(element);

				if (nameChecker.Any(x => x.Name.Equals(model.Name, StringComparison.OrdinalIgnoreCase)))
				{
					throw new InvalidOperationException("The Fortis Model Assembley '" + model.Name + "' is defined twice. Model Assemblies should have unique names.");
				}

				nameChecker.Add(model);
			}

			_defaultConfiguration.Models = nameChecker;
		}

		protected virtual IFortisModelConfiguration LoadModelConfiguration(XmlElement configuration)
		{
			var name = GetAttributeValue(configuration, "name");

			Assert.IsNotNullOrEmpty(name, "Model assembley node had empty or missing name attribute.");

			var type = GetAttributeValue(configuration, "type");
			Assert.IsNotNullOrEmpty(type, "Model assembley node had empty or missing type attribute.");

			return new FortisModelConfiguration(name, type);
		}

		/// <summary>
		/// Gets an XML attribute value, returning null if it does not exist and its inner text otherwise.
		/// </summary>
		protected virtual string GetAttributeValue(XmlNode node, string attribute)
		{
			return node?.Attributes?[attribute]?.InnerText;
		}
	}
}