using System;
using System.Collections.Generic;

namespace Fortis.Items
{
	public interface ITypesSource
	{
		IEnumerable<Type> Types { get; }
	}
}
