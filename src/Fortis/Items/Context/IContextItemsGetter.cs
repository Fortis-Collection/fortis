using System.Collections.Generic;

namespace Fortis.Items.Context
{
	public interface IContextItemsGetter
	{
		IEnumerable<T> GetItems<T>(string query);
	}
}