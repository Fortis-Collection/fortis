using System.Reflection;

namespace Fortis.Fields
{
	public class PropertyInfoFieldNameParser : IPropertyInfoFieldNameParser
	{
		private const string affixPrefix = "Field";

		public string Parse(PropertyInfo property)
		{
			var fieldAttribute = property.GetCustomAttribute<FieldAttribute>();

			if (fieldAttribute != null)
			{
				return fieldAttribute.Name;
			}

			return property.Name.Replace(affixPrefix, string.Empty);
		}
	}
}
