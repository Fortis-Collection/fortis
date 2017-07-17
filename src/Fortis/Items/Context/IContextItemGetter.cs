using System;

namespace Fortis.Items.Context
{
	public interface IContextItemGetter
	{
		T GetItem<T>(Guid id);
		T GetItem<T>(Guid id, string language);
		T GetItem<T>(Guid id, string language, int version);
		T GetItem<T>(string path);
		T GetItem<T>(string path, string language);
		T GetItem<T>(string path, string language, int version);
	}
}
