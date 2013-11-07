using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortis.Providers
{
	public interface IItemContextProvider
	{
		Item RenderingItem { get; }
		Item PageItem { get; }
	}
}