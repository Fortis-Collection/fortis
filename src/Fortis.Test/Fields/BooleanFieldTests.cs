using Fortis.Fields.BooleanField;
using Xunit;

namespace Fortis.Test.Fields
{
	public class BooleanFieldTests : FieldTestAutoFixture<BooleanField>
	{
		[Theory]
		[InlineData("", false)]
		[InlineData("0", false)]
		[InlineData("invalid value", false)]
		[InlineData(null, false)]
		[InlineData("1", true)]
		public void Value_FieldValue_ReturnsExpectedValue(string fieldValue, bool expectedValue)
		{
			FakeField.Value = fieldValue;

			var expected = expectedValue;
			var actual = ModelledField.Value;

			Assert.Equal(expected, actual);
		}

		protected override BooleanField Create()
		{
			return new BooleanField();
		}
	}
}
