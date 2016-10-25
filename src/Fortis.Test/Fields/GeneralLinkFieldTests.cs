using Fortis.Fields.GeneralLinkField;
using Xunit;

namespace Fortis.Test.Fields
{
	public class GeneralLinkFieldTests : FieldTestAutoFixture<GeneralLinkField>
	{
		//<link text=\"Description\" linktype=\"internal\" class=\"Styles\" title=\"Alt Text\" target=\"_blank\" querystring=\"Querystring\" id=\"{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}\" />
		//<link text=\"Description\" linktype=\"media\" title=\"Alt Text\" class="Styles" target="_blank" id="{094AED03-02E7-4868-80CB-19926661FB77}" />
		//<link text="Description" linktype="external" url="http://www.external.com" anchor="" title="Alt Text" class="Styles" target="_blank" />

		[Theory]
		[InlineData("", "")]
		[InlineData("<link linktype=\"internal\" title=\"Alt Text\" id=\"{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}\" />", "Alt Text")]
		[InlineData(null, "")]
		public void AltText_FieldValue_ReturnsExpectedValue(string fieldValue, string expected)
		{
			FakeField.Value = fieldValue;

			var actual = ModelledField.AltText;

			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData("", "")]
		[InlineData("<link linktype=\"internal\" target=\"_blank\" id=\"{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}\" />", "_blank")]
		[InlineData(null, "")]
		public void BrowserTarget_FieldValue_ReturnsExpectedValue(string fieldValue, string expected)
		{
			FakeField.Value = fieldValue;

			var actual = ModelledField.BrowserTarget;

			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData("", "")]
		[InlineData("<link linktype=\"internal\" text=\"Description\" id=\"{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}\" />", "Description")]
		[InlineData(null, "")]
		public void Description_FieldValue_ReturnsExpectedValue(string fieldValue, string expected)
		{
			FakeField.Value = fieldValue;

			var actual = ModelledField.Description;

			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData("", true)]
		[InlineData("<link linktype=\"internal\" id=\"{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}\" />", true)]
		[InlineData("<link linktype=\"media\" id=\"{094AED03-02E7-4868-80CB-19926661FB77}\" />", false)]
		[InlineData("<link linktype=\"external\" url=\"http://www.external.com\" />", false)]
		[InlineData(null, true)]
		public void IsInternal_FieldValue_ReturnsExpectedValue(string fieldValue, bool expected)
		{
			FakeField.Value = fieldValue;

			var actual = ModelledField.IsInternal;

			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData("", false)]
		[InlineData("<link linktype=\"internal\" id=\"{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}\" />", false)]
		[InlineData("<link linktype=\"media\" id=\"{094AED03-02E7-4868-80CB-19926661FB77}\" />", true)]
		[InlineData("<link linktype=\"external\" url=\"http://www.external.com\" />", false)]
		[InlineData(null, false)]
		public void IsMedia_FieldValue_ReturnsExpectedValue(string fieldValue, bool expected)
		{
			FakeField.Value = fieldValue;

			var actual = ModelledField.IsMediaLink;

			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData("", "")]
		[InlineData("<link linktype=\"internal\" class=\"Styles\" id=\"{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}\" />", "Styles")]
		[InlineData(null, "")]
		public void Styles_FieldValue_ReturnsExpectedValue(string fieldValue, string expected)
		{
			FakeField.Value = fieldValue;

			var actual = ModelledField.Styles;

			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData("", "")]
		[InlineData("<link linktype=\"internal\" id=\"{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}\" />", "")]
		[InlineData("<link linktype=\"media\" id=\"{094AED03-02E7-4868-80CB-19926661FB77}\" />", "")]
		[InlineData("<link linktype=\"external\" url=\"http://www.external.com\" />", "http://www.external.com")]
		[InlineData(null, "")]
		public void Url_FieldValue_ReturnsExpectedValue(string fieldValue, string expected)
		{
			FakeField.Value = fieldValue;

			var actual = ModelledField.Url;

			Assert.Equal(expected, actual);
		}

		protected override GeneralLinkField Create()
		{
			return new GeneralLinkField();
		}
	}
}
