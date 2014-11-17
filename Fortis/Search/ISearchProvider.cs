using Fortis.Model;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortis.Search
{
	public interface ISearchProvider
	{
		IQueryable<TResult> GetQueryable<TResult>(IProviderSearchContext context, IExecutionContext executionContext) where TResult : IItemWrapper;
	}
}
