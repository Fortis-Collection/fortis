using Fortis.Dynamics;
using System;
using System.Linq;
using Sitecore.Data.Items;
using System.Collections.Generic;
using Sitecore.Data;

namespace Fortis.Items
{
	public class BaseItem : FortisDynamicObject, IBaseItem
	{
		protected readonly ISitecoreItemGetter SitecoreItemGetter;
		protected readonly IItemFactory ItemFactory;

		public BaseItem(
			ISitecoreItemGetter sitecoreItemGetter,
			IItemFactory itemFactory)
		{
			SitecoreItemGetter = sitecoreItemGetter;
			ItemFactory = itemFactory;
		}

		private Item item;
		public Item Item
		{
			get
			{
				if (item == null &&
					itemId != null &&
					!string.IsNullOrEmpty(itemDatabase))
				{
					item = itemVersion == null ?
						SitecoreItemGetter.GetItem(itemId.Value, itemLanguage, itemDatabase) :
						SitecoreItemGetter.GetItem(itemId.Value, itemLanguage, itemVersion.Value, itemDatabase);
				}

				return item;
			}
			set { item = value; }
		}
		private Guid? itemId;
		public Guid ItemId
		{
			get { return Item.ID.Guid; }
			set { itemId = value; }
		}
		public string ItemLongId => Item.Paths.LongID;
		public string ItemShortId => Item.ID.ToShortID().ToString();
		public string ItemPath => Item.Paths.Path;
		public string ItemName
		{
			get { return Item.Name; }
			set { Item.Name = value; }
		}
		public string ItemDisplayName => Item.DisplayName;
		private string itemLanguage;
		public string ItemLanguage
		{
			get { return item == null ? itemLanguage : item.Language.Name; }
			set { itemLanguage = value; }
		}
		private string itemDatabase;
		public string ItemDatabase
		{
			get { return item == null ? itemDatabase : item.Database.Name; }
			set { itemDatabase = value; }
		}
		public Guid ItemTemplateId => Item.TemplateID.Guid;
		public string ItemTemplateName => Item.TemplateName;
		public IEnumerable<Guid> ItemTemplateIds => Item.Template.BaseTemplates.Select(bt => bt.ID.Guid);
		private int? itemVersion;
		public int ItemVersion
		{
			get { return item == null ? (itemVersion == null ? default(int) : itemVersion.Value) : item.Version.Number; }
			set { itemVersion = value; }
		}
		public bool ItemIsLatestVersion => Item.Versions.IsLatestVersion();
		public bool ItemIsStandardValues => StandardValuesManager.IsStandardValuesHolder(Item);
		public int ItemChildrenCount => Item.Children.Count;
		public bool ItemHasChildren => Item.HasChildren;

		public IEnumerable<T> GetChildren<T>()
		{
			if (ItemHasChildren)
			{
				return ItemFactory.Create<T>(Item.Children.AsEnumerable());
			}

			return Enumerable.Empty<T>();
		}
		public IEnumerable<T> GetChildrenRecursive<T>()
		{
			return GetChildrenRecursive<T>(this);
		}
		public IEnumerable<T> GetChildrenRecursive<T>(IItem item)
		{
			var children = item.GetChildren<IItem>();

			foreach (var child in children)
			{
				if (child is T)
				{
					yield return (T)child;
				}

				if (child.ItemHasChildren)
				{
					var innerChildren = GetChildrenRecursive<T>(child);

					foreach (var innerChild in innerChildren)
					{
						yield return innerChild;
					}
				}
			}
		}

		public T GetParent<T>()
		{
			return ItemFactory.Create<T>(Item.Parent);
		}
		public T GetParentOrSelf<T>()
		{
			if (this is T)
			{
				return (T)(this as IItem);
			}

			return GetParent<T>();
		}
		public T GetAncestor<T>()
		{
			var parent = GetParent<IItem>();

			while (parent != null)
			{
				if (parent is T)
				{
					return (T)parent;
				}

				parent = parent.GetParent<IItem>();
			}

			return default(T);
		}
		public T GetAncestorOrSelf<T>()
		{
			if (this is T)
			{
				return (T)(this as IItem);
			}

			return GetAncestor<T>();
		}

		public IEnumerable<T> GetSiblings<T>()
		{
			var parent = GetParent<IItem>();

			return parent.GetChildren<T>();
		}
	}
}
