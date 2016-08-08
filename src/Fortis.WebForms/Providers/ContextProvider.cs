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
			get { return null; }
		}

		public Item PageContextItem
		{
			get { return Sitecore.Context.Item; }
		}

		public Item RenderingItem
		{
			get { return null; }
		}

		public Dictionary<string, string> RenderingParameters
		{
			get { return new Dictionary<string,string>(); }
		}
	}
}
