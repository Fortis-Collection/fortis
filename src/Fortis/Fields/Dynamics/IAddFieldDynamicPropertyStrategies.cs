using System.Collections.Generic;
using System.Reflection;

namespace Fortis.Fields.Dynamics
{
	public interface IAddFieldDynamicPropertyStrategies
	{
		IEnumerable<IAddFieldDynamicPropertyStrategy> Strategies { get; }
		IAddFieldDynamicPropertyStrategy Select(PropertyInfo property, IField field);
	}
}
