using System;
using System.Reflection;
using Fortis.Dynamics;

namespace Fortis.Fields.Dynamics
{
	public class AddFieldDynamicProperty : IAddFieldDynamicProperty
	{
		protected readonly IAddFieldDynamicPropertyStrategies Strategies;

		public AddFieldDynamicProperty(
			IAddFieldDynamicPropertyStrategies strategies)
		{
			Strategies = strategies;
		}

		public void Add(IFortisDynamicObject dynamicObject, PropertyInfo property, IField field)
		{
			var strategy = Strategies.Select(property, field);

			if (strategy == null)
			{
				throw new Exception($"Fortis: no strategy found " +
									$" [ Property Name: {property.Name} | Property Type: {property.PropertyType.Name} ] " +
									$" [ Item ID: {field.Field.ID} | Field Name: {field.Name} | Field Type: {field.Type} ]");
			}

			strategy.Add(dynamicObject, property, field);
		}
	}
}
