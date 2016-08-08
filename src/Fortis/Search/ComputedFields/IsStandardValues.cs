using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Data;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortis.Search.ComputedFields
{
	public class IsStandardValues : IComputedIndexField
	{
		public string FieldName { get; set; }

		public string ReturnType { get; set; }

		public object ComputeFieldValue(IIndexable indexable)
		{
			var item = (Item)(indexable as SitecoreIndexableItem);

			if (item == null)
			{
				return null;
			}

			var isStandardValues = StandardValuesManager.IsStandardValuesHolder(item);

			return isStandardValues;
		}
	}
}
