using Sitecore.ContentSearch.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortis.Search
{
	public interface ISearchResults<TSource> : IEnumerable<ISearchHit<TSource>>, IEnumerable
	{
		FacetResults Facets { get; set; }
		IEnumerable<ISearchHit<TSource>> Hits { get; set; }
		int TotalHits { get; set; }
	}
}
