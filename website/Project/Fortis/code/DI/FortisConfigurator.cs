using Fortis.Model;
using Fortis.Mvc.Providers;
using Fortis.Providers;
using Fortis.Search;
using Fortis.Website.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace Fortis.Website.DI
{
    public class FortisConfigurator : IServicesConfigurator
    {
        public void Configure(Microsoft.Extensions.DependencyInjection.IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ContentController>();

            serviceCollection.AddScoped<IItemFactory, ItemFactory>();
            serviceCollection.AddScoped<IItemSearchFactory, ItemSearchFactory>();
            serviceCollection.AddScoped<IContextProvider, ContextProvider>();
            serviceCollection.AddSingleton<IModelAssemblyProvider, ModelAssemblyProvider>();
            serviceCollection.AddSingleton<ISpawnProvider, SpawnProvider>();
            serviceCollection.AddSingleton<ITemplateMapProvider, TemplateMapProvider>();
            serviceCollection.AddScoped<ISearchResultsAdapter, SearchResultsAdapter>();
        }
    }
}