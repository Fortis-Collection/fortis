using Sitecore.Data.Fields;

namespace Fortis.Fields
{
	public interface IFieldFactory
	{
		IField Create(Field field);
	}
}