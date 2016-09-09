using Fortis.Model.Fields;
using Xunit;

namespace Fortis.Tests.Model.Fields
{
    /// <summary>
    /// Test Methods Syntax: [Testing method/property name]_[given condition/input parameters]_[Expected behavior]
    /// </summary>
    /// <seealso cref="Fortis.Tests.Model.Fields.FieldWrapperTestClass{Fortis.Model.Fields.NumberFieldWrapper}" />
    public class NumberFieldWrapperTests : FieldWrapperTestClass<NumberFieldWrapper>
    {
        [Theory]
        [InlineData("", 0)]
        [InlineData("22", 22)]
        [InlineData("invalid float value", 0)]
        public void Value_SpecificRawValue_ReturnsExpectedFloatValue(string rawValue, float expectedValue)
        {
            this.Field.Value = rawValue;

            var actual = this.FieldWrapper.Value;

            Assert.Equal(expectedValue, actual);
        }

        [Theory]
        [InlineData("", false)]
        [InlineData("22", true)]
        [InlineData("invalid float value", false)]
        public void HasValue_SpecificRawValue_ReturnsExpectedBoolean(string rawValue, bool expectedValue)
        {
            this.Field.Value = rawValue;

            var actual = this.FieldWrapper.HasValue;

            Assert.Equal(expectedValue, actual);
        }
    }
}