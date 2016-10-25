using Fortis.Fields;
using NSubstitute;
using System;
using Xunit;
using Sitecore.Data.Fields;

namespace Fortis.Test.Fields
{
	public class TypedFieldFactory_1Tests
	{
		[Theory]
		[InlineData("valid", true)]
		[InlineData("invalid", false)]
		public void CanCreate_FieldType_ReturnsExpected(string fieldType, bool expected)
		{
			var typedFieldMappingValidator = Substitute.For<ITypedFieldMappingValidator>();
			var factory = new MockTypedFieldFactory(typedFieldMappingValidator);

			typedFieldMappingValidator.IsValid("valid", factory.Name).Returns(true);

			var actual = factory.CanCreate(fieldType);

			Assert.Equal(expected, actual);
		}

		public class MockTypedFieldFactory : TypedFieldFactory<BaseField>
		{
			public MockTypedFieldFactory(ITypedFieldMappingValidator mappingValidator)
				: base(mappingValidator)
			{
			}

			public override string Name => "Test";

			public override BaseField Create(Field field)
			{
				throw new NotImplementedException();
			}
		}
	}
}
