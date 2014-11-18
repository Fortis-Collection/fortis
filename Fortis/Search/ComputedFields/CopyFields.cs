using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Data.Items;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Fortis.Search.ComputedFields
{
	public class CopyFields : AbstractComputedIndexField
	{
		public readonly List<string> CopyFromFields;

		public CopyFields()
			: this(null)
		{

		}

		public CopyFields(XmlNode configurationNode)
		{
			CopyFromFields = ParseCopyFromFields(configurationNode);
		}

		public override object ComputeFieldValue(IIndexable indexable)
		{
			var computedField = new List<string>();

			if (CopyFromFields.Any())
			{
				var item = (Item)(indexable as SitecoreIndexableItem);

				if (item != null)
				{
					foreach (var copyFromField in CopyFromFields)
					{
						var value = item[copyFromField];

						if (!string.IsNullOrEmpty(value))
						{
							computedField.Add(value);
						}
					}
				}
			}

			return computedField;
		}

		protected virtual List<string> ParseCopyFromFields(XmlNode configurationNode)
		{
			var copyFromFields = new List<string>();

			if (configurationNode != null)
			{
				var configNode = configurationNode.SelectSingleNode("copyFields");

				if (configNode != null)
				{
					foreach (XmlNode childConfigNode in configNode.ChildNodes)
					{
						if (!string.IsNullOrEmpty(childConfigNode.InnerText))
						{
							copyFromFields.Add(childConfigNode.InnerText);
						}
					}
				}
			}

			return copyFromFields;
		}
	}
}
