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
	public class IsStandardValue : AbstractComputedIndexField
	{
		public override object ComputeFieldValue(IIndexable indexable)
		{
			var item = (Item)(indexable as SitecoreIndexableItem);
			var isStandardValues = StandardValuesManager.IsStandardValuesHolder(item);

			return isStandardValues;
		}
	}
}
