using System;

namespace Fortis.Dynamics
{
	public interface IDynamicObjectCaster
	{
		T Cast<T>(IFortisDynamicObject dynamicObject, params Type[] otherInterfaces);
	}
}
