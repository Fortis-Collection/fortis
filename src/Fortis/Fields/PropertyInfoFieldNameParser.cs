using System.Reflection;

namespace Fortis.Fields
{
	public class PropertyInfoFieldNameParser : IPropertyInfoFieldNameParser
	{
		protected readonly IFieldNameParser FieldNameParser;

		public PropertyInfoFieldNameParser(
			IFieldNameParser fieldNameParser)
		{
			FieldNameParser = fieldNameParser;
		}

		public string Parse(PropertyInfo property)
		{
			var fieldAttribute = property.GetCustomAttribute<FieldAttribute>();

			if (fieldAttribute != null)
			{
				return fieldAttribute.Name;
			}

			return FieldNameParser.Parse(property.Name);
		}
	}
}
