using Fortis.Fields;
using Fortis.Items;

namespace Fortis.Website.Models
{
	public interface ITestTemplateItem : IItem
	{
		// Simple
		IBooleanField TestCheckbox { get; }
		IDateTimeField TestDate { get; }
		[Field("Test Datetime")]
		IDateTimeField TestDateTime { get; }
		IFileField TestFile { get; }
		IImageField TestImage { get; }
		IIntegerField TestInteger { get; }
		ITextField TestMultiLineText { get; }
		INumberField TestNumber { get; }
		ITextField TestPassword { get; }
		ITextField TestRichText { get; }
		ITextField TestSingleLineText { get; }
		// List
		ILinkListField TestChecklist { get; }
		ITextField TestDroplist { get; }
		ILinkField TestGroupedDroplink { get; }
		ITextField TestGroupedDroplist { get; }
		ILinkListField TestMultilist { get; }
		ILinkListField TestMultilistWithSearch { get; }
		INameValueListField TestNameLookupValueList { get; }
		INameValueListField TestNameValueList { get; }
		ILinkListField TestTreelist { get; }
		ILinkListField TestTreelistEx { get; }
		// Link
		ILinkField TestDroplink { get; }
		ILinkField TestDroptree { get; }
		IGeneralLinkField TestGeneralLink { get; }
		IGeneralLinkField TestGeneralLinkWithSearch { get; }
        IGeneralLinkField TestGeneralLinkMedia { get; }
    }
}
