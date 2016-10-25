using Sitecore.Data.Fields;

namespace Fortis.Fields
{
	public interface ITypedFieldFactory
	{
		IField Create(Field field);
		bool CanCreate(string fieldType);
		string Name { get; }
	}
}
