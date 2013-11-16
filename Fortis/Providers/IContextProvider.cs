using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortis.Providers
{
	public interface IContextProvider
	{
		Item RenderingContextItem { get; }
		Item PageContextItem { get; }
		Item RenderingItem { get; }
		Dictionary<string, string> RenderingParameters { get; }
	}
}