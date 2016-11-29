using Fortis.Items;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace Fortis.Test.Items
{
	public class TemplateTypeMapTests
	{
		private const string templateId = "{38e74a30-c916-406e-bcfe-a85589a3e6d7}";
		private const string invalidTemplateId = "{0edd28c8-a1cb-4bb7-967c-6057a2eec575}";

		[Theory]
		[InlineData(templateId, true)]
		[InlineData(invalidTemplateId, false)]
		public void Contains_TemplateId_Expected(string id, bool expected)
		{
			var templateTypeMap = new TemplateTypeMap(CreateMockTypesSource());

			var actual = templateTypeMap.Contains(new Guid(id));

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void Contains_ValidType_True()
		{
			var templateTypeMap = new TemplateTypeMap(CreateMockTypesSource());

			var condition = templateTypeMap.Contains(typeof(ITestTemplateModel));

			Assert.True(condition);
		}

		[Fact]
		public void Contains_InvalidType_True()
		{
			var templateTypeMap = new TemplateTypeMap(CreateMockTypesSource());

			var condition = templateTypeMap.Contains(typeof(ITestModel));

			Assert.False(condition);
		}

		[Fact]
		public void Find_ValidTemplateId_Type()
		{
			var templateTypeMap = new TemplateTypeMap(CreateMockTypesSource());

			var expected = typeof(ITestTemplateModel);
			var actual = templateTypeMap.Find(new Guid(templateId));

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void Find_InvalidTemplateId_Type()
		{
			var templateTypeMap = new TemplateTypeMap(CreateMockTypesSource());

			var obj = templateTypeMap.Find(new Guid(invalidTemplateId));

			Assert.Null(obj);
		}

		[Fact]
		public void Find_ValidType_Guid()
		{
			var templateTypeMap = new TemplateTypeMap(CreateMockTypesSource());

			var expected = new Guid(templateId);
			var actual = templateTypeMap.Find(typeof(ITestTemplateModel));

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void Find_InvalidType_DefaultGuid()
		{
			var templateTypeMap = new TemplateTypeMap(CreateMockTypesSource());

			var expected = default(Guid);
			var actual = templateTypeMap.Find(typeof(ITestModel));

			Assert.Equal(expected, actual);
		}

		[Template(templateId)]
		public interface ITestTemplateModel
		{

		}

		public interface ITestModel
		{

		}

		public ITypesSource CreateMockTypesSource()
		{
			var mockTypesSource = Substitute.For<ITypesSource>();

			mockTypesSource.Types.Returns(new List<Type>(GetType().GetNestedTypes()));

			return mockTypesSource;
		}
	}
}
