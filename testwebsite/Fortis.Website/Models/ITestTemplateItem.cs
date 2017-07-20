using Fortis.Fields;
using Fortis.Items;

namespace Fortis.Website.Models
{
	public interface ITestTemplateItem : IItem
	{
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
	}
}
