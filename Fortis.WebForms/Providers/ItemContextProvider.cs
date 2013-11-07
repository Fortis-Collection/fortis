namespace Fortis.WebForms.Providers
{
	using System;
	using Sitecore.Data.Items;
	using Fortis.Providers;

	public class ItemContextProvider : IItemContextProvider
	{
		public Item RenderingItem
		{
			get { throw new NotImplementedException("Fortis - Web Forms: Rendering Context Item is not currently implemented"); }
		}

		public Item PageItem
		{
			get { return Sitecore.Context.Item; }
		}
	}
}
