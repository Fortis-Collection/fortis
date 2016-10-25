using Fortis.Fields;
using NSubstitute;
using Sitecore.FakeDb;
using System;
using Xunit;

namespace Fortis.Test.Fields
{
	public class TypedFieldFactory_3Tests : ItemTestAutoFixture
	{
		private const string fieldType = "checkbox";

		[Fact]
		public void Create_ValidFieldType_ReturnsField()
		{
			var typedFieldMappingValidator = Substitute.For<ITypedFieldMappingValidator>();
			var factory = new MockTypedFieldFactory(typedFieldMappingValidator);

			factory.CanCreate(fieldType).Returns(true);

			var modelledField = factory.Create(Field);

			Assert.NotNull(modelledField);
		}

		[Fact]
		public void Create_InvalidFieldType_ThrowsExcecption()
		{
			var typedFieldMappingValidator = Substitute.For<ITypedFieldMappingValidator>();
			var factory = new MockTypedFieldFactory(typedFieldMappingValidator);

			factory.CanCreate(fieldType).Returns(false);

			Assert.Throws(typeof(Exception), () => factory.Create(Field));
		}

		public override void SetField(ref DbField field)
		{
			field.Type = fieldType;
		}

		public class MockTypedFieldFactory : TypedFieldFactory<BaseField, IField, Sitecore.Data.Fields.Field>
		{
			public MockTypedFieldFactory(ITypedFieldMappingValidator mappingValidator)
				: base(mappingValidator)
			{
			}

			public override string Name => "Test";
		}
	}
}
