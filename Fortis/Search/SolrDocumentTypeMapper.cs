using Fortis.Model;
using Sitecore.ContentSearch.Linq.Common;
using Sitecore.ContentSearch.Linq.Methods;
using Sitecore.ContentSearch.Security;
using Sitecore.ContentSearch.SolrProvider.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortis.Search
{
	public class SolrDocumentTypeMapper : SolrDocumentPropertyMapper
	{
		public override TElement MapToType<TElement>(Dictionary<string, object> document, SelectMethod selectMethod, IEnumerable<IFieldQueryTranslator> virtualFieldProcessors, SearchSecurityOptions securityOptions)
		{
			var typeOfTElement = typeof(TElement);

			if (!typeof(IItemWrapper).IsAssignableFrom(typeOfTElement))
			{
				return base.MapToType<TElement>(document, selectMethod, virtualFieldProcessors, securityOptions);	
			}

			Guid itemId;
			Guid templateId;

			if (document.ContainsKey("_group") &&
				document.ContainsKey("_template") &&
				Guid.TryParse(document["_group"].ToString(), out itemId) &&
				Guid.TryParse(document["_template"].ToString(), out templateId))
			{
				var item = Spawn.FromItem(itemId, templateId, typeOfTElement, document);

				if (item is TElement)
				{
					return (TElement)item;
				}
			}

			return default(TElement);
		}
	}
}
