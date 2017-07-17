using Fortis.Context;
using Sitecore.Data.Items;
using System;

namespace Fortis.Items.Context
{
	public class ContextItemGetter : IContextItemGetter
	{
		protected readonly ISitecoreContextDatabase Context;
		protected readonly ISitecoreItemGetter SitecoreItemGetter;
		protected readonly IItemFactory ItemFactory;

		public ContextItemGetter(
			ISitecoreContextDatabase context,
			ISitecoreItemGetter sitecoreItemGetter,
			IItemFactory itemFactory)
		{
			Context = context;
			SitecoreItemGetter = sitecoreItemGetter;
			ItemFactory = itemFactory;
		}

		public T GetItem<T>(Guid id)
		{
			var item = SitecoreItemGetter.GetItem(id, Context.Database);

			return CreateItem<T>(item);
		}

		public T GetItem<T>(Guid id, string language)
		{
			var item = SitecoreItemGetter.GetItem(id, language, Context.Database);

			return CreateItem<T>(item);
		}

		public T GetItem<T>(Guid id, string language, int version)
		{
			var item = SitecoreItemGetter.GetItem(id, language, version, Context.Database);

			return CreateItem<T>(item);
		}

		public T GetItem<T>(string path)
		{
			var item = SitecoreItemGetter.GetItem(path, Context.Database);

			return CreateItem<T>(item);
		}

		public T GetItem<T>(string path, string language)
		{
			var item = SitecoreItemGetter.GetItem(path, language, Context.Database);

			return CreateItem<T>(item);
		}

		public T GetItem<T>(string path, string language, int version)
		{
			var item = SitecoreItemGetter.GetItem(path, language, version, Context.Database);

			return CreateItem<T>(item);
		}

		public T CreateItem<T>(Item item)
		{
			return ItemFactory.Create<T>(item);
		}
	}
}
