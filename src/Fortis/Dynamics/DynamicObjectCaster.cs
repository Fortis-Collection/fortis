using ImpromptuInterface;
using System;

namespace Fortis.Dynamics
{
	public class DynamicObjectCaster : IDynamicObjectCaster
	{
		public T Cast<T>(IFortisDynamicObject dynamicObject, params Type[] otherInterfaces)
		{
			return otherInterfaces == null ? Impromptu.ActLike(dynamicObject) : Impromptu.ActLike(dynamicObject, otherInterfaces);
		}
	}
}
