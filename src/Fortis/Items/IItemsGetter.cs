using System.Collections.Generic;

namespace Fortis.Items
{
	public interface IItemsGetter
	{
		IEnumerable<T> GetItems<T>(string query, string database);
	}
}