using System;
using System.Collections.Generic;

namespace Fortis.Items.Context
{
	public class ContextItemGetChildren : IContextItemGetChildren
	{
		protected readonly IContextItemGetter ContextItemGetter;

		public ContextItemGetChildren(
			IContextItemGetter contextItemGetter)
		{
			ContextItemGetter = contextItemGetter;
		}

		public IEnumerable<T> GetChildren<T>(Guid id)
		{
			var item = ContextItemGetter.GetItem<IItem>(id);

			return item.GetChildren<T>();
		}

		public IEnumerable<T> GetChildren<T>(string path)
		{
			var item = ContextItemGetter.GetItem<IItem>(path);

			return item.GetChildren<T>();
		}
	}
}
