using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Configuration;
using Sitecore.Data.Items;
using Sitecore.Publishing;
using Fortis.Model.Fields;
using System.Runtime.CompilerServices;
using Sitecore.ContentSearch;
using System.ComponentModel;
using Sitecore.ContentSearch.Converters;
using Fortis.Providers;

namespace Fortis.Model
{
	public partial class ItemWrapper : IItemWrapper, IDisposable
	{
		private readonly ISpawnProvider _spawnProvider;
		private Item _item;
		private Dictionary<string, IFieldWrapper> _fields;
		private Dictionary<string, object> _lazyFields;

		public ItemWrapper(ISpawnProvider spawnProvider)
			: this(null, spawnProvider)
		{

		}

		public ItemWrapper(Item item, ISpawnProvider spawnProvider)
		{
			_spawnProvider = spawnProvider;
			_item = item;
			_fields = new Dictionary<string, IFieldWrapper>();
			_lazyFields = new Dictionary<string, object>();
		}

		public ItemWrapper(Guid id, ISpawnProvider spawnProvider)
			: this(null, spawnProvider)
		{
			_itemId = id;
		}

		public ItemWrapper(Guid id, Dictionary<string, object> lazyFields, ISpawnProvider spawnProvider)
			: this(id, spawnProvider)
		{
			_lazyFields = lazyFields;
		}

		[IndexerName("LazyFields")]
		public virtual string this[string key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}

				return _lazyFields.ContainsKey(key.ToLowerInvariant()) ? _lazyFields[key.ToLowerInvariant()].ToString() : null;
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}

