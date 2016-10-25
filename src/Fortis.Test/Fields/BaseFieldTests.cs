using Fortis.Fields;
using Xunit;

namespace Fortis.Test.Fields
{
	public class BaseFieldTests : FieldTestAutoFixture<BaseField>
	{
		[Fact]
		public void Value_FieldNotNull_ReturnsValue()
		{
			var rawValue = "Value";

			FakeField.Value = rawValue;

			var expected = rawValue;
			var actual = ModelledField.Value;

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void Value_FieldNull_ReturnsNull()
		{
			var modelledField = ModelledField;

			modelledField.Field = null;

			string expected = null;
			var actual = modelledField.Value;

			Assert.Equal(expected, actual);
		}

		protected override BaseField Create()
		{
			return new BaseField();
		}
	}
}
