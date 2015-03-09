using System.Collections;
using System.Collections.Generic;

using Sitecore.ContentSearch.Linq;

namespace Fortis.Search
{
	public class SearchResults<TSource> : ISearchResults<TSource>
	{
		public FacetResults Facets { get; set; }
		public IEnumerable<ISearchHit<TSource>> Hits { get; set; }
		public int TotalHits { get; set; }

		IEnumerator<ISearchHit<TSource>> IEnumerable<ISearchHit<TSource>>.GetEnumerator()
		{
			return this.Hits.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.Hits.GetEnumerator();
		}
	}
}
