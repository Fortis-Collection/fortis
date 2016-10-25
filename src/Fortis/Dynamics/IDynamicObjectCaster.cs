namespace Fortis.Dynamics
{
	public interface IDynamicObjectCaster
	{
		T Cast<T>(IFortisDynamicObject dynamicObject);
	}
}
