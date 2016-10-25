using Fortis.Dynamics;
using System.Reflection;

namespace Fortis.Fields.Dynamics
{
	public interface IAddFieldDynamicProperty
	{
		void Add(IFortisDynamicObject dynamicObject, PropertyInfo property, IField field);
	}
}
