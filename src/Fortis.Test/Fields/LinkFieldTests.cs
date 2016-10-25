using Fortis.Fields.LinkField;
using System;
using Xunit;

namespace Fortis.Test.Fields
{
	public class LinkFieldTests : FieldTestAutoFixture<LinkField>
	{
		[Fact]
		public void Value_ValidId_ReturnsValidGuid()
		{
			var rawValue = "{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}";

			FakeField.Value = rawValue;

			var expected = Guid.Parse(rawValue);
			var actual = ModelledField.Value;

			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData("")]
		[InlineData("invalid value")]
		[InlineData(null)]
		public void Value_FieldValue_ReturnsDefaultGuid(string fieldValue)
		{
			FakeField.Value = fieldValue;

			var expected = default(Guid);
			var actual = ModelledField.Value;

			Assert.Equal(expected, actual);
		}

		protected override LinkField Create()
		{
			return new LinkField();
		}
	}
}
