namespace Fortis.Model
{
	using Fortis.Providers;
	using Sitecore.Configuration;
	using Sitecore.Data;
	using Sitecore.Data.Items;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class ItemFactory : IItemFactory
	{
		private readonly IContextProvider _contextProvider;

		public ItemFactory(IContextProvider contextProvider)
		{
			_contextProvider = contextProvider;
		}

		public string GetTemplateID(Type type)
		{
			if (Spawn.InterfaceTemplateMap.ContainsKey(type))
			{
				return Spawn.InterfaceTemplateMap[type];
			}
			return string.Empty;
		}

		public Type GetInterfaceType(string templateId)
		{
			if (Spawn.TemplateMap.ContainsKey(templateId))
			{
				return Spawn.TemplateMap[templateId];
			}

			return typeof(IItemWrapper);
		}

		public void Publish(IEnumerable<IItemWrapper> wrappers)
		{
			Publish(wrappers, false);
		}

		public void Publish(IEnumerable<IItemWrapper> wrappers, bool children)
		{
			foreach (var wrapper in wrappers)
			{
				wrapper.Publish(children);
			}
		}

		protected virtual Item GetItem(string pathOrId)
		{
			return GetItem(pathOrId, Sitecore.Context.Database);
		}

		protected virtual Item GetItem(string pathOrId, Database database)
		{
			return database.GetItem(pathOrId);
		}

		protected virtual Item SelectSingleItem(string pathOrId)
		{
			return SelectSingleItem(pathOrId, Sitecore.Context.Database);
		}

		protected virtual Item SelectSingleItem(string pathOrId, Database database)
		{
			return database.SelectSingleItem(pathOrId);
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

		public T Create<T>(IItemWrapper parent, string itemName) where T : IItemWrapper
		{
			return Create<T>(parent.ItemLocation, itemName);
		}

		public T Create<T>(string parentPathOrId, string itemName) where T : IItemWrapper
		{
			object newItemObject = null;
			var type = typeof(T);

			if (Spawn.InterfaceTemplateMap.Keys.Contains(type))
			{
				var templateId = Spawn.InterfaceTemplateMap[type];
				var database = Sitecore.Configuration.Factory.GetDatabase("master");
				var parent = GetItem(parentPathOrId, database);

				if (parent != null)
				{
					TemplateItem newItemTemplate = GetItem(templateId, database);
					var newItem = parent.Add(itemName, newItemTemplate);

					newItemObject = Spawn.FromItem<T>(newItem);
				}
			}

			return (T)newItemObject;
		}

		public T GetSiteHome<T>() where T : IItemWrapper
		{
			var item = GetItem(Sitecore.Context.Site.StartPath);
			var wrapper = Spawn.FromItem<T>(item);
			return (T)((wrapper is T) ? wrapper : null);
		}

		public T GetContextItem<T>() where T : IItemWrapper
		{
			var item = _contextProvider.PageContextItem;
			var wrapper = Spawn.FromItem<T>(item);

			return (T)((wrapper is T) ? wrapper : null);
		}

		public IRenderingModel<TPageItem, TRenderingItem> GetRenderingContextItems<TPageItem, TRenderingItem>()
			where TPageItem : IItemWrapper
			where TRenderingItem : IItemWrapper
		{
			var pageWrapper = Spawn.FromItem<TPageItem>(_contextProvider.PageContextItem);
			var renderingWrapper = Spawn.FromItem<TRenderingItem>(_contextProvider.RenderingContextItem);
			var validPageWrapper = (TPageItem)(pageWrapper is TPageItem ? pageWrapper : null);
			var validRenderingWrapper = (TRenderingItem)(renderingWrapper is TRenderingItem ? renderingWrapper : null);

			return new RenderingModel<TPageItem, TRenderingItem>(validPageWrapper, validRenderingWrapper);
		}

		public IRenderingModel<TPageItem, TRenderingItem, TRenderingParametersItem> GetRenderingContextItems<TPageItem, TRenderingItem, TRenderingParametersItem>()
			where TPageItem : IItemWrapper
			where TRenderingItem : IItemWrapper
			where TRenderingParametersItem : IRenderingParameterWrapper
		{
			var pageWrapper = Spawn.FromItem<TPageItem>(_contextProvider.PageContextItem);
			var renderingWrapper = Spawn.FromItem<TRenderingItem>(_contextProvider.RenderingContextItem);
			var renderingParametersWrapper = Spawn.FromRenderingParameters<TRenderingParametersItem>(_contextProvider.RenderingItem, _contextProvider.RenderingParameters);
			var validPageWrapper = (TPageItem)(pageWrapper is TPageItem ? pageWrapper : null);
			var validRenderingWrapper = (TRenderingItem)(renderingWrapper is TRenderingItem ? renderingWrapper : null);
			var validRenderingParametersWrapper = (TRenderingParametersItem)(renderingParametersWrapper is TRenderingParametersItem ? renderingParametersWrapper : null);

			return new RenderingModel<TPageItem, TRenderingItem, TRenderingParametersItem>(validPageWrapper, validRenderingWrapper, validRenderingParametersWrapper);
		}

		public T GetContextItemsItem<T>(string key) where T : IItemWrapper
		{
			IItemWrapper wrapper = null;

			if (Sitecore.Context.Items.Contains(key) && Sitecore.Context.Items[key].GetType() == typeof(Item))
			{
				wrapper = Spawn.FromItem<T>((Item)Sitecore.Context.Items[key]);
			}

			return (T)((wrapper is T) ? wrapper : null);
		}

		public T Select<T>(string path) where T : IItemWrapper
		{
			return Select<T>(path, Sitecore.Context.Database);
		}

		public T Select<T>(string path, string database) where T : IItemWrapper
		{
			return Select<T>(path, Factory.GetDatabase(database));
		}

		protected virtual T Select<T>(string path, Database database) where T : IItemWrapper
		{
			if (database != null)
			{
				string pathOrId = path;

				try
				{
					pathOrId = Sitecore.Data.ID.Parse(path).ToString();
				}
				catch { }

				object wrapper = null;
				try
				{
					var item = SelectSingleItem(pathOrId, database);
					wrapper = Spawn.FromItem<T>(item);
				}
				catch { }
				return (T)((wrapper is T) ? wrapper : null);
			}
			else
			{
				return default(T);
			}
		}

		public IEnumerable<T> SelectAll<T>(string path) where T : IItemWrapper
		{
			return SelectAll<T>(Sitecore.Context.Database, path);
		}

		public IEnumerable<T> SelectAll<T>(string path, string database) where T : IItemWrapper
		{
			return SelectAll<T>(Factory.GetDatabase(database), path);
		}

		protected virtual IEnumerable<T> SelectAll<T>(Database database, string path) where T : IItemWrapper
		{
			if (database != null)
			{
				var items = database.SelectItems(path);
				return FilterWrapperTypes<T>(Spawn.FromItems(items));
			}
			else
			{
				return Enumerable.Empty<T>();
			}
		}

		public T SelectChild<T>(IItemWrapper item) where T : IItemWrapper
		{
			return SelectChild<T>(item.ItemLocation);
		}

		public T SelectChild<T>(string path) where T : IItemWrapper
		{
			var children = SelectChildren<T>(path);
			IItemWrapper firstChild = null;

			if (children.Count() > 0)
			{
				firstChild = children.First();
			}

			return (T)((firstChild is T) ? firstChild : null);
		}

		public T SelectChildRecursive<T>(string path) where T : IItemWrapper
		{
			var children = SelectChildren<T>(path);

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

		public IEnumerable<T> SelectChildren<T>(IItemWrapper item) where T : IItemWrapper
		{
			return SelectChildren<T>(item.ItemLocation);
		}

		public IEnumerable<T> SelectChildren<T>(string path) where T : IItemWrapper
		{
			try
			{
				var item = SelectSingleItem(path);

				return FilterWrapperTypes<T>(Spawn.FromItems(item.Children.AsEnumerable()));
			}
			catch
			{
				return Enumerable.Empty<T>();
			}
		}

		public IEnumerable<T> SelectChildrenRecursive<T>(IItemWrapper wrapper) where T : IItemWrapper
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

		public T SelectSibling<T>(IItemWrapper wrapper) where T : IItemWrapper
		{
			var parent = wrapper.Parent<IItemWrapper>();

			if (parent != null)
			{
				return SelectChild<T>(parent);
			}

			return default(T);
		}

		public IEnumerable<T> SelectSiblings<T>(IItemWrapper wrapper) where T : IItemWrapper
		{
			var parent = wrapper.Parent<IItemWrapper>();

			if (parent != null)
			{
				return SelectChildren<T>(parent);
			}
			else
			{
				return Enumerable.Empty<T>();
			}
		}

		public T GetRenderingDataSource<T>(System.Web.UI.Control control) where T : IItemWrapper
		{
			if (control.Parent is Sitecore.Web.UI.WebControls.Sublayout)
			{
				string dataSourcePath = ((Sitecore.Web.UI.WebControls.Sublayout)control.Parent).DataSource;
				if (dataSourcePath.Length > 0)
				{
					return this.Select<T>(dataSourcePath);
				}
			}

			return this.GetContextItem<T>();
		}
	}
}
