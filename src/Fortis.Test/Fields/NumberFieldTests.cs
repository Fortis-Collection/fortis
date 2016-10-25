using Fortis.Fields.NumberField;
using Xunit;

namespace Fortis.Test.Fields
{
	public class NumberFieldTests : FieldTestAutoFixture<NumberField>
	{
		[Theory]
		[InlineData("", 0.0)]
		[InlineData("invalid value", 0.0)]
		[InlineData(null, 0.0)]
		[InlineData("1", 1.0)]
		[InlineData("1.1", 1.1)]
		public void Value_FieldValue_ReturnsExpectedValue(string fieldValue, float expectedValue)
		{
			FakeField.Value = fieldValue;

			var expected = expectedValue;
			var actual = ModelledField.Value;

			Assert.Equal(expected, actual);
		}

		protected override NumberField Create()
		{
			return new NumberField();
		}
	}
}
