using Fortis.Fields;
using NSubstitute;
using System.Collections.Generic;
using Xunit;

namespace Fortis.Test.Fields
{
	public class TypedFieldFactoriesTests
	{
		[Fact]
		public void Count_Factories_CorrectCount()
		{
			var factoryName = "test";
			var typedFieldFactory1 = Substitute.For<ITypedFieldFactory>();
			var typedFieldFactory2 = Substitute.For<ITypedFieldFactory>();

			typedFieldFactory1.Name.Returns(factoryName + 1);
			typedFieldFactory2.Name.Returns(factoryName + 2);

			var typedFieldFactoriesEnumerable = new List<ITypedFieldFactory>
			{
				typedFieldFactory1,
				typedFieldFactory2
			};
			var typedFieldFactories = new TypedFieldFactories(typedFieldFactoriesEnumerable);

			Assert.Equal(typedFieldFactories, typedFieldFactoriesEnumerable);
		}

		[Fact]
		public void Find_ValidFieldType_NotNull()
		{
			var fieldType = "test";
			var typedFieldFactory = Substitute.For<ITypedFieldFactory>();

			typedFieldFactory.CanCreate(fieldType).Returns(true);

			var typedFieldFactoriesEnumerable = new List<ITypedFieldFactory>
			{
				typedFieldFactory
			};
			var typedFieldFactories = new TypedFieldFactories(typedFieldFactoriesEnumerable);
			var factory = typedFieldFactories.Find(fieldType);

			Assert.NotNull(factory);
		}

		[Fact]
		public void Find_InvalidFieldType_Null()
		{
			var fieldType = "test";
			var typedFieldFactory = Substitute.For<ITypedFieldFactory>();

			typedFieldFactory.CanCreate(fieldType).Returns(false);

			var typedFieldFactoriesEnumerable = new List<ITypedFieldFactory>
			{
				typedFieldFactory
			};
			var typedFieldFactories = new TypedFieldFactories(typedFieldFactoriesEnumerable);
			var factory = typedFieldFactories.Find(fieldType);

			Assert.Null(factory);
		}
	}
}
