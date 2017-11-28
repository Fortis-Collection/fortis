namespace Fortis.Items
{
	public class BaseItemGetter : IBaseItemGetter
	{
		protected readonly IItemGetter ItemGetter;

		public BaseItemGetter(
			IItemGetter itemGetter)
		{
			ItemGetter = itemGetter;
		}

		public IBaseItem GetItem(IItem item)
		{
			var baseItem = item as IBaseItem;

			if (baseItem == null)
			{
				baseItem = ItemGetter.GetItem<IBaseItem>(item.ItemId, item.ItemDatabase);
			}

			return baseItem;
		}
	}
}
