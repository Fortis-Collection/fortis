using Sitecore.ContentSearch.Linq.Common;
using Sitecore.ContentSearch.Linq.Lucene;
using Sitecore.ContentSearch.Linq.Parsing;
using Sitecore.ContentSearch.LuceneProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortis.Search
{
	// Thanks to Kam (@kamsar)
	// https://github.com/kamsar/Synthesis/tree/master/Source/Synthesis/ContentSearch/Hacks
	/// <summary>
	/// This class exists to allow us to inject our custom ExpressionParser instance (via the extended GenericQueryable)
	/// </summary>
	public class CustomLinqToLuceneIndex<TItem> : LinqToLuceneIndex<TItem>
	{
		public CustomLinqToLuceneIndex(LuceneSearchContext context)
			: this(context, null) { }

		public CustomLinqToLuceneIndex(LuceneSearchContext context, IExecutionContext executionContext)
			: base(context, executionContext) { }

		public override IQueryable<TItem> GetQueryable()
		{
			GenericQueryable<TItem, LuceneQuery> genericQueryable = new CustomGenericQueryable<TItem, LuceneQuery>(this, QueryMapper, QueryOptimizer, FieldNameTranslator);

			return genericQueryable;
		}
	}

}
