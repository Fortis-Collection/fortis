using Fortis.Fields;
using Fortis.Fields.TextField;
using Fortis.Items;
using NSubstitute;
using System.Collections.Generic;
using Xunit;
using Sitecore.FakeDb;
using Fortis.Fields.BooleanField;
using Fortis.Fields.DateTimeField;
using System;
using Fortis.Fields.Dynamics;
using Fortis.Dynamics;

namespace Fortis.Test.Items
{
	public class ItemFactoryTests : ItemTestAutoFixture
	{
		private const string itemName = "Test Item";
		private const string testTextFieldValue = "Test Text Field Value";
		private DateTime testDateTimeFieldValue = new DateTime(2016, 4, 28, 22, 0, 0);
		private bool testBooleanFieldValue = true;

		[Fact]
		public void Create_TestModel_NotNull()
		{
			var itemFactory = CreateItemFactory();
			var item = itemFactory.Create<ITestModel>(Item);

			Assert.NotNull(item);
		}

		[Fact]
		public void Create_TextFieldProperty_NotNull()
		{
			var itemFactory = CreateItemFactory();
			var item = itemFactory.Create<ITestModel>(Item);

			Assert.NotNull(item.TestField);
		}

		[Fact]
		public void Create_StringProperty_FieldValue()
		{
			var itemFactory = CreateItemFactory();
			var item = itemFactory.Create<ITestModel>(Item);

			Assert.Equal(testTextFieldValue, item.Test);
		}

		[Fact]
		public void Create_DateTimeFieldProperty_NotNull()
		{
			var itemFactory = CreateItemFactory();
			var item = itemFactory.Create<ITestModel>(Item);

			Assert.NotNull(item.TestDateTimeField);
		}

		[Fact]
		public void Create_DateTimeProperty_FieldValue()
		{
			var itemFactory = CreateItemFactory();
			var item = itemFactory.Create<ITestModel>(Item);

			Assert.Equal(testDateTimeFieldValue, item.TestDateTime);
		}

		[Fact]
		public void Create_BooleanFieldProperty_NotNull()
		{
			var itemFactory = CreateItemFactory();
			var item = itemFactory.Create<ITestModel>(Item);

			Assert.NotNull(item.TestBooleanField);
		}

		[Fact]
		public void Create_BooleanProperty_FieldValue()
		{
			var itemFactory = CreateItemFactory();
			var item = itemFactory.Create<ITestModel>(Item);

			Assert.Equal(testBooleanFieldValue, item.TestBoolean);
		}

		[Fact]
		public void Create_AttributeProperty_FieldValue()
		{
			var itemFactory = CreateItemFactory();
			var item = itemFactory.Create<ITestModel>(Item);

			Assert.Equal(testBooleanFieldValue, item.Boolean);
		}

		[Fact]
		public void Create_NonField_DefaultValue()
		{
			var itemFactory = CreateItemFactory();
			var item = itemFactory.Create<ITestModel>(Item);

			Assert.Equal(default(string), item.NonField);
		}

		[Fact]
		public void Create_NonField_CanSet()
		{
			var itemFactory = CreateItemFactory();
			var item = itemFactory.Create<ITestModel>(Item);

			var expected = "Test";

			item.NonField = expected;

			Assert.Equal(expected, item.NonField);
		}

		[Fact]
		public void Create_UnhandledReturnTypeForField_ThrowException()
		{
			var itemFactory = CreateItemFactory();

			Assert.Throws(typeof(Exception), () => itemFactory.Create<IBadTestModel>(Item));
		}

		[Fact]
		public void Create_TestItemModel_NotNull()
		{
			var itemFactory = CreateItemFactory();
			var item = itemFactory.Create<ITestItemModel>(Item);

			Assert.NotNull(item);
		}

		public override void SetField(ref DbField field)
		{
			field.Name = "Test";
			field.Type = "Single-Line Text";
			field.Value = testTextFieldValue;
		}

		public override void SetItem(ref DbItem item)
		{
			item.Name = itemName;
			item.Fields.Add(new DbField("Test Date Time")
			{
				Type = "DateTime",
				Value = "20160428T220000Z"
			});
			item.Fields.Add(new DbField("Test Boolean")
			{
				Type = "Checkbox",
				Value = "1"
			});
		}

		public interface ITestModel
		{
			ITextField TestField { get; }
			string Test { get; set; }
			IDateTimeField TestDateTimeField { get; }
			DateTime TestDateTime { get; set; }
			IBooleanField TestBooleanField { get; }
			bool TestBoolean { get; set; }
			[Field("Test Boolean")]
			bool Boolean { get; }
			string NonField { get; set; }
		}

		public interface IBadTestModel
		{
			DateTime Test { get; set; }
		}

		public interface ITestItemModel : IItem, ITestModel
		{

		}

		public ItemFactory CreateItemFactory()
		{
			return new ItemFactory(
				CreateMockFieldFactory(),
				CreateMockPropertyInfoFieldNameParser(),
				CreateMockAddFieldDynamicProperty(),
				new DynamicObjectCaster()
			);
		}

		public IFieldFactory CreateMockFieldFactory()
		{
			var mappingValidator = Substitute.For<ITypedFieldMappingValidator>();
			var textFieldFactory = new TextFieldFactory(mappingValidator);
			var dateTimeFieldFactory = new DateTimeFieldFactory(mappingValidator);
			var booleanFieldFactory = new BooleanFieldFactory(mappingValidator);

			mappingValidator.IsValid("Single-Line Text", textFieldFactory.Name).Returns(true);
			mappingValidator.IsValid("DateTime", dateTimeFieldFactory.Name).Returns(true);
			mappingValidator.IsValid("Checkbox", booleanFieldFactory.Name).Returns(true);

			return new FieldFactory(
					new TypedFieldFactories(
						new List<ITypedFieldFactory>
						{
							textFieldFactory,
							dateTimeFieldFactory,
							booleanFieldFactory
						}
					)
				);
		}

		public IPropertyInfoFieldNameParser CreateMockPropertyInfoFieldNameParser()
		{
			return new PropertyInfoFieldNameParser(
					new FieldNameParser()
				);
		}

		public IAddFieldDynamicProperty CreateMockAddFieldDynamicProperty()
		{
			var addValueFieldDynamicProperty = new AddValueFieldDynamicProperty();
			return new AddFieldDynamicProperty(
					new AddFieldDynamicPropertyStrategies(
							new List<IAddFieldDynamicPropertyStrategy>
							{
								new AddFieldDynamicPropertyStrategy(),
								new AddFieldDynamicPropertyStrategy<bool>(addValueFieldDynamicProperty),
								new AddFieldDynamicPropertyStrategy<DateTime>(addValueFieldDynamicProperty),
								new StringAddFieldDynamicPropertyStrategy()
							}
						)
				);
		}
	}
}
