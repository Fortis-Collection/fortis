using Fortis.Model.Fields;
using Xunit;

namespace Fortis.Tests.Model.Fields
{
	/// <summary>
	/// Test Methods Syntax: [Testing method/property name]_[Input parameters if applicable]_[Expected behavior]
	/// </summary>
	/// <seealso cref="System.IDisposable" />
	public class BooleanFieldWrapperTests : FieldWrapperTestClass<BooleanFieldWrapper>
	{
		[Theory]
		[InlineData("", false)]
		[InlineData("0", false)]
		[InlineData("1", true)]
		public void ValueProperty_SpecificRawValue_ReturnsExpectedBoolean(string fieldValue, bool expectedValue)
		{
			this.Field.Value = fieldValue;

			var fieldWrapper = this.CreateFieldWrapper();

			var actual = fieldWrapper.Value;

			Assert.Equal(expectedValue, actual);
		}

		[Theory]
		[InlineData("", false)]
		[InlineData("0", true)]
		[InlineData("1", true)]
		public void HasValueProperty_SpecificRawValue_ReturnsExpectedBoolean(string fieldValue, bool expectedValue)
		{
			this.Field.Value = fieldValue;

			var fieldWrapper = this.CreateFieldWrapper();

			var actual = fieldWrapper.HasValue;

			Assert.Equal(expectedValue, actual);
		}
	}
}