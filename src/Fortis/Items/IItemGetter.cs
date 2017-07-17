using System;

namespace Fortis.Items
{
	public interface IItemGetter
	{
		T GetItem<T>(Guid id, string database);
		T GetItem<T>(Guid id, string language, string database);
		T GetItem<T>(Guid id, string language, int version, string database);
		T GetItem<T>(string path, string database);
		T GetItem<T>(string path, string language, string database);
		T GetItem<T>(string path, string language, int version, string database);
	}
}
