using Fortis.Model.Fields;
using Xunit;

namespace Fortis.Tests.Model.Fields
{
    /// <summary>
    /// Test Methods Syntax: [Testing method/property name]_[given condition/input parameters]_[Expected behavior]
    /// </summary>
    /// <seealso cref="Fortis.Tests.Model.Fields.FieldWrapperTestClass{Fortis.Model.Fields.BooleanFieldWrapper}" />
    public class BooleanFieldWrapperTests : FieldWrapperTestClass<BooleanFieldWrapper>
	{
		[Theory]
		[InlineData("", false)]
		[InlineData("0", false)]
		[InlineData("invalid boolean value", false)]
		[InlineData("1", true)]
		public void Value_SpecificRawValue_ReturnsExpectedBoolean(string fieldValue, bool expectedValue)
		{
			this.Field.Value = fieldValue;

			var actual = this.FieldWrapper.Value;

			Assert.Equal(expectedValue, actual);
		}

		[Theory]
		[InlineData("", false)]
		[InlineData("0", true)]
		[InlineData("invalid boolean value", true)]
		[InlineData("1", true)]
		public void HasValue_SpecificRawValue_ReturnsExpectedBoolean(string fieldValue, bool expectedValue)
		{
			this.Field.Value = fieldValue;

			var actual = this.FieldWrapper.HasValue;

			Assert.Equal(expectedValue, actual);
		}
	}
}