using Fortis.Fields;
using Xunit;

namespace Fortis.Test.Fields
{
	public class FieldNameParserTests
	{
		[Theory]
		[InlineData("Test", "Test")]
		[InlineData("TestField", "Test")]
		[InlineData("FieldTest", "Test")]
		[InlineData("TestOne", "Test One")]
		[InlineData("TestOneField", "Test One")]
		[InlineData("FieldTestOne", "Test One")]
		[InlineData("TestOneTwo", "Test One Two")]
		[InlineData("TestOneTwoField", "Test One Two")]
		[InlineData("FieldTestOneTwo", "Test One Two")]
		[InlineData("TestONETwo", "Test ONE Two")]
		[InlineData("TestONETwoField", "Test ONE Two")]
		[InlineData("FieldTestONETwo", "Test ONE Two")]
		public void Parse_Name_ExpectFieldName(string name, string expected)
		{
			var fieldNameParser = new FieldNameParser();
			var actual = fieldNameParser.Parse(name);

			Assert.Equal(expected, actual);
		}
	}
}
