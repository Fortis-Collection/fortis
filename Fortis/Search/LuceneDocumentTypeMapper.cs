using Sitecore.Data;

namespace Fortis.Search
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Model;

	using Lucene.Net.Documents;

	using Sitecore.ContentSearch.Linq.Common;
	using Sitecore.ContentSearch.LuceneProvider;
	using Sitecore.Diagnostics;

	public class LuceneDocumentTypeMapper : DefaultLuceneDocumentTypeMapper
	{
		public override TElement MapToType<TElement>(Document document, Sitecore.ContentSearch.Linq.Methods.SelectMethod selectMethod, IEnumerable<IFieldQueryTranslator> virtualFieldProcessors, Sitecore.ContentSearch.Security.SearchSecurityOptions securityOptions)
		{
			var typeOfTElement = typeof(TElement);

			if (!typeof(IItemWrapper).IsAssignableFrom(typeOfTElement))
			{
				return base.MapToType<TElement>(document, selectMethod, virtualFieldProcessors, securityOptions);
			}

			Guid itemId;
			Guid templateId;

			var fields = ExtractFieldsFromDocument(document, virtualFieldProcessors);

			if (fields.ContainsKey("_group") &&
				fields.ContainsKey("_template") &&
				Guid.TryParse(fields["_group"].ToString(), out itemId) &&
				Guid.TryParse(fields["_template"].ToString(), out templateId))
			{
				var item = Global.SpawnProvider.FromItem(itemId, templateId, typeOfTElement, fields);

				if (item is TElement)
				{
					return (TElement)item;
				}
			}

		    if (fields.ContainsKey("_uniqueid"))
		    {
		        var id = fields["_uniqueid"].ToString();

		        var uri = ItemUri.Parse(id);
		        var item = Sitecore.Context.Database.GetItem(uri.ToDataUri());

		        if (item != null)
		        {
		            var mappedItem = Global.SpawnProvider.FromItem(item);
		            if (mappedItem is TElement)
		            {
		                return (TElement) mappedItem;
		            }
		        }
		    }

			return default(TElement);
		}

		private Dictionary<string, object> ExtractFieldsFromDocument(Document document, IEnumerable<IFieldQueryTranslator> virtualFieldProcessors)
		{
			Assert.ArgumentNotNull(document, "document");

			IDictionary<string, object> dictionary = new Dictionary<string, object>();

			foreach (var grouping in document.GetFields().GroupBy(f => f.Name))
			{
				if (grouping.Count() > 1)
				{
					dictionary[grouping.Key] = string.Join("|", grouping.Select(x => x.StringValue));
				}
				else
				{
					dictionary[grouping.Key] = grouping.First().StringValue;
				}
			}

			if (virtualFieldProcessors != null)
			{
				dictionary = virtualFieldProcessors.Aggregate(dictionary, (current, processor) => processor.TranslateFieldResult(current, index.FieldNameTranslator));
			}

			return dictionary.ToDictionary(x => x.Key, x => x.Value);
		}
	}
}
