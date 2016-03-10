using Fortis.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortis.Search
{
	public interface ISearchResultsAdapter
	{
		ISearchResults<T> GetResults<T>(IQueryable<T> searchQueryable);
	}
}
