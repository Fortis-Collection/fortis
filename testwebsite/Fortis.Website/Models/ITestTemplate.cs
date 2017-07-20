using Fortis.Fields;
using System;

namespace Fortis.Website.Models
{
	public interface ITestTemplate
	{
		bool TestCheckbox { get; }
		DateTime TestDate { get; }
		[Field("Test Datetime")]
		DateTime TestDateTime { get; }
		string TestFile { get; }
		string TestImage { get; }
		int TestInteger { get; }
		string TestMultiLineText { get; }
		float TestNumber { get; }
		string TestPassword { get; }
		string TestRichText { get; }
		string TestSingleLineText { get; }
	}
}
