using Fortis.Fields;
using NSubstitute;
using System;
using Xunit;

namespace Fortis.Test.Fields
{
	public class FieldFactoryTests : ItemTestAutoFixture
	{
		[Fact]
		public void Create_SupportedFieldType_NoException()
		{
			var typedFieldFactories = Substitute.For<ITypedFieldFactories>();
			var typedFieldFactory = Substitute.For<ITypedFieldFactory>();

			typedFieldFactory.Create(Field).Returns(Substitute.For<IField>());
			typedFieldFactories.Find(Field.Type).Returns(typedFieldFactory);

			var fieldFactory = new FieldFactory(typedFieldFactories);
			var modelledField = fieldFactory.Create(Field);

			Assert.NotNull(modelledField);
		}

		[Fact]
		public void Create_UnsupportedFieldType_Exception()
		{
			var typedFieldFactories = Substitute.For<ITypedFieldFactories>();

			typedFieldFactories.Find("text").Returns((ITypedFieldFactory)null);

			var fieldFactory = new FieldFactory(typedFieldFactories);

			Assert.Throws(typeof(Exception), () => fieldFactory.Create(Field));
		}
	}
}
