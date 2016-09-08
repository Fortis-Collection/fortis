using Fortis.Model.Fields;
using Xunit;

namespace Fortis.Tests.Model.Fields
{
    /// <summary>
    /// Test Methods Syntax: [Testing method/property name]_[given condition/input parameters]_[Expected behavior]
    /// </summary>
    /// <seealso cref="Fortis.Tests.Model.Fields.FieldWrapperTestClass{Fortis.Model.Fields.TextFieldWrapper}" />
    public class TextFieldWrapperTests : FieldWrapperTestClass<TextFieldWrapper>
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("   ", "   ")]
        [InlineData("test", "test")]
        public void Value_SpecificRawValue_ReturnsExpectedStringValue(string rawValue, string expectedValue)
        {
            this.Field.Value = rawValue;

            var actual = this.FieldWrapper.Value;

            Assert.Equal(expectedValue, actual);
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