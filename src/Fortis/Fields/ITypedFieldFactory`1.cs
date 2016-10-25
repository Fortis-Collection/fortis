using Sitecore.Data.Fields;

namespace Fortis.Fields
{
	public interface ITypedFieldFactory<T> : ITypedFieldFactory
		where T : IField
	{
		new T Create(Field field);
	}
}
