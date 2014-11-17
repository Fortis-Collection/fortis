using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Diagnostics;
using Sitecore.ContentSearch.Linq.Common;
using Sitecore.ContentSearch.LuceneProvider;
using Sitecore.ContentSearch.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortis.Search.Lucene
{
	public class SearchProvider : ISearchProvider
	{
		public IQueryable<TResult> GetQueryable<TResult>(IProviderSearchContext context, IExecutionContext executionContext) where TResult : Model.IItemWrapper
		{
			IQueryable<TResult> queryable = null;
			var luceneContext = context as LuceneSearchContext;

			if (luceneContext != null)
			{
				// once the hacks in the Hacks namespace are fixed (around update 2, I hear), the commented line below can be used instead of BugFixIndex
				// in fact once Update 3? is released, this class may become largely irrelevant as interface support is coming natively
				//var linqToLuceneIndex = (executionContext == null) ? new LinqToLuceneIndex<TResult>(context) : new LinqToLuceneIndex<TResult>(context, executionContext);
				var linqToLuceneIndex = (executionContext == null)
											? new CustomLinqToLuceneIndex<TResult>(luceneContext)
											: new CustomLinqToLuceneIndex<TResult>(luceneContext, executionContext);

				if (ContentSearchConfigurationSettings.EnableSearchDebug)
				{
					((IHasTraceWriter)linqToLuceneIndex).TraceWriter = new LoggingTraceWriter(SearchLog.Log);
				}

				queryable = linqToLuceneIndex.GetQueryable();
			}

			return queryable;
		}
	}
}
