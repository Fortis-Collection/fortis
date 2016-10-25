using Sitecore.Data.Items;

namespace Fortis.Items
{
	public interface IItemFactory
	{
		T Create<T>(Item item);
	}
}
