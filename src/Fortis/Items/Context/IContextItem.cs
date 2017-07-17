namespace Fortis.Items.Context
{
	public interface IContextItem
	{
		T GetItem<T>();
	}
}
