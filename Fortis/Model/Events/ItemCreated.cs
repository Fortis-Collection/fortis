using Sitecore.Data.Events;
using Sitecore.Diagnostics;
using Sitecore.Events;
using System;

namespace Fortis.Model.Events
{
	public abstract class ItemCreated<T> where T : IItemWrapper
	{
		public void OnItemCreated(object sender, EventArgs args)
		{
			var eventArgs = args as SitecoreEventArgs;
			Assert.IsNotNull(eventArgs, "eventArgs");
			var itemCreatedEventArgs = eventArgs.Parameters[0] as ItemCreatedEventArgs;
			Assert.IsNotNull(itemCreatedEventArgs, "itemCreatedEventArgs");
			var scItem = itemCreatedEventArgs.Item;
			Assert.IsNotNull(scItem, "scItem");

			var item = Spawn.FromItem<T>(scItem);

			OnItemCreated((T)((item is T) ? item : null), sender, (object)args);
		}

		protected abstract void OnItemCreated(T item, object sender, object args);
	}
}
