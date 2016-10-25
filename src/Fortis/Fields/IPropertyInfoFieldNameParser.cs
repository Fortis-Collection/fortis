using System.Reflection;

namespace Fortis.Fields
{
	public interface IPropertyInfoFieldNameParser
	{
		string Parse(PropertyInfo property);
	}
}
