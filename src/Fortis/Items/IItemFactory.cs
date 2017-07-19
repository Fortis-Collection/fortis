using Sitecore.Data.Items;
using System.Collections.Generic;

namespace Fortis.Items
{
	public interface IItemFactory
	{
		T Create<T>(Item item);
		IEnumerable<T> Create<T>(IEnumerable<Item> items);
	}
}
