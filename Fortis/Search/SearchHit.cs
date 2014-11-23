using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortis.Search
{
	public class SearchHit<TSource> : ISearchHit<TSource>
	{
		public TSource Document { get; set; }
		public float Score { get; set; }
	}
}
