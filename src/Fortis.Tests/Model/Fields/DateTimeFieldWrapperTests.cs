using System;
using Fortis.Model.Fields;
using Xunit;

namespace Fortis.Tests.Model.Fields
{
    /// <summary>
    /// Test Methods Syntax: [Testing method/property name]_[given condition/input parameters]_[Expected behavior]
    /// </summary>
    /// <seealso cref="Fortis.Tests.Model.Fields.FieldWrapperTestClass{Fortis.Model.Fields.DateTimeFieldWrapper}" />
    public class DateTimeFieldWrapperTests : FieldWrapperTestClass<DateTimeFieldWrapper>
	{
		[Fact]
		public void Value_ValidRawValue_ReturnsExpectedDateTime()
		{
			this.Field.Value = "20160428T220000Z";

			var actual = this.FieldWrapper.Value;

			Assert.Equal(new DateTime(2016, 4, 28, 22, 0, 0), actual);
		}

		[Fact]
		public void Value_InvalidRawValue_ReturnsDefaultDateTime()
		{
			this.Field.Value = "this is not a valid date time value";

			var actual = this.FieldWrapper.Value;

			Assert.Equal(default(DateTime), actual);
		}

		[Fact]
		public void Value_EmptyRawValue_ReturnsDefaultDateTime()
		{
			this.Field.Value = "";

			var actual = this.FieldWrapper.Value;

			Assert.Equal(default(DateTime), actual);
		}

		[Fact]
		public void HasValue_EmptyRawValue_ReturnsFalse()
		{
			this.Field.Value = "";

			var actual = this.FieldWrapper.HasValue;

			Assert.Equal(false, actual);
		}

		[Fact]
		public void HasValue_ValidRawValue_ReturnsTrue()
		{
			this.Field.Value = "20160428T220000Z";

			var actual = this.FieldWrapper.HasValue;

			Assert.Equal(true, actual);
		}

		[Fact]
		public void HasValue_InvalidRawValue_ReturnsTrue()
		{
			this.Field.Value = "this is not a valid date time value";

			var actual = this.FieldWrapper.HasValue;

			Assert.Equal(true, actual);
		}
	}
}