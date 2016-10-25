using System;
using Fortis.Dynamics;

namespace Fortis.Fields.Dynamics
{
	public class AddValueFieldDynamicProperty : IAddValueFieldDynamicProperty
	{
		public void Add<TReturnType>(IFortisDynamicObject modelledItem, IField<TReturnType> modelledField, string propertyName)
		{
			modelledItem.AddDynamicProperty($"{propertyName}_get", ((Func<TReturnType>)(() => { return modelledField.Value; })));
			modelledItem.AddDynamicProperty($"{propertyName}_set", ((Action<TReturnType>)((TReturnType value) => { modelledField.Value = value; })));
		}
	}
}
