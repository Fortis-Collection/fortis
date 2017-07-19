using System;
using System.Collections.Generic;

namespace Fortis.Items.Context
{
	public interface IContextItemGetChildren
	{
		IEnumerable<T> GetChildren<T>(Guid id);
		IEnumerable<T> GetChildren<T>(string path);
	}
}