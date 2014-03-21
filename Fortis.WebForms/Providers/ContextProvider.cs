namespace Fortis.WebForms.Providers
{
	using System;
	using Sitecore.Data.Items;
	using Fortis.Providers;
	using System.Collections.Generic;

	public class ContextProvider : IContextProvider
	{
		public Item RenderingContextItem
		{
			get { throw new NotSupportedException(); }
		}

		public Item PageContextItem
		{
			get { return Sitecore.Context.Item; }
		}

		public Item RenderingItem
		{
			get { throw new NotSupportedException("You must use the Rendering Parameters Factory to access the item on the rendering datasource"); }
		}

		public Dictionary<string, string> RenderingParameters
		{
			get { throw new NotSupportedException(); }
		}
	}
}
