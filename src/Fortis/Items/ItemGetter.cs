using Sitecore.Data.Items;
using System;

namespace Fortis.Items
{
	public class ItemGetter : IItemGetter
	{
		protected readonly ISitecoreItemGetter SitecoreItemGetter;
		protected readonly IItemFactory ItemFactory;

		public ItemGetter(
			ISitecoreItemGetter sitecoreItemGetter,
			IItemFactory itemFactory)
		{
			SitecoreItemGetter = sitecoreItemGetter;
			ItemFactory = itemFactory;
		}

		public T GetItem<T>(Guid id, string database)
		{
			var item = SitecoreItemGetter.GetItem(id, database);

			return CreateItem<T>(item);
		}

		public T GetItem<T>(Guid id, string language, string database)
		{
			var item = SitecoreItemGetter.GetItem(id, language, database);

			return CreateItem<T>(item);
		}

		public T GetItem<T>(Guid id, string language, int version, string database)
		{
			var item = SitecoreItemGetter.GetItem(id, language, version, database);

			return CreateItem<T>(item);
		}

		public T GetItem<T>(string path, string database)
		{
			var item = SitecoreItemGetter.GetItem(path, database);

			return CreateItem<T>(item);
		}

		public T GetItem<T>(string path, string language, string database)
		{
			var item = SitecoreItemGetter.GetItem(path, language, database);

			return CreateItem<T>(item);
		}

		public T GetItem<T>(string path, string language, int version, string database)
		{
			var item = SitecoreItemGetter.GetItem(path, language, version, database);

			return CreateItem<T>(item);
		}

		public T CreateItem<T>(Item item)
		{
			return ItemFactory.Create<T>(item);
		}
	}
}
