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
		public void Create_TestModel_PropertiesHaveValues()
		{
			var itemFactory = CreateItemFactory();
			var item = itemFactory.Create<ITestModel>(Item);

			Assert.NotNull(item);
			Assert.NotNull(item.TestField);
			Assert.Equal(testTextFieldValue, item.Test);
			Assert.NotNull(item.TestDateTimeField);
			Assert.Equal(testDateTimeFieldValue, item.TestDateTime);
			Assert.NotNull(item.TestBooleanField);
			Assert.Equal(testBooleanFieldValue, item.TestBoolean);
		}

		public void Create_TestItemModel_PropertiesHaveValues()
		{
			var itemFactory = CreateItemFactory();
			var item = itemFactory.Create<ITestItemModel>(Item);

			Assert.NotNull(item);
			Assert.NotNull(item.TestField);
			Assert.Equal(testTextFieldValue, item.Test);
			Assert.NotNull(item.TestDateTimeField);
			Assert.Equal(testDateTimeFieldValue, item.TestDateTime);
			Assert.NotNull(item.TestBooleanField);
			Assert.Equal(testBooleanFieldValue, item.TestBoolean);
			Assert.Equal(itemName, item.ItemName);
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
			string Test { get; }
			IDateTimeField TestDateTimeField { get; }
			DateTime TestDateTime { get; }
			IBooleanField TestBooleanField { get; }
			bool TestBoolean { get; }
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
