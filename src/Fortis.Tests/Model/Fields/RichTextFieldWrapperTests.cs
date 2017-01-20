using System.Web;
using Fortis.Model.Fields;
using Xunit;

namespace Fortis.Tests.Model.Fields
{
    /// <summary>
    /// Test Methods Syntax: [Testing method/property name]_[given condition/input parameters]_[Expected behavior]
    /// </summary>
    /// <seealso cref="Fortis.Tests.Model.Fields.FieldWrapperTestClass{Fortis.Model.Fields.RichTextFieldWrapper}" />
    public class RichTextFieldWrapperTests : FieldWrapperTestClass<RichTextFieldWrapper>
    {
		[Theory]
		[InlineData("", "")]
		[InlineData("   ", "   ")]
		[InlineData("test", "test")]
		public void Value_SpecificRawValue_ReturnsExpectedHtmlStringValue(string rawValue, string expectedValue)
		{
			this.Field.Value = rawValue;

			var actual = this.FieldWrapper.Value;
			// Unable to compare two HtmlString objects here, need to compare strings instead.
			Assert.Equal(expectedValue, actual.ToHtmlString());
		}

		[Theory]
        [InlineData("", false)]
        [InlineData("   ", false)]
        [InlineData("test", true)]
        public void HasValue_SpecificRawValue_ReturnsExpectedBoolean(string rawValue, bool expectedValue)
        {
            this.Field.Value = rawValue;

            var actual = this.FieldWrapper.HasValue;

            Assert.Equal(expectedValue, actual);
        }
    }
}
