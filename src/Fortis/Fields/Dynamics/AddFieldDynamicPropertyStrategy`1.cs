using Fortis.Dynamics;
using System.Reflection;

namespace Fortis.Fields.Dynamics
{
	public class AddFieldDynamicPropertyStrategy<TReturnType> : IAddFieldDynamicPropertyStrategy
	{
		protected readonly IAddValueFieldDynamicProperty AddValueFieldDynamicProperty;

		public AddFieldDynamicPropertyStrategy(
			IAddValueFieldDynamicProperty addValueFieldDynamicProperty)
		{
			AddValueFieldDynamicProperty = addValueFieldDynamicProperty;
		}

		public bool CanAdd(PropertyInfo property, IField field)
		{
			return string.Equals(property.PropertyType.Name, typeof(TReturnType).Name) &&
					field is IField<TReturnType>;
		}

		public void Add(IFortisDynamicObject dynamicObject, PropertyInfo property, IField field)
		{
			AddValueFieldDynamicProperty.Add(dynamicObject, (IField<TReturnType>)field, property.Name);
		}
	}
}
