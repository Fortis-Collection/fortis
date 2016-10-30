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
		public Guid ItemId
		{
			get { return Item.ID.Guid; }
		}

		public Item Item { get; set; }

		public string ItemName
		{
			get { return Item.Name; }
			set { Item.Name = value; }
		}

		public string ItemLongId => Item.Paths.LongID;

		public string ItemShortId => Item.ID.ToShortID().ToString();

		public string ItemPath => Item.Paths.Path;

		public string ItemDisplayName => Item.DisplayName;

		public string ItemLanguage => Item.Language.Name;

		public string ItemDatabase => Item.Database.Name;

		public Guid ItemTemplateId => Item.TemplateID.Guid;

		public string ItemTemplateName => Item.TemplateName;

		public IEnumerable<Guid> ItemTemplateIds => Item.Template.BaseTemplates.Select(bt => bt.ID.Guid);

		public bool ItemIsLatestVersion => Item.Versions.IsLatestVersion();

		public bool ItemIsStandardValues => StandardValuesManager.IsStandardValuesHolder(Item);

		public int ItemChildrenCount => Item.Children.Count;

		public bool ItemHasChildren => Item.HasChildren;
	}
}
