using Sitecore.ContentSearch.Linq;
using System.Linq;

namespace Fortis.Search
{
	public class SearchResultsAdapter : ISearchResultsAdapter
	{
		public ISearchResults<T> GetResults<T>(IQueryable<T> searchQueryable)
		{
			var searchResults = searchQueryable.GetResults();
			var results = CreateSearchResults<T>(searchResults);

			return results;
		}

		public SearchResults<T> CreateSearchResults<T>(Sitecore.ContentSearch.Linq.SearchResults<T> searchResults)
		{
			var results = new SearchResults<T>
			{
				Hits = searchResults.Hits.Select(searchHit => CreateSearchHit<T>(searchHit)),
				Facets = searchResults.Facets,
				TotalHits = searchResults.TotalSearchResults
			};

			return results;
		}

		public SearchHit<T> CreateSearchHit<T>(Sitecore.ContentSearch.Linq.SearchHit<T> searchHit)
		{
			var hit = new SearchHit<T>
			{
				Document = searchHit.Document,
				Score = searchHit.Score
			};

			return hit;
		}
	}
}
