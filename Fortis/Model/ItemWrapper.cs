using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Configuration;
using Sitecore.Data.Items;
using Sitecore.Publishing;
using Fortis.Model.Fields;

namespace Fortis.Model
{
	public class ItemWrapper : IItemWrapper, IDisposable
	{
		private Item _item;
		private Dictionary<string, IFieldWrapper> _fields;

		protected Item Item
		{
			get { return _item; }
		}

		protected Dictionary<string, IFieldWrapper> Fields
		{
			get { return _fields; }
		}

		public object Original
		{
			get { return _item; }
		}

		public string DatabaseName
		{
			get { return Item.Database.Name; }
		}

		public string LanguageName
		{
			get { return Item.Language.Name; }
		}

		public string ItemLocation
		{
			get { return Item.Paths.FullPath; }
		}

		public Guid ItemID
		{
			get { return Item.ID.Guid; }
		}

		public string ItemShortID
		{
			get { return Item.ID.ToShortID().ToString(); }
		}

		public string ItemName
		{
			get { return Item.Name; }
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

		public ItemWrapper(Item item)
		{
			_item = item;
			_fields = new Dictionary<string, IFieldWrapper>();
		}

		protected FieldWrapper GetField(string key)
		{
			key = key.ToLower();

			if (!Fields.Keys.Contains(key))
			{
				try
				{
					var scField = Item.Fields[key];

					switch (scField.Type.ToLower())
					{
						case "checkbox":
							Fields[key] = new BooleanFieldWrapper(scField);
							break;
						case "image":
							Fields[key] = new ImageFieldWrapper(scField);
							break;
						case "date":
						case "datetime":
							Fields[key] = new DateTimeFieldWrapper(scField);
							break;
						case "checklist":
						case "treelist":
						case "treelistex":
						case "multilist":
							Fields[key] = new ListFieldWrapper(scField);
							break;
						case "file":
							Fields[key] = new FileFieldWrapper(scField);
							break;
						case "droplink":
						case "droptree":
							Fields[key] = new LinkFieldWrapper(scField);
							break;
						case "general link":
							Fields[key] = new GeneralLinkFieldWrapper(scField);
							break;
						case "text":
						case "single-line text":
						case "multi-line text":
						case "number":
						case "droplist":
							Fields[key] = new TextFieldWrapper(scField);
							break;
						case "rich text":
							Fields[key] = new RichTextFieldWrapper(scField);
							break;
						default:
							Fields[key] = null;
							break;
					}
				}
				catch
				{
					// Todo: Log error
				}
			}
			return (FieldWrapper)Fields[key];
		}

		public void Save()
		{
			if (_item.Editing.IsEditing)
			{
				_item.Editing.EndEdit();
			}
		}

		public void Delete()
		{
			_item.Delete();
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

		public virtual T Parent<T>() where T : IItemWrapper
		{
			return Parent<T>(Item.Parent);
		}

		protected T Parent<T>(Item parent) where T : IItemWrapper
		{
			IItemWrapper wrapper = null;

			while (parent != null)
			{
				wrapper = Spawn.FromItem<T>(parent);

				if (wrapper is T)
				{
					return (T)wrapper;
				}

				parent = parent.Parent;
			}

			return (T)((wrapper is T) ? wrapper : null);
		}
	}
}
