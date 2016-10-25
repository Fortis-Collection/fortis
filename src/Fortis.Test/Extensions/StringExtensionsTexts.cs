using Fortis.Extensions;
using Xunit;

namespace Fortis.Test.Extensions
{
	public class StringExtensionsTexts
	{
		[Theory]
		[InlineData("Test", " ", "Test")]
		[InlineData("TestOne", " ", "Test One")]
		[InlineData("TestOneTwo", " ", "Test One Two")]
		[InlineData("TestONETwo", " ", "Test ONE Two")]
		[InlineData("Test", "|", "Test")]
		[InlineData("TestOne", "|", "Test|One")]
		[InlineData("TestOneTwo", "|", "Test|One|Two")]
		[InlineData("TestONETwo", "|", "Test|ONE|Two")]
		public void SeparatePascal_Source_ExpectedValue(string source, string separator, string expected)
		{
			var actual = source.SeparatePascal(separator);

			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData("Test", "Test")]
		[InlineData("TestOne", "Test One")]
		[InlineData("TestOneTwo", "Test One Two")]
		[InlineData("TestONETwo", "Test ONE Two")]
		public void SpaceSeparatePascal_Source_ExpectedValue(string source, string expected)
		{
			var actual = source.SpaceSeparatePascal();

			Assert.Equal(expected, actual);
		}
	}
}
