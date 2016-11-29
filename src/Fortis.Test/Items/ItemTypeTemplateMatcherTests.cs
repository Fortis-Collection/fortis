using Fortis.Items;
using NSubstitute;
using System;
using System.Collections.Generic;
using Xunit;

namespace Fortis.Test.Items
{
	public class ItemTypeTemplateMatcherTests : ItemTestAutoFixture
	{
		[Fact]
		public void Find_Model_ModelType()
		{
			var itemTypeTemplateMatcher = new ItemTypeTemplateMatcher(CreateMockTemplateTypeMap());

			var expected = typeof(ITestModel);
			var actual = itemTypeTemplateMatcher.Find<ITestModel>(Item);

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void Find_TemplateModelInvalidItem_Null()
		{
			// Model with template restriction and item which doesn't inherit from the template
			var itemTypeTemplateMatcher = new ItemTypeTemplateMatcher(CreateMockTemplateTypeMap());

			var obj = itemTypeTemplateMatcher.Find<ITestBTemplateModel>(Item);

			Assert.Null(obj);
		}

		[Fact]
		public void Find_TemplateModelValidItemNoDirectMatch_RequestedModelType()
		{
			// Model with template restriction, item inherits from template but no matching template model
			var itemTypeTemplateMatcher = new ItemTypeTemplateMatcher(CreateMockTemplateTypeMap(new List<Type>
			{
				typeof(ITestModel),
				typeof(ITestATemplateModel),
				typeof(ITestBTemplateModel)
			}));

			var expected = typeof(ITestATemplateModel);
			var actual = itemTypeTemplateMatcher.Find<ITestATemplateModel>(Item);

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void Find_TemplateModelValidItemDirectMatchAssignable_MatchingModelType()
		{
			var itemTypeTemplateMatcher = new ItemTypeTemplateMatcher(CreateMockTemplateTypeMap());

			var expected = typeof(ITestCTemplateModel);
			var actual = itemTypeTemplateMatcher.Find<ITestATemplateModel>(Item);

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void Find_TemplateModelValidItemDirectMatchNotAssignable_RequestedModelType()
		{
			// Model with template restriction, item inherits from template and matching template model but model is not assignable to requested model
			var itemTypeTemplateMatcher = new ItemTypeTemplateMatcher(CreateMockTemplateTypeMap(new List<Type>
			{
				typeof(ITestModel),
				typeof(ITestATemplateModel),
				typeof(ITestBTemplateModel),
				typeof(ITestNonAssignableCTemplateModel)
			}));

			var expected = typeof(ITestNonAssignableCTemplateModel);
			var actual = itemTypeTemplateMatcher.Find<ITestNonAssignableCTemplateModel>(Item);

			Assert.Equal(expected, actual);
		}

		public interface ITestModel
		{

		}

		[Template(ItemTemplateAId)]
		public interface ITestATemplateModel
		{

		}

		[Template(ItemTemplateBId)]
		public interface ITestBTemplateModel
		{

		}

		[Template(ItemTemplateCId)]
		public interface ITestCTemplateModel : ITestATemplateModel
		{

		}

		[Template(ItemTemplateCId)]
		public interface ITestNonAssignableCTemplateModel
		{

		}

		public ITemplateTypeMap CreateMockTemplateTypeMap()
		{
			return CreateMockTemplateTypeMap(new List<Type>
			{
				typeof(ITestModel),
				typeof(ITestATemplateModel),
				typeof(ITestBTemplateModel),
				typeof(ITestCTemplateModel)
			});
		}

		public ITemplateTypeMap CreateMockTemplateTypeMap(IEnumerable<Type> types)
		{
			var mockTypesSource = Substitute.For<ITypesSource>();

			mockTypesSource.Types.Returns(new List<Type>(types));

			return new TemplateTypeMap(mockTypesSource);
		}
	}
}
