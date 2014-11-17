using Sitecore.ContentSearch.Linq.Common;
using Sitecore.ContentSearch.Linq.Parsing;
using Sitecore.ContentSearch.Linq.Solr;
using Sitecore.ContentSearch.SolrProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortis.Search.Solr
{
	// Thanks to Kam (@kamsar)
	// https://github.com/kamsar/Synthesis/tree/master/Source/Synthesis/ContentSearch/Hacks
	/// <summary>
	/// This class exists to allow us to inject our custom ExpressionParser instance (via the extended GenericQueryable)
	/// </summary>
	public class CustomLinqToSolrIndex<TItem> : LinqToSolrIndex<TItem>
	{
		public CustomLinqToSolrIndex(SolrSearchContext context)
			: this(context, null) { }

		public CustomLinqToSolrIndex(SolrSearchContext context, IExecutionContext executionContext)
			: base(context, executionContext) { }

		public override IQueryable<TItem> GetQueryable()
		{
			GenericQueryable<TItem, SolrCompositeQuery> genericQueryable = new CustomGenericQueryable<TItem, SolrCompositeQuery>(this, QueryMapper, QueryOptimizer, FieldNameTranslator);

			return genericQueryable;
		}
	}
}
