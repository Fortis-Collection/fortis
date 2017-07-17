using System.Collections.Generic;
using Sitecore.Data.Items;

namespace Fortis.Items
{
	public interface IItemsFactory
	{
		IEnumerable<T> Create<T>(IEnumerable<Item> items);
	}
}