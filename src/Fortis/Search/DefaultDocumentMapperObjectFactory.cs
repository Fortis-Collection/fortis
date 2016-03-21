using System;
using System.Collections.Generic;
using System.Linq;
using Fortis.Model;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq.Common;

namespace Fortis.Search
{
	public class DefaultDocumentMapperObjectFactory : Sitecore.ContentSearch.DefaultDocumentMapperObjectFactory, IIndexDocumentPropertyMapperObjectFactory
	{
		public new object CreateElementInstance(Type baseType, IDictionary<string, object> fieldValues, IEnumerable<IExecutionContext> executionContexts)
		{
			if (!typeof(IItemWrapper).IsAssignableFrom(baseType))
				return base.CreateElementInstance(baseType, fieldValues, executionContexts);

			Guid groupID;
			Guid templateID;

			if (fieldValues.ContainsKey("_group") && fieldValues.ContainsKey("_template")
				&& (Guid.TryParse(fieldValues["_group"].ToString(), out groupID)
				&& Guid.TryParse(fieldValues["_template"].ToString(), out templateID)))
			{
				return Global.SpawnProvider.FromItem(groupID, templateID, baseType, fieldValues.ToDictionary(k => k.Key, v => v.Value));
			}

			return base.CreateElementInstance(baseType, fieldValues, executionContexts);
		}
	}
}