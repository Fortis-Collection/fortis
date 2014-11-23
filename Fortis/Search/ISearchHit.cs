using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortis.Search
{
	public interface ISearchHit<TSource>
	{
		TSource Document { get; }
		float Score { get; }
	}
}
