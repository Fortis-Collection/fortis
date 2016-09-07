using System;
using Fortis.Model.Fields;
using Xunit;

namespace Fortis.Tests.Model.Fields
{
	/// <summary>
	/// Test Methods Syntax: [Testing method/property name]_[Input parameters if applicable]_[Expected behavior]
	/// </summary>
	/// <seealso cref="Fortis.Tests.Model.Fields.FieldWrapperTestClass{Fortis.Model.Fields.DateTimeFieldWrapper}" />
	public class DateTimeFieldWrapperTests : FieldWrapperTestClass<DateTimeFieldWrapper>
	{
		[Fact]
		public void ValueProperty_ValidRawValue_ReturnsExpectedDateTime()
		{
			this.Field.Value = "20160428T220000Z";

			var fieldWrapper = this.CreateFieldWrapper();

			var actual = fieldWrapper.Value;

			Assert.Equal(new DateTime(2016, 4, 28, 22, 0, 0), actual);
		}

		[Fact]
		public void ValueProperty_InvalidRawValue_ReturnsDefaultDateTime()
		{
			this.Field.Value = "this is not a valid date time value";

			var fieldWrapper = this.CreateFieldWrapper();

			var actual = fieldWrapper.Value;

			Assert.Equal(default(DateTime), actual);
		}

		[Fact]
		public void ValueProperty_EmptyRawValue_ReturnsDefaultDateTime()
		{
			this.Field.Value = "";

			var fieldWrapper = this.CreateFieldWrapper();

			var actual = fieldWrapper.Value;

			Assert.Equal(default(DateTime), actual);
		}

		[Fact]
		public void HasValueProperty_EmptyRawValue_ReturnsFalse()
		{
			this.Field.Value = "";

			var fieldWrapper = this.CreateFieldWrapper();

			var actual = fieldWrapper.HasValue;

			Assert.Equal(false, actual);
		}

		[Fact]
		public void HasValueProperty_ValidRawValue_ReturnsTrue()
		{
			this.Field.Value = "20160428T220000Z";

			var fieldWrapper = this.CreateFieldWrapper();

			var actual = fieldWrapper.HasValue;

			Assert.Equal(true, actual);
		}

		[Fact]
		public void HasValueProperty_InvalidRawValue_ReturnsTrue()
		{
			this.Field.Value = "this is not a valid date time value";

			var fieldWrapper = this.CreateFieldWrapper();

			var actual = fieldWrapper.HasValue;

			Assert.Equal(true, actual);
		}
	}
}