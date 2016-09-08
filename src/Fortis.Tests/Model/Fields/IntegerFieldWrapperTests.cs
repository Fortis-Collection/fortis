using Fortis.Model.Fields;
using Xunit;

namespace Fortis.Tests.Model.Fields
{
    /// <summary>
    /// [Testing method/property name]_[given condition/input parameters]_[Expected behavior]
    /// </summary>
    /// <seealso cref="Fortis.Tests.Model.Fields.FieldWrapperTestClass{Fortis.Model.Fields.IntegerFieldWrapper}" />
    public class IntegerFieldWrapperTests: FieldWrapperTestClass<IntegerFieldWrapper>
    {
        [Theory]
        [InlineData("", 0)]
        [InlineData("22", 22)]
        [InlineData("invalid long value", 0)]
        public void Value_SpecificRawValue_ReturnsExpectedLongValue(string rawValue, long expectedValue)
        {
            this.Field.Value = rawValue;

            var actual = this.FieldWrapper.Value;

            Assert.Equal(expectedValue, actual);
        }

        [Theory]
        [InlineData("", false)]
        [InlineData("22", true)]
        [InlineData("invalid long value", false)]
        public void HasValue_SpecificRawValue_ReturnsExpectedBoolean(string rawValue, bool expectedValue)
        {
            this.Field.Value = rawValue;

            var actual = this.FieldWrapper.HasValue;

            Assert.Equal(expectedValue, actual);
        }
    }
}
