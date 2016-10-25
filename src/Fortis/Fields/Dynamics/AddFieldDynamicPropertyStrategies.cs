using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System;

namespace Fortis.Fields.Dynamics
{
	public class AddFieldDynamicPropertyStrategies : IAddFieldDynamicPropertyStrategies
	{
		public AddFieldDynamicPropertyStrategies(
			IEnumerable<IAddFieldDynamicPropertyStrategy> strategies)
		{
			Strategies = strategies;
		}

		public IEnumerable<IAddFieldDynamicPropertyStrategy> Strategies { get; set; }

		public IAddFieldDynamicPropertyStrategy Select(PropertyInfo property, IField field)
		{
			var availableStrategies = Strategies.Where(s => s.CanAdd(property, field));

			if (availableStrategies.Count() > 1)
			{
				throw new Exception("Fortis: multiple strategies found");
			}

			return availableStrategies.FirstOrDefault();
		}
	}
}
