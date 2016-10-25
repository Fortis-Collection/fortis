using Fortis.Fields.FileField;
using Xunit;

namespace Fortis.Test.Fields
{
	public class FileFieldTests : FieldTestAutoFixture<FileField>
	{
		[Theory]
		[InlineData("<file mediaid=\"{094AED03-02E7-4868-80CB-19926661FB77}\" src=\"-/media/094AED0302E7486880CB19926661FB77.ashx\" />")]
		[InlineData("<file mediaid=\"{094AED03-02E7-4868-80CB-19926661FB77}\" />")]
		[InlineData("")]
		[InlineData(null)]
		public void Src_FieldValue_ReturnsUrl(string fieldValue)
		{
			FakeField.Value = fieldValue;

			var expected = ((Sitecore.Data.Fields.FileField)Field).Src;
			var actual = ModelledField.Url;

			Assert.Equal(expected, actual);
		}

		protected override FileField Create()
		{
			return new FileField();
		}
	}
}
