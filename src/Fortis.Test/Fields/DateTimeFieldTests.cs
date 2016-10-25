using Fortis.Fields.DateTimeField;
using System;
using Xunit;

namespace Fortis.Test.Fields
{
	public class DateTimeFieldTests : FieldTestAutoFixture<DateTimeField>
	{
		[Fact]
		public void Value_ValidValue_ReturnsExpectedDateTime()
		{
			var rawValue = "20160428T220000Z";

			FakeField.Value = rawValue;

			var expected = new DateTime(2016, 4, 28, 22, 0, 0);
			var actual = ModelledField.Value;

			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData("")]
		[InlineData("Invalid Value")]
		[InlineData(null)]
		public void Value_InvalidValue_ReturnsDefaultDateTime(string fieldValue)
		{
			FakeField.Value = fieldValue;

			var expected = default(DateTime);
			var actual = ModelledField.Value;

			Assert.Equal(expected, actual);
		}

		protected override DateTimeField Create()
		{
			return new DateTimeField();
		}
	}
}
