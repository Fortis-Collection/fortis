using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Diagnostics;
using Sitecore.ContentSearch.Linq.Common;
using Sitecore.ContentSearch.Utilities;
using Fortis.Model;
using Fortis.Providers;

namespace Fortis.Search
{
	public class ItemSearchFactory : IItemSearchFactory
	{
		protected readonly ITemplateMapProvider _templateMapProvider;

		public ItemSearchFactory(ITemplateMapProvider templateMapProvider)
		{
			_templateMapProvider = templateMapProvider;
		}

		public IQueryable<T> Search<T>(IQueryable<T> queryable)
			where T : IItemWrapper
		{
			if (queryable != null)
			{
				var typeOfT = typeof(T);

				if (_templateMapProvider.InterfaceTemplateMap.ContainsKey(typeOfT))
				{
					var templateId = _templateMapProvider.InterfaceTemplateMap[typeOfT];

					queryable = queryable.Where(item => item.TemplateIds.Contains(templateId));
				}
			}

			return queryable;
		}

		public IQueryable<T> FilteredSearch<T>(IQueryable<T> queryable)
			where T : IItemWrapper
		{
			return Search<T>(queryable).ApplyFilters();
		}

		[Obsolete("Use Search<T> methods which accept an IQueryable<T>")]
		public IQueryable<T> Search<T>(IProviderSearchContext context, IExecutionContext executionContext = null)
			where T : IItemWrapper
		{
			IQueryable<T> queryable = context.GetQueryable<T>(executionContext);

			if (queryable != null)
			{
				queryable = FilteredSearch<T>(queryable);
			}

			return queryable;
		}
	}
}