				_lazyFields[key.ToLowerInvariant()] = value;
			}
		}

		internal Item Item
		{
			get
			{
				if (_item == null && _itemId != default(Guid))
				{
					_item = Sitecore.Context.Database.GetItem(new Sitecore.Data.ID(_itemId));

					if (_item == null)
					{
						throw new Exception("Fortis: Item with ID of " + _itemId + " not found in " + Sitecore.Context.Database.Name);
					}

					if (!_spawnProvider.TemplateMapProvider.IsCompatibleTemplate(_item.TemplateID.Guid, this.GetType()))
					{
						throw new Exception("Fortis: Item " + _itemId + " of template " + _item.TemplateID.Guid + " is not compatible with " + this.GetType());
					}
				}

				return _item;
			}
		}

		protected Dictionary<string, IFieldWrapper> Fields
		{
			get { return _fields; }
		}

		public object Original
		{
			get { return _item; }
		}

		public bool IsLazy
		{
			get { return _item == null; }
		}

		public string DatabaseName
		{
			get { return Item.Database.Name; }
		}

		private string _languageName = null;

		[IndexField("_language")]
		public string LanguageName
		{
			get { return IsLazy && !string.IsNullOrEmpty(_languageName) ? _languageName : Item.Language.Name; }
			set { _languageName = value; }
		}

	    private string _longID;

        /// <summary>
        /// Gets/Sets the LongID of the item. This is all the Guids of the current items parents in a 
        /// single string. It is used for limiting searchs to the descendants of a particular item.
        /// </summary>
	    [IndexField("_path")]
	    public string LongID
	    {
	        get { return IsLazy && !string.IsNullOrWhiteSpace(_longID) ? _longID : Item.Paths.LongID; }
            set { _longID = value; }
	    }

        [IndexField("_content")]
        public string SearchContent { get; set; }

		public string ItemLocation
		{
			get { return Item.Paths.FullPath; }
		}

		private Guid _itemId = default(Guid);

		[TypeConverter(typeof(IndexFieldGuidValueConverter)), IndexField("_group")]
		public Guid ItemID
		{
			get { return IsLazy && _itemId != default(Guid) ? _itemId : Item.ID.Guid; }
			set { _itemId = value; }
		}

		[TypeConverter(typeof(IndexFieldGuidValueConverter)), IndexField("_template")]
		public Guid TemplateId
		{
			get { return _spawnProvider.TemplateMapProvider.TemplateMap.FirstOrDefault(t => t.Value == this.GetType()).Key; }
		}

		[IndexField("_templates")]
		public IEnumerable<Guid> TemplateIds
		{
			get
			{
				foreach (var template in _spawnProvider.TemplateMapProvider.InterfaceTemplateMap)
				{
					if (template.Key.IsAssignableFrom(this.GetType()))
					{
						yield return template.Value;
					}
				}
			}
		}

		public string ItemShortID
		{
			get { return Item.ID.ToShortID().ToString(); }
		}

		private string _name = null;

		[IndexField("_name")]
		public string Name
		{
			get { return IsLazy && !string.IsNullOrEmpty(_name) ? _name : Item.Name; }
			set { _name = value; }
		}

		[IndexField("_name")]
		public string ItemName
		{
			get { return Name; }
			set { Name = value; }
		}

		private string _displayName = null;

		[IndexField("__display_name")]
		public string DisplayName
		{
			get { return IsLazy && !string.IsNullOrEmpty(_displayName) ? _displayName : Item.DisplayName; }
			set { _displayName = value; }
		}

		[IndexField("_latestversion")]
		public bool IsLatestVersion
		{
			get { return Item.Versions.IsLatestVersion(); }
		}

		public int ChildCount
		{
			get { return Item.Children.Count; }
		}

		public bool HasChildren
		{
			get { return Item.HasChildren; }
		}

		public virtual string SearchTitle
		{
			get { return Item.Name; }
		}

		protected T GetField<T>(string key, string lazyFieldsKey = null) where T : IFieldWrapper
		{
			if (!Fields.ContainsKey(key))
			{
				object lazyValue = null;
				var typeOfT = typeof(T);
				object[] constructorArgs;

				// Attempt to get lazy value
				if (lazyFieldsKey != null && _lazyFields != null && _lazyFields.ContainsKey(lazyFieldsKey))
				{
					lazyValue = _lazyFields[lazyFieldsKey];
				}

				if (lazyValue == null)
				{
					var scField = Item.Fields[key];

                    constructorArgs = new object[] { scField, _spawnProvider };
				}
				else
				{
                    constructorArgs = new object[] { key, this, _spawnProvider, lazyValue };
				}

				Fields[key] = (IFieldWrapper)Activator.CreateInstance(typeOfT, constructorArgs);
			}

			return (T)Fields[key];
		}

		[Obsolete("Use GetField<T>")]
		protected IFieldWrapper GetField(string key)
		{
			key = key.ToLower();

			if (!Fields.ContainsKey(key))
			{
				try
				{
					Fields[key] = _spawnProvider.FromField(Item.Fields[key]);
				}
				catch(Exception ex)
				{
					Sitecore.Diagnostics.Log.Error("Fortis: Unable to spawn field with key " + key, ex, this);
				}
			}

			return (FieldWrapper)Fields[key];
		}

		public void Save()
		{
			if (Item.Editing.IsEditing)
			{
				Item.Editing.EndEdit();
			}
		}

		public void Delete()
		{
			Item.Delete();
		}

		public void Publish()
		{
			Publish(false);
		}

		public void Publish(bool children)
		{
			var publishOptions = new PublishOptions(Factory.GetDatabase("master"), Factory.GetDatabase("web"), PublishMode.SingleItem, Item.Language, DateTime.Now);
			var publisher = new Publisher(publishOptions);

			publisher.Options.RootItem = Item;
			publisher.Options.Deep = children;
			publisher.Publish();
		}

		public virtual string GenerateUrl()
		{
			return Sitecore.Links.LinkManager.GetItemUrl(Item);
		}

		public virtual string GenerateUrl(bool includeHostname)
		{
			var options = Sitecore.Links.LinkManager.GetDefaultUrlOptions();
			options.AlwaysIncludeServerUrl = true;

			return Sitecore.Links.LinkManager.GetItemUrl(Item, options);
		}

		public void Dispose()
		{
			if (Item.Editing.IsEditing)
			{
				Item.Editing.CancelEdit();
			}

			_item = null;
		}

		public virtual IEnumerable<T> Children<T>(bool recursive = false) where T : IItemWrapper
		{
			if (Item.HasChildren)
			{
				if (recursive)
				{
					return ChildrenRecursive<T>(this);
				}
				else
				{
					return _spawnProvider.FromItems<T>(Item.Children.AsEnumerable());
				}
			}

			return Enumerable.Empty<T>();
		}

		protected virtual IEnumerable<T> ChildrenRecursive<T>(IItemWrapper wrapper) where T : IItemWrapper
		{
			var children = wrapper.Children<IItemWrapper>();

			foreach (var child in children)
			{
				if (child is T)
				{
					yield return (T)child;
				}

				if (child.HasChildren)
				{
					var innerChildren = ChildrenRecursive<T>(child);

					foreach (var innerChild in innerChildren)
					{
						yield return (T)innerChild;
					}
				}
			}
		}

		public virtual IEnumerable<T> Siblings<T>()
			where T : IItemWrapper
		{
			return Parent<IItemWrapper>().Children<T>();
		}

        /// <summary>
        /// Returns the Parent of the item if it implements T. If it does implement T the method returns null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ancestors"></param>
        /// <returns></returns>
        public virtual T Parent<T>(bool ancestors = true) where T : IItemWrapper
		{
            if (ancestors)
            {
                return Ancestor<T>(Item.Parent);
            }
            var wrapper = _spawnProvider.FromItem<T>(Item.Parent);
            if (wrapper is T)
            {
                return (T)wrapper;
            }
            return (T)((wrapper is T) ? wrapper : null);
		}

        /// <summary>
        /// Returns the Parent of the item if it implements T. If the item implements T, it is returned. If neither implement T the method returns null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual T ParentOrSelf<T>() where T : IItemWrapper
        {
            if (this is T)
            {
                return (T)(this as IItemWrapper);
            }
            return Ancestor<T>(Item.Parent);
        }

        protected T Ancestor<T>(Item parent) where T : IItemWrapper
		{
			IItemWrapper wrapper = null;

			while (parent != null)
			{
				wrapper = _spawnProvider.FromItem<T>(parent);

				if (wrapper is T)
				{
					return (T)wrapper;
				}

				parent = parent.Parent;
			}

			return (T)((wrapper is T) ? wrapper : null);
		}

        /// <summary>
        /// Returns the first Ancestor of the item that implements T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
	    public virtual T Ancestor<T>() where T : IItemWrapper
	    {
	        return this.Ancestor<T>(Item.Parent);
	    }

        /// <summary>
        /// Returns the first Ancestor of the item that implements T. If the item implements T, it is returned.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual T AncestorOrSelf<T>() where T : IItemWrapper
        {
            if (this is T)
            {
                return (T)(this as IItemWrapper);
            }
            return Ancestor<T>(Item.Parent);
        }
	}
}
