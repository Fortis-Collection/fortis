using Fortis.Fields;
using NSubstitute;
using System.Collections.Generic;
using Xunit;

namespace Fortis.Test.Fields
{
	public class TypedFieldMappingValidatorTests
	{
		[Theory]
		[InlineData("VALIDFIELDTYPE", "VALIDFACTORYNAME", true)]
		[InlineData("validFieldType", "validFactoryName", true)]
		[InlineData("invalidFieldType", "validFactoryName", false)]
		[InlineData("validFieldType", "invalidFactoryName", false)]
		[InlineData("invalidFieldType", "invalidFactoryName", false)]
		public void IsValid_FieldTypeFactoryName_ReturnsExpected(string fieldType, string factoryName, bool expected)
		{
			var configuration = Substitute.For<ITypedFieldFactoryConfiguration>();
			var validator = new TypedFieldMappingValidator(configuration);

			configuration.FieldTypeMappings.Returns(new Dictionary<string, string>
			{
				{ "validFieldType", "validFactoryName" }
			});

			var actual = validator.IsValid(fieldType, factoryName);

			Assert.Equal(expected, actual);
		}
	}
}
