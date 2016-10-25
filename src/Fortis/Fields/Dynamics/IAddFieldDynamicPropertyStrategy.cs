using Fortis.Dynamics;
using System.Reflection;

namespace Fortis.Fields.Dynamics
{
	public interface IAddFieldDynamicPropertyStrategy 
	{
		void Add(IFortisDynamicObject dynamicObject, PropertyInfo property, IField field);
		bool CanAdd(PropertyInfo property, IField field);
	}
}
