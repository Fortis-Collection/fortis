namespace Fortis.Configuration
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Xml;

	using Fortis.Exceptions;

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

			var modelAssemblies = new List<IFortisModelConfiguration>();
			foreach (XmlElement element in modelNodes)
			{
				var model = LoadModelConfiguration(element);

				if (modelAssemblies.Any(x => x.Name.Equals(model.Name, StringComparison.OrdinalIgnoreCase)))
				{
					throw new InvalidOperationException("The Fortis Model Assembly '" + model.Name + "' is defined twice. Model Assemblies should have unique names.");
				}

				modelAssemblies.Add(model);
			}

			var supportedFields = new List<ISupportedFieldType>();
			var fieldNodes = configNode.SelectNodes("./fieldTypes/field");

			if (fieldNodes == null || fieldNodes.Count == 0)
			{
				throw new FortisConfigurationException("No supported fields configured");
			}

			foreach (XmlElement field in fieldNodes)
			{
				var supportedField = LoadFieldTypeConfiguration(field);
				if (supportedFields.Any(x => x.FieldName.Equals(supportedField.FieldName, StringComparison.InvariantCultureIgnoreCase)))
				{
					throw new InvalidOperationException("The Fortis Supported Field Type '" + supportedField.FieldName + "' is defined more than once.");
				}

				supportedFields.Add(supportedField);
			}

			_defaultConfiguration.Models = modelAssemblies;
			_defaultConfiguration.Fields = supportedFields;
		}

		protected virtual IFortisModelConfiguration LoadModelConfiguration(XmlElement configuration)
		{
			var name = GetAttributeValue(configuration, "name");

			Assert.IsNotNullOrEmpty(name, "Model assembly node had empty or missing name attribute.");

			var type = GetAttributeValue(configuration, "assembly");
			Assert.IsNotNullOrEmpty(type, "Model assembly node had empty or missing type attribute.");

			return new FortisModelConfiguration(name, type);
		}

		protected virtual ISupportedFieldType LoadFieldTypeConfiguration(XmlElement configuration)
		{
			var name = GetAttributeValue(configuration, "name");

			Assert.IsNotNullOrEmpty(name, "Field type node had empty or missing name attribute.");

			var type = GetAttributeValue(configuration, "type");
			Assert.IsNotNullOrEmpty(type, "Field type node had empty or missing type attribute.");

			return new SupportedFieldType(name, type);
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