using System.Reflection;
using Fortis.Dynamics;

namespace Fortis.Fields.Dynamics
{
	public class AddFieldDynamicPropertyStrategy : IAddFieldDynamicPropertyStrategy
	{
		public bool CanAdd(PropertyInfo property, IField field)
		{
			return property.PropertyType.IsAssignableFrom(field.GetType());
		}

		public void Add(IFortisDynamicObject dynamicObject, PropertyInfo property, IField field)
		{
			dynamicObject.AddDynamicProperty(property.Name, field);
		}
	}
}
