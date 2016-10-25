using ImpromptuInterface;

namespace Fortis.Dynamics
{
	public class DynamicObjectCaster : IDynamicObjectCaster
	{
		public T Cast<T>(IFortisDynamicObject dynamicObject)
		{
			return Impromptu.ActLike(dynamicObject);
		}
	}
}
