namespace Fortis.Search
{
	public class SearchHit<TSource> : ISearchHit<TSource>
	{
		public TSource Document { get; set; }
		public float Score { get; set; }
	}
}
