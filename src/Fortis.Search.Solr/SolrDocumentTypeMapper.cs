using Fortis.Model;
using Sitecore.ContentSearch.Linq.Common;
using Sitecore.ContentSearch.Linq.Methods;
using Sitecore.ContentSearch.Security;
using Sitecore.ContentSearch.SolrProvider.Mapping;
using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
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

			if (document.ContainsKey(Templates.Fields.Group) &&
				document.ContainsKey(Templates.Fields.TemplateName) &&
				Guid.TryParse(document[Templates.Fields.Group].ToString(), out itemId) &&
				Guid.TryParse(document[Templates.Fields.TemplateName].ToString(), out templateId))
			{
				var item = Global.SpawnProvider.FromItem(itemId, templateId, typeOfTElement, document);

				if (item is TElement)
				{
					return (TElement)item;
				}
			}

			return default(TElement);
		}
	}
}
