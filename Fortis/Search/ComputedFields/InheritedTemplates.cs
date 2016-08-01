using Sitecore;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.ContentSearch.Utilities;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortis.Search.ComputedFields
{
	/// <summary>
	/// A computed field that correctly recurses all templates, unlike the default.
	/// 
	/// Code is courtesy of http://mikael.com/2013/05/sitecore-7-query-items-that-inherits-a-template/
	/// </summary>
	public class InheritedTemplates : IComputedIndexField
	{
		public string FieldName { get; set; }

		public string ReturnType { get; set; }

		public object ComputeFieldValue(IIndexable indexable)
		{
			var indexableItem = indexable as SitecoreIndexableItem;

			if (indexableItem == null)
			{
				return null;
			}

			return GetAllTemplates(indexableItem);
		}

		private static List<string> GetAllTemplates(Item item)
		{
			Assert.ArgumentNotNull(item, "item");
			Assert.IsNotNull(item.Template, "Item template not found.");

			var templates = new List<string> { IdHelper.NormalizeGuid(item.TemplateID) };

			RecurseTemplates(templates, item.Template);

			return templates;
		}

		private static void RecurseTemplates(List<string> list, TemplateItem template)
		{
			foreach (var baseTemplateItem in template.BaseTemplates)
			{
				list.Add(IdHelper.NormalizeGuid(baseTemplateItem.ID));

				if (baseTemplateItem.ID != TemplateIDs.StandardTemplate)
				{
					RecurseTemplates(list, baseTemplateItem);
				}
			}
		}
	}
}
