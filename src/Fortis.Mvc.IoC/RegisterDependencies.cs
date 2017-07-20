using Sitecore.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Fortis.Context;

namespace Fortis.Mvc.IoC
{
	public class RegisterDependencies : IServicesConfigurator
	{
		public void Configure(IServiceCollection serviceCollection)
		{
			// Context
			serviceCollection.AddSingleton<ISitecoreContextItem, Context.SitecoreContextItem>();
		}
	}
}
