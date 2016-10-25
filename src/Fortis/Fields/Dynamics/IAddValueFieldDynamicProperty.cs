using Fortis.Dynamics;

namespace Fortis.Fields.Dynamics
{
	public interface IAddValueFieldDynamicProperty
	{
		void Add<TReturnType>(IFortisDynamicObject modelledItem, IField<TReturnType> modelledField, string propertyName);
	}
}
