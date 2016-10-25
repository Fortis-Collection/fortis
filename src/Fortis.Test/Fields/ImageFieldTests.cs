using Fortis.Fields.ImageField;
using System;
using Xunit;

namespace Fortis.Test.Fields
{
	public class ImageFieldTests : FieldTestAutoFixture<ImageField>
	{
		//<image mediaid="{094AED03-02E7-4868-80CB-19926661FB77}" alt="Alt Text" height="" width="" hspace="" vspace="" />

		[Theory]
		[InlineData("", "")]
		[InlineData(null, "")]
		[InlineData("<image mediaid=\"{094AED03-02E7-4868-80CB-19926661FB77}\" alt=\"Alt Text\" height=\"\" width=\"\" hspace=\"\" vspace=\"\" />", "Alt Text")]
		public void AltText_FieldValue_ReturnsExpectedValue(string fieldValue, string expected)
		{
			FakeField.Value = fieldValue;

			var actual = ModelledField.AltText;

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void MediaId_ValidId_ReturnsValidGuid()
		{
			FakeField.Value = "<image mediaid=\"{094AED03-02E7-4868-80CB-19926661FB77}\" alt=\"Alt Text\" height=\"\" width=\"\" hspace=\"\" vspace=\"\" />";

			var expected = Guid.Parse("{094AED03-02E7-4868-80CB-19926661FB77}");
			var actual = ModelledField.MediaId;

			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData("", "")]
		[InlineData(null, "")]
		public void MediaId_ValidId_ReturnsDefaultGuid(string fieldValue, string expectedValue)
		{
			FakeField.Value = fieldValue;

			var expected = default(Guid);
			var actual = ModelledField.MediaId;

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void Url_ValidMediaItem_ReturnsRelativeUrl()
		{
			FakeField.Value = "<image mediaid=\"{094AED03-02E7-4868-80CB-19926661FB77}\" alt=\"Alt Text\" height=\"\" width=\"\" hspace=\"\" vspace=\"\" />";

			var expected = "";
			var actual = ModelledField.Url;

			Assert.Equal(expected, actual);
		}

		protected override ImageField Create()
		{
			return new ImageField();
		}
	}
}
