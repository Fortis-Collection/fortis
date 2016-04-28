namespace Fortis.Proxy.Model.Providers
{
	using System;

	using Fortis.Model;
	using Fortis.Providers;

	using Sitecore.Data.Items;

	public class DynamicSpawnProvider : SpawnProvider, ISpawnProvider
	{
		public DynamicSpawnProvider(ITemplateMapProvider templateMappingProvider) : base(templateMappingProvider)
		{
		}

		public override IItemWrapper FromItem(Item item, Type template)
		{
			return base.FromItem(item, template);
		}
	}
}
