using Fortis.Fields;
using Xunit;

namespace Fortis.Test.Fields
{
	public class PropertyInfoFieldNameParserTests
	{
		[Fact]
		public void Parse_Attribute_SpecifiedAttributeName()
		{
			var propertyInfo = typeof(ITestModel).GetProperty("TestField");
			var propertyInfoFieldNameParser = new PropertyInfoFieldNameParser(null);

			var expected = "Test";
			var actual = propertyInfoFieldNameParser.Parse(propertyInfo);

			Assert.Equal(expected, actual);
		}

		public interface ITestModel
		{
			[Field("Test")]
			string TestField { get; set; }
		}
	}
}
