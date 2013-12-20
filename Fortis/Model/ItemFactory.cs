namespace Fortis.Model
{
	using Fortis.Providers;
	using Fortis.Search;
	using Sitecore.Configuration;
	using Sitecore.ContentSearch;
	using Sitecore.ContentSearch.Diagnostics;
	using Sitecore.ContentSearch.Linq.Common;
	using Sitecore.ContentSearch.LuceneProvider;
	using Sitecore.ContentSearch.SearchTypes;
	using Sitecore.ContentSearch.SolrProvider;
	using Sitecore.ContentSearch.Utilities;
	using Sitecore.Data;
	using Sitecore.Data.Items;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;

	public partial class ItemFactory : IItemFactory
	{
		private readonly IContextProvider _contextProvider;

		public ItemFactory(IContextProvider contextProvider)
		{
			_contextProvider = contextProvider;
		}

		public Guid GetTemplateID(Type type)
		{
			if (Spawn.InterfaceTemplateMap.ContainsKey(type))
			{
				return Spawn.InterfaceTemplateMap[type];
			}
			return Guid.Empty;
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

		protected virtual Item GetItem(string path, Database database = null)
		{
			return (database ?? Sitecore.Context.Database).GetItem(path);
		}

		protected virtual Item GetItem(Guid id, Database database = null)
		{
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
				wrapper = Spawn.FromItem<T>(item);
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

		public IRenderingModel<TPageItem, TRenderingItem> GetRenderingContextItems<TPageItem, TRenderingItem>(IItemFactory factory = null)
			where TPageItem : IItemWrapper
			where TRenderingItem : IItemWrapper
		{
			var pageWrapper = Spawn.FromItem<TPageItem>(_contextProvider.PageContextItem);
			var renderingWrapper = Spawn.FromItem<TRenderingItem>(_contextProvider.RenderingContextItem);
			var validPageWrapper = (TPageItem)(pageWrapper is TPageItem ? pageWrapper : null);
			var validRenderingWrapper = (TRenderingItem)(renderingWrapper is TRenderingItem ? renderingWrapper : null);

			return new RenderingModel<TPageItem, TRenderingItem>(validPageWrapper, validRenderingWrapper, factory);
		}

		public IRenderingModel<TPageItem, TRenderingItem, TRenderingParametersItem> GetRenderingContextItems<TPageItem, TRenderingItem, TRenderingParametersItem>(IItemFactory factory = null)
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

			return new RenderingModel<TPageItem, TRenderingItem, TRenderingParametersItem>(validPageWrapper, validRenderingWrapper, validRenderingParametersWrapper, factory);
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

		public T Select<T>(Guid id) where T : IItemWrapper
		{
			return SpawnFromItem<T>(GetItem(id));
		}

		public T Select<T>(string path, string database) where T : IItemWrapper
		{
			return Select<T>(path, Factory.GetDatabase(database));
		}

		public T Select<T>(Guid id, string database) where T : IItemWrapper
		{
			return SpawnFromItem<T>(GetItem(id, Factory.GetDatabase(database)));
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
			return SelectChildRecursive<T>(SelectChildren<T>(path));
		}

		public T SelectChildRecursive<T>(Guid id) where T : IItemWrapper
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
			return SelectChildren<T>(GetItem(id));
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

		public IQueryable<T> Search<T>(IProviderSearchContext context, IExecutionContext executionContext = null) where T : IItemWrapper
		{
			IQueryable<T> results = null;

			#region Lucene

			var luceneContext = context as LuceneSearchContext;

			if (luceneContext != null)
			{
				results = GetLuceneQueryable<T>(luceneContext, executionContext);
			}

			#endregion

			#region Solr

			var solrContext = context as SolrSearchContext;

			if (solrContext != null)
			{
				results = GetSolrQueryable<T>(solrContext, executionContext);
			}

			#endregion

			if (solrContext == null && luceneContext == null)
			{
				throw new Exception("Fortis: Unsupported search context");
			}

			if (results != null)
			{
				var typeOfT = typeof(T);

				if (Spawn.InterfaceTemplateMap.ContainsKey(typeOfT))
				{
					var templateId = Spawn.InterfaceTemplateMap[typeOfT];

					return results.Where(item => item.TemplateId == templateId);
				}
				else
				{
					return results;
				}
			}

			return results;
		}

		public IQueryable<TResult> GetLuceneQueryable<TResult>(LuceneSearchContext context, IExecutionContext executionContext) where TResult : IItemWrapper
		{
			// once the hacks in the Hacks namespace are fixed (around update 2, I hear), the commented line below can be used instead of BugFixIndex
			// in fact once Update 3? is released, this class may become largely irrelevant as interface support is coming natively
			//var linqToLuceneIndex = (executionContext == null) ? new LinqToLuceneIndex<TResult>(context) : new LinqToLuceneIndex<TResult>(context, executionContext);
			var linqToLuceneIndex = (executionContext == null)
										? new CustomLinqToLuceneIndex<TResult>(context)
										: new CustomLinqToLuceneIndex<TResult>(context, executionContext);

			if (ContentSearchConfigurationSettings.EnableSearchDebug)
			{
				((IHasTraceWriter)linqToLuceneIndex).TraceWriter = new LoggingTraceWriter(SearchLog.Log);
			}

			return linqToLuceneIndex.GetQueryable();
		}

		public IQueryable<TResult> GetSolrQueryable<TResult>(SolrSearchContext context, IExecutionContext executionContext) where TResult : IItemWrapper
		{
			// once the hacks in the Hacks namespace are fixed (around update 2, I hear), the commented line below can be used instead of BugFixIndex
			// in fact once Update 3? is released, this class may become largely irrelevant as interface support is coming natively
			//var linqToSolrIndex = (executionContext == null) ? new LinqToSolrIndex<TResult>(context) : new LinqToSolrIndex<TResult>(context, executionContext);
			var linqToSolrIndex = (executionContext == null)
										? new CustomLinqToSolrIndex<TResult>(context)
										: new CustomLinqToSolrIndex<TResult>(context, executionContext);

			if (ContentSearchConfigurationSettings.EnableSearchDebug)
			{
				((IHasTraceWriter)linqToSolrIndex).TraceWriter = new LoggingTraceWriter(SearchLog.Log);
			}

			return linqToSolrIndex.GetQueryable();
		}
	}
}