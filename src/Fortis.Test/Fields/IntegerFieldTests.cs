using Fortis.Fields.IntegerField;
using Xunit;

namespace Fortis.Test.Fields
{
	public class IntegerFieldTests : FieldTestAutoFixture<IntegerField>
	{
		[Theory]
		[InlineData("", 0)]
		[InlineData("invalid value", 0)]
		[InlineData(null, 0)]
		[InlineData("1", 1)]
		[InlineData("1.1", 0)]
		public void Value_FieldValue_ReturnsExpectedValue(string fieldValue, int expectedValue)
		{
			FakeField.Value = fieldValue;

			var expected = expectedValue;
			var actual = ModelledField.Value;

			Assert.Equal(expected, actual);
		}

		protected override IntegerField Create()
		{
			return new IntegerField();
		}
	}
}
