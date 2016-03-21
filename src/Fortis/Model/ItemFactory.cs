using Sitecore.Globalization;
using Sitecore.Web.UI.WebControls;
using Fortis.Providers;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fortis.Model
{
	public partial class ItemFactory : IItemFactory
	{
		protected readonly IContextProvider ContextProvider;
		protected readonly ISpawnProvider SpawnProvider;

		public ItemFactory(IContextProvider contextProvider, ISpawnProvider spawnProvider)
		{
			ContextProvider = contextProvider;
			SpawnProvider = spawnProvider;
		}

		public virtual Guid GetTemplateID(Type type)
		{
			if (SpawnProvider.TemplateMapProvider.InterfaceTemplateMap.ContainsKey(type))
			{
				return SpawnProvider.TemplateMapProvider.InterfaceTemplateMap[type];
			}
			return Guid.Empty;
		}

		public virtual Type GetInterfaceType(Guid templateId)
		{
			if (SpawnProvider.TemplateMapProvider.TemplateMap.ContainsKey(templateId))
			{
				return SpawnProvider.TemplateMapProvider.TemplateMap[templateId];
			}

			return typeof(IItemWrapper);
		}

		public virtual void Publish(IEnumerable<IItemWrapper> wrappers)
		{
			Publish(wrappers, false);
		}

		public virtual void Publish(IEnumerable<IItemWrapper> wrappers, bool children)
		{
			foreach (var wrapper in wrappers)
			{
				wrapper.Publish(children);
			}
		}

		protected virtual Item GetItem(string path, Database database = null)
		{
			return (database ?? Sitecore.Context.Database).GetItem(path);
		}

		protected virtual Item GetItem(Guid id, Database database = null)
		{
			return (database ?? Sitecore.Context.Database).GetItem(new ID(id));
		}

		protected virtual Item GetItem(Guid id, Language language = null, Database database = null)
		{
			if (language != null)
			{
				return (database ?? Sitecore.Context.Database).GetItem(new ID(id), language);
			}
			return (database ?? Sitecore.Context.Database).GetItem(new ID(id));
		}

		protected virtual Item SelectSingleItem(string path)
		{
			return SelectSingleItem(path, Sitecore.Context.Database);
		}

		protected virtual Item SelectSingleItem(string path, Database database)
		{
			return database.SelectSingleItem(path);
		}

		protected virtual T SpawnFromItem<T>(Item item) where T : IItemWrapper
		{
			object wrapper = null;
			try
			{
				wrapper = SpawnProvider.FromItem<T>(item);
			}
			catch { }
			return (T)((wrapper is T) ? wrapper : null);
		}

		private IEnumerable<T> FilterWrapperTypes<T>(IEnumerable<IItemWrapper> wrappers)
		{
			foreach (IItemWrapper wrapper in wrappers)
			{
				if (wrapper is T)
				{
					yield return (T)wrapper;
				}
			}
		}

		public virtual T Create<T>(IItemWrapper parent, string itemName) where T : IItemWrapper
		{
			return Create<T>(parent.ItemID, itemName);
		}

		public virtual T Create<T>(string parentPath, string itemName) where T : IItemWrapper
		{
			var database = Factory.GetDatabase("master");
			var parentItem = GetItem(parentPath, database);

			return Create<T>(parentItem, itemName);
		}

		public virtual T Create<T>(Guid parentId, string itemName) where T : IItemWrapper
		{
			var database = Factory.GetDatabase("master");
			var parentItem = GetItem(parentId, database);

			return Create<T>(parentItem, itemName);
		}

		private T Create<T>(Item parentItem, string itemName) where T : IItemWrapper
		{
			object newItemObject = null;
			var type = typeof(T);

			if (SpawnProvider.TemplateMapProvider.InterfaceTemplateMap.ContainsKey(type))
			{
				var templateId = SpawnProvider.TemplateMapProvider.InterfaceTemplateMap[type];

				if (parentItem != null)
				{
					TemplateItem newItemTemplate = GetItem(templateId, parentItem.Database);
					var newItem = parentItem.Add(itemName, newItemTemplate);

					newItemObject = SpawnProvider.FromItem<T>(newItem);
				}
			}

			return (T)newItemObject;
		}

		public virtual T GetSiteHome<T>() where T : IItemWrapper
		{
			var item = GetItem(Sitecore.Context.Site.StartPath);
			var wrapper = SpawnProvider.FromItem<T>(item);
			return (T)((wrapper is T) ? wrapper : null);
		}

		public virtual T GetSiteRoot<T>() where T : IItemWrapper
		{
			var item = GetItem(Sitecore.Context.Site.RootPath);
			// Make sure we have value item at this point
			if (item == null)
			{
				return default(T);
			}

			var wrapper = SpawnProvider.FromItem<T>(item);
			return (T) ((wrapper is T) ? wrapper : null);
		}

		public virtual T GetContextItem<T>() where T : IItemWrapper
		{
			var item = ContextProvider.PageContextItem;
			var wrapper = SpawnProvider.FromItem<T>(item);

			return (T)((wrapper is T) ? wrapper : null);
		}

		public virtual IRenderingModel<TPageItem, TRenderingItem> GetRenderingContextItems<TPageItem, TRenderingItem>(IItemFactory factory = null)
			where TPageItem : IItemWrapper
			where TRenderingItem : IItemWrapper
		{
			var pageWrapper = SpawnProvider.FromItem<TPageItem>(ContextProvider.PageContextItem);
			var renderingWrapper = SpawnProvider.FromItem<TRenderingItem>(ContextProvider.RenderingContextItem);
			var validPageWrapper = (TPageItem)(pageWrapper is TPageItem ? pageWrapper : null);
			var validRenderingWrapper = (TRenderingItem)(renderingWrapper is TRenderingItem ? renderingWrapper : null);

			return new RenderingModel<TPageItem, TRenderingItem>(validPageWrapper, validRenderingWrapper, factory);
		}

		public virtual IRenderingModel<TPageItem, TRenderingItem, TRenderingParametersItem> GetRenderingContextItems<TPageItem, TRenderingItem, TRenderingParametersItem>(IItemFactory factory = null)
			where TPageItem : IItemWrapper
			where TRenderingItem : IItemWrapper
			where TRenderingParametersItem : IRenderingParameterWrapper
		{
			var pageWrapper = SpawnProvider.FromItem<TPageItem>(ContextProvider.PageContextItem);
			var renderingWrapper = SpawnProvider.FromItem<TRenderingItem>(ContextProvider.RenderingContextItem);
			var renderingParametersWrapper = SpawnProvider.FromRenderingParameters<TRenderingParametersItem>(ContextProvider.RenderingItem, ContextProvider.RenderingParameters);
			var validPageWrapper = (TPageItem)(pageWrapper is TPageItem ? pageWrapper : null);
			var validRenderingWrapper = (TRenderingItem)(renderingWrapper is TRenderingItem ? renderingWrapper : null);
			var validRenderingParametersWrapper = (TRenderingParametersItem)(renderingParametersWrapper is TRenderingParametersItem ? renderingParametersWrapper : null);

			return new RenderingModel<TPageItem, TRenderingItem, TRenderingParametersItem>(validPageWrapper, validRenderingWrapper, validRenderingParametersWrapper, factory);
		}

		public virtual T GetContextItemsItem<T>(string key) where T : IItemWrapper
		{
			IItemWrapper wrapper = null;

			if (Sitecore.Context.Items.Contains(key) && Sitecore.Context.Items[key].GetType() == typeof(Item))
			{
				wrapper = SpawnProvider.FromItem<T>((Item)Sitecore.Context.Items[key]);
			}

			return (T)((wrapper is T) ? wrapper : null);
		}


		public virtual T Select<T>(string path) where T : IItemWrapper
		{
			return Select<T>(path, Sitecore.Context.Database);
		}

		public virtual T Select<T>(Guid id) where T : IItemWrapper
		{
			return SpawnFromItem<T>(GetItem(id, null));
		}

		public T Select<T>(string path, string database) where T : IItemWrapper
		{
			return Select<T>(path, Factory.GetDatabase(database));
		}

		public virtual T Select<T>(Guid id, string database) where T : IItemWrapper
		{
			return SpawnFromItem<T>(GetItem(id, Factory.GetDatabase(database)));
		}

		public virtual T Select<T>(Guid id, string database, string language) where T : IItemWrapper
		{
			Language lang = Sitecore.Context.Language;
			if (!string.IsNullOrWhiteSpace(language))
			{
				lang = Language.Parse(language);
			}

			if (string.IsNullOrWhiteSpace(database))
			{
				return SpawnFromItem<T>(GetItem(id, lang));
			}

			return SpawnFromItem<T>(GetItem(id, lang, Factory.GetDatabase(database)));
		}

		protected virtual T Select<T>(string path, Database database) where T : IItemWrapper
		{
			if (database != null)
			{
				var pathOrId = path;
				try
				{
					pathOrId = ID.Parse(path).ToString();
				}
				catch { }

				object wrapper = null;
				try
				{
					var item = SelectSingleItem(pathOrId, database);
					wrapper = SpawnProvider.FromItem<T>(item);
				}
				catch { }
				return (T)((wrapper is T) ? wrapper : null);
			}

			return default(T);
		}

		public virtual IEnumerable<T> SelectAll<T>(string path) where T : IItemWrapper
		{
			return SelectAll<T>(Sitecore.Context.Database, path);
		}

		public virtual IEnumerable<T> SelectAll<T>(string path, string database) where T : IItemWrapper
		{
			return SelectAll<T>(Factory.GetDatabase(database), path);
		}

		protected virtual IEnumerable<T> SelectAll<T>(Database database, string path) where T : IItemWrapper
		{
			if (database != null)
			{
				var items = database.SelectItems(path);
				return FilterWrapperTypes<T>(SpawnProvider.FromItems(items));
			}

			return Enumerable.Empty<T>();
		}

		public T SelectChild<T>(IItemWrapper item) where T : IItemWrapper
		{
			return SelectChild<T>(item.ItemID);
		}

		public T SelectChild<T>(string path) where T : IItemWrapper
		{
			var children = SelectChildren<T>(path);

			return children.FirstOrDefault();
		}

		public virtual T SelectChild<T>(Guid id) where T : IItemWrapper
		{
			var children = SelectChildren<T>(id);

			return children.FirstOrDefault();
		}

		public virtual T SelectChildRecursive<T>(string path) where T : IItemWrapper
		{
			return SelectChildRecursive<T>(SelectChildren<T>(path));
		}

		public virtual T SelectChildRecursive<T>(Guid id) where T : IItemWrapper
		{
			return SelectChildRecursive<T>(SelectChildren<T>(id));
		}

		protected T SelectChildRecursive<T>(IEnumerable<T> children) where T : IItemWrapper
		{
			foreach (IItemWrapper item in children)
			{
				if (item is T)
				{
					return (T)item;
				}

				if (item.HasChildren)
				{
					var innerChildren = SelectChildrenRecursive<T>(item);

					foreach (IItemWrapper innerChild in innerChildren)
					{
						return (T)innerChild;
					}
				}
			}

			return default(T);
		}

		public virtual IEnumerable<T> SelectChildren<T>(IItemWrapper item) where T : IItemWrapper
		{
			return SelectChildren<T>(item.ItemID);
		}

		public virtual IEnumerable<T> SelectChildren<T>(string path) where T : IItemWrapper
		{
			return SelectChildren<T>(SelectSingleItem(path));
		}

		public virtual IEnumerable<T> SelectChildren<T>(Guid id) where T : IItemWrapper
		{
			return SelectChildren<T>(GetItem(id, null));
		}

		protected IEnumerable<T> SelectChildren<T>(Item item) where T : IItemWrapper
		{
			try
			{
				return FilterWrapperTypes<T>(SpawnProvider.FromItems(item.Children.AsEnumerable()));
			}
			catch
			{
				return Enumerable.Empty<T>();
			}
		}

		public virtual IEnumerable<T> SelectChildrenRecursive<T>(IItemWrapper wrapper) where T : IItemWrapper
		{
			var children = SelectChildren<IItemWrapper>(wrapper);

			foreach (IItemWrapper item in children)
			{
				if (item is T)
				{
					yield return (T)item;
				}

				if (item.HasChildren)
				{
					var innerChildren = SelectChildrenRecursive<T>(item);

					foreach (IItemWrapper innerChild in innerChildren)
					{
						yield return (T)innerChild;
					}
				}
			}
		}

		public virtual T SelectSibling<T>(IItemWrapper wrapper) where T : IItemWrapper
		{
			var parent = wrapper.Parent<IItemWrapper>();

			if (parent != null)
			{
				return SelectChild<T>(parent);
			}

			return default(T);
		}

		public virtual IEnumerable<T> SelectSiblings<T>(IItemWrapper wrapper) where T : IItemWrapper
		{
			var parent = wrapper.Parent<IItemWrapper>();

			if (parent != null)
			{
				return SelectChildren<T>(parent);
			}
			return Enumerable.Empty<T>();
		}

		public virtual T GetRenderingDataSource<T>(System.Web.UI.Control control) where T : IItemWrapper
		{
			var parent = control.Parent as Sublayout;
			if (parent != null)
			{
				var dataSourcePath = parent.DataSource;
				if (dataSourcePath.Length > 0)
				{
					if (dataSourcePath.StartsWith("query:"))
					{
						var item = Sitecore.Context.Database.SelectSingleItem(dataSourcePath);
						if (item != null)
						{
							return SpawnFromItem<T>(item);
						}
					}
					return Select<T>(dataSourcePath);
				}
			}

			return GetContextItem<T>();
		}
	}
}