using System;
using System.Collections.Generic;

namespace Fortis.Items
{
	public interface IItemGetChildren
	{
		IEnumerable<T> GetChildren<T>(Guid id, string database);
		IEnumerable<T> GetChildren<T>(string path, string database);
	}
}