using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace Fortis.Model
{
	public class ItemFactory : IItemFactory
	{
		public Guid GetTemplateID(Type type)
		{
			return Spawn.InterfaceTemplateMap.ContainsKey(type) 
				? Spawn.InterfaceTemplateMap[type] 
				: Guid.Empty;
		}

		public Type GetInterfaceType(Guid templateId)
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

		public T GetSiteHome<T>() where T : IItemWrapper
		{
			var item = GetItem(Sitecore.Context.Site.StartPath);
			var wrapper = Spawn.FromItem<T>(item);
			return (T)((wrapper is T) ? wrapper : null);
		}

		public T GetContextItem<T>() where T : IItemWrapper
		{
			var item = Sitecore.Context.Item;
			var wrapper = Spawn.FromItem<T>(item);
			return (T)((wrapper is T) ? wrapper : null);
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

		#region Create

		public T Create<T>(IItemWrapper parent, string itemName) where T : IItemWrapper
		{
			return Create<T>(parent.ItemID, itemName);
		}

		public T Create<T>(string parentPath, string itemName) where T : IItemWrapper
		{
			var database = Factory.GetDatabase("master");
			var parentItem = GetItem(parentPath, database);

			return Create<T>(parentItem, itemName);
		}

		public T Create<T>(Guid parentId, string itemName) where T : IItemWrapper
		{
			var database = Factory.GetDatabase("master");
			var parentItem = GetItem(parentId, database);

			return Create<T>(parentItem, itemName);
		}

		private T Create<T>(Item parentItem, string itemName) where T : IItemWrapper
		{
			object newItemObject = null;
			var type = typeof(T);

			if (Spawn.InterfaceTemplateMap.ContainsKey(type))
			{
				var templateId = Spawn.InterfaceTemplateMap[type];

				if (parentItem != null)
				{
					TemplateItem newItemTemplate = GetItem(templateId, parentItem.Database);
					var newItem = parentItem.Add(itemName, newItemTemplate);

					newItemObject = Spawn.FromItem<T>(newItem);
				}
			}

			return (T)newItemObject;
		}

		#endregion

		#region Select

		public T Select<T>(string path, string database = null) where T : IItemWrapper
		{
			return Select<T>(path, database == null ? Sitecore.Context.Database : Factory.GetDatabase(database));
		}

		public T Select<T>(Guid id, string database = null) where T : IItemWrapper
		{
			return SpawnFromItem<T>(SelectSingleItem(id, database == null ? Sitecore.Context.Database : Factory.GetDatabase(database)));
		}

		protected virtual T Select<T>(string path, Database database) where T : IItemWrapper
		{
			if (database != null)
			{
				string pathOrId = path;

				try
				{
					pathOrId = ID.Parse(path).ToString();
				}
				catch { }

				var item = SelectSingleItem(pathOrId, database);

				if (item != null)
				{
					return SpawnFromItem<T>(item);
				}
			}

			return default(T);
		}

		#endregion

		#region SelectAll

		public IEnumerable<T> SelectAll<T>(string path, string database = null) where T : IItemWrapper
		{
			return SelectAll<T>(database == null ? Sitecore.Context.Database : Factory.GetDatabase(database), path);
		}

		protected virtual IEnumerable<T> SelectAll<T>(Database database, string path) where T : IItemWrapper
		{
			if (database != null)
			{
				var items = database.SelectItems(path);
				return FilterWrapperTypes<T>(Spawn.FromItems(items));
			}

			return Enumerable.Empty<T>();
		}

		#endregion

		#region SelectChild

		public T SelectChild<T>(IItemWrapper item) where T : IItemWrapper
		{
			return SelectChild<T>(item.ItemID);
		}

		public T SelectChild<T>(string path) where T : IItemWrapper
		{
			var children = SelectChildren<T>(path);

			return children.FirstOrDefault();
		}

		public T SelectChild<T>(Guid id) where T : IItemWrapper
		{
			var children = SelectChildren<T>(id);

			return children.FirstOrDefault();
		}

		public T SelectChildRecursive<T>(string path) where T : IItemWrapper
		{
			var item = GetItem(path);

			return SelectChildrenRecursive<T>(item).FirstOrDefault();
		}

		public T SelectChildRecursive<T>(Guid id) where T : IItemWrapper
		{
			var item = GetItem(id);

			return SelectChildrenRecursive<T>(item).FirstOrDefault();
		}

		public IEnumerable<T> SelectChildren<T>(IItemWrapper item) where T : IItemWrapper
		{
			return SelectChildren<T>(item.ItemID);
		}

		public IEnumerable<T> SelectChildren<T>(string path) where T : IItemWrapper
		{
			return SelectChildren<T>(SelectSingleItem(path));
		}

		public IEnumerable<T> SelectChildren<T>(Guid id) where T : IItemWrapper
		{
			return SelectChildren<T>(SelectSingleItem(id));
		}

		public IEnumerable<T> SelectChildrenRecursive<T>(IItemWrapper wrapper) where T : IItemWrapper
		{
			return SelectChildrenRecursive<T>(((Item)wrapper.Original));
		}

		protected IEnumerable<T> SelectChildren<T>(Item item) where T : IItemWrapper
		{
			try
			{
				return FilterWrapperTypes<T>(Spawn.FromItems(item.Children.AsEnumerable()));
			}
			catch
			{
				return Enumerable.Empty<T>();
			}
		}

		protected IEnumerable<T> SelectChildrenRecursive<T>(Item item) where T : IItemWrapper
		{
			try
			{
				return FilterWrapperTypes<T>(Spawn.FromItems(item.Axes.GetDescendants()));
			}
			catch
			{
				return Enumerable.Empty<T>();
			}
		}

		#endregion

		public T SelectSibling<T>(IItemWrapper wrapper) where T : IItemWrapper
		{
			var parent = wrapper.Parent<IItemWrapper>();

			return parent != null 
				? SelectChild<T>(parent) 
				: default(T);
		}

		public IEnumerable<T> SelectSiblings<T>(IItemWrapper wrapper) where T : IItemWrapper
		{
			var parent = wrapper.Parent<IItemWrapper>();

			return parent != null 
				? SelectChildren<T>(parent)
				: Enumerable.Empty<T>();
		}

		public T GetRenderingDataSource<T>(System.Web.UI.Control control) where T : IItemWrapper
		{
			if (control.Parent is Sitecore.Web.UI.WebControls.Sublayout)
			{
				var dataSourcePath = ((Sitecore.Web.UI.WebControls.Sublayout)control.Parent).DataSource;
				if (dataSourcePath.Length > 0)
				{
					return Select<T>(dataSourcePath);
				}
			}

			return GetContextItem<T>();
		}

		#region private

		protected virtual Item GetItem(string path)
		{
			return GetItem(path, Sitecore.Context.Database);
		}

		protected virtual Item GetItem(Guid id)
		{
			return GetItem(id, Sitecore.Context.Database);
		}

		protected virtual Item GetItem(string path, Database database)
		{
			return database.GetItem(path);
		}

		protected virtual Item GetItem(Guid id, Database database)
		{
			return database.GetItem(new ID(id));
		}

		protected virtual Item SelectSingleItem(string path)
		{
			return SelectSingleItem(path, Sitecore.Context.Database);
		}

		protected virtual Item SelectSingleItem(Guid id)
		{
			return SelectSingleItem(id, Sitecore.Context.Database);
		}

		protected virtual Item SelectSingleItem(string path, Database database)
		{
			return database.SelectSingleItem(path);
		}

		protected virtual Item SelectSingleItem(Guid id, Database database)
		{
			return database.GetItem(new ID(id));
		}

		private static IEnumerable<T> FilterWrapperTypes<T>(IEnumerable<IItemWrapper> wrappers)
		{
			return wrappers.Where(wrapper => wrapper != null).OfType<T>();
		}
		
		protected virtual T SpawnFromItem<T>(Item item) where T : IItemWrapper
		{
			object wrapper = null;
			try
			{
				wrapper = Spawn.FromItem<T>(item);
			}
			catch { }
			return (T)((wrapper is T) ? wrapper : null);
		}

		#endregion
	}
}
