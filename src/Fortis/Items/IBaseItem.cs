using Sitecore.Data.Items;

namespace Fortis.Items
{
	public interface IBaseItem : IItem
	{
		Item Item { get; }
	}
}
