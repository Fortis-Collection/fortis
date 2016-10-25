using System;
using System.Linq;

namespace Fortis.Fields
{
	public class TypedFieldMappingValidator : ITypedFieldMappingValidator
	{
		protected readonly ITypedFieldFactoryConfiguration Configuration;

		public TypedFieldMappingValidator(
			ITypedFieldFactoryConfiguration configuration)
		{
			Configuration = configuration;
		}

		public bool IsValid(string fieldType, string factoryName)
		{
			return Configuration.FieldTypeMappings.Any(ftm =>
				string.Equals(factoryName, ftm.Value, StringComparison.InvariantCultureIgnoreCase) &&
				string.Equals(fieldType, ftm.Key, StringComparison.InvariantCultureIgnoreCase)
			);
		}
	}
}
