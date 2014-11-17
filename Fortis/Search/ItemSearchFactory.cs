using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Diagnostics;
using Sitecore.ContentSearch.Linq.Common;
using Sitecore.ContentSearch.Utilities;
using Fortis.Model;
using Fortis.Providers;

namespace Fortis.Search
{
	public class ItemSearchFactory : IItemSearchFactory
	{
		protected readonly ISearchProvider _searchProvider;
		protected readonly ISpawnProvider _spawnProvider;

		public ItemSearchFactory(ISearchProvider searchProvider, ISpawnProvider spawnProvider)
		{
			_searchProvider = searchProvider;
			_spawnProvider = spawnProvider;
		}

		public IQueryable<T> Search<T>(IProviderSearchContext context, IExecutionContext executionContext = null) where T : IItemWrapper
		{
			IQueryable<T> results = _searchProvider.GetQueryable<T>(context, executionContext);

			if (results != null)
			{
				var typeOfT = typeof(T);

				if (_spawnProvider.TemplateMapProvider.InterfaceTemplateMap.ContainsKey(typeOfT))
				{
					var templateId = _spawnProvider.TemplateMapProvider.InterfaceTemplateMap[typeOfT];

					return results.Where(item => item.TemplateIds.Contains(templateId) && item.LanguageName == Sitecore.Context.Language.Name && item.IsLatestVersion);
				}
				else
				{
					return results;
				}
			}

			return results;
		}
	}
}
