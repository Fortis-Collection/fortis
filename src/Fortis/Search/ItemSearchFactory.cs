using System;
using System.Linq;

using Fortis.Model;
using Fortis.Providers;

using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq.Common;

namespace Fortis.Search
{
	public class ItemSearchFactory : IItemSearchFactory
	{
		protected readonly ITemplateMapProvider TemplateMapProvider;
		protected readonly ISearchResultsAdapter SearchResultsAdapter;

		public ItemSearchFactory(
			ITemplateMapProvider templateMapProvider,
			ISearchResultsAdapter searchResultsAdapter)
		{
			TemplateMapProvider = templateMapProvider;
			SearchResultsAdapter = searchResultsAdapter;
		}

		public virtual IQueryable<T> Search<T>(IQueryable<T> queryable)
			where T : IItemWrapper
		{
			if (queryable != null)
			{
				queryable = queryable.WhereTemplate(TemplateMapProvider);
			}

			return queryable;
		}

		public virtual IQueryable<T> FilteredSearch<T>(IQueryable<T> queryable)
			where T : IItemWrapper
		{
			return Search<T>(queryable).ApplyFilters();
		}

		public virtual ISearchResults<T> GetResults<T>(IQueryable<T> queryable)
			where T : IItemWrapper
		{
			return SearchResultsAdapter.GetResults<T>(queryable);
		}

		[Obsolete("Use Search<T> methods which accept an IQueryable<T>")]
		public virtual IQueryable<T> Search<T>(IProviderSearchContext context, IExecutionContext executionContext = null)
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
