using System;
using System.Reflection;
using Fortis.Dynamics;

namespace Fortis.Fields.Dynamics
{
	public class StringAddFieldDynamicPropertyStrategy : IAddFieldDynamicPropertyStrategy
	{
		public bool CanAdd(PropertyInfo property, IField field)
		{
			return string.Equals(property.PropertyType.Name, typeof(string).Name);
		}

		public void Add(IFortisDynamicObject dynamicObject, PropertyInfo property, IField field)
		{
			dynamicObject.AddDynamicProperty($"{property.Name}_get", ((Func<string>)(() => { return field.Value; })));
			dynamicObject.AddDynamicProperty($"{property.Name}_set", ((Action<string>)((string value) => { field.Value = value; })));
		}
	}
}
