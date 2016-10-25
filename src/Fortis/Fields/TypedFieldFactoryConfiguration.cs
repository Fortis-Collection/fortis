using System.Collections.Generic;

namespace Fortis.Fields
{
	public class TypedFieldFactoryConfiguration : ITypedFieldFactoryConfiguration
	{
		private Dictionary<string, string> fieldTypeMappings;

		public Dictionary<string, string> FieldTypeMappings
		{
			get
			{
				if (fieldTypeMappings == null)
				{
					fieldTypeMappings = Configuration.TypedFields;
				}

				return fieldTypeMappings;
			}
		}

		public TypedFieldsConfiguration Configuration => Sitecore.Configuration.Factory.CreateObject("fortis/fields/typedFieldsConfiguration", true) as TypedFieldsConfiguration;
	}
}
