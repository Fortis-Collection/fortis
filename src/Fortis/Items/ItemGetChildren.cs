using System;
using System.Collections.Generic;

namespace Fortis.Items
{
	public class ItemGetChildren : IItemGetChildren
	{
		protected readonly IItemGetter ItemGetter;

		public ItemGetChildren(
			IItemGetter itemGetter)
		{
			ItemGetter = itemGetter;
		}

		public IEnumerable<T> GetChildren<T>(Guid id, string database)
		{
			var item = ItemGetter.GetItem<IItem>(id, database);

			return item.GetChildren<T>();
		}

		public IEnumerable<T> GetChildren<T>(string path, string database)
		{
			var item = ItemGetter.GetItem<IItem>(path, database);

			return item.GetChildren<T>();
		}
	}
}
