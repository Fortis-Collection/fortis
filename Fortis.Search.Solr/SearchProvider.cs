using Fortis.Model;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Diagnostics;
using Sitecore.ContentSearch.Linq.Common;
using Sitecore.ContentSearch.SolrProvider;
using Sitecore.ContentSearch.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortis.Search.Solr
{
	public class SearchProvider : ISearchProvider
	{
		public IQueryable<TResult> GetQueryable<TResult>(IProviderSearchContext context, IExecutionContext executionContext) where TResult : IItemWrapper
		{
			IQueryable<TResult> queryable = null;
			var solrContext = context as SolrSearchContext;

			if (solrContext != null)
			{
				// once the hacks in the Hacks namespace are fixed (around update 2, I hear), the commented line below can be used instead of BugFixIndex
				// in fact once Update 3? is released, this class may become largely irrelevant as interface support is coming natively
				//var linqToSolrIndex = (executionContext == null) ? new LinqToSolrIndex<TResult>(context) : new LinqToSolrIndex<TResult>(context, executionContext);
				var linqToSolrIndex = (executionContext == null)
											? new CustomLinqToSolrIndex<TResult>(solrContext)
											: new CustomLinqToSolrIndex<TResult>(solrContext, executionContext);

				if (ContentSearchConfigurationSettings.EnableSearchDebug)
				{
					((IHasTraceWriter)linqToSolrIndex).TraceWriter = new LoggingTraceWriter(SearchLog.Log);
				}

				queryable = linqToSolrIndex.GetQueryable();
			}

			return queryable;
		}
	}
}
