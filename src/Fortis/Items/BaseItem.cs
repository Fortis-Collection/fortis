using Fortis.Dynamics;
using System;
using Sitecore.Data.Items;

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
		}
	}
}
