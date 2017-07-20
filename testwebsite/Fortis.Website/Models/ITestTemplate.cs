using Fortis.Fields;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Fortis.Website.Models
{
	public interface ITestTemplate
	{
		// Simple
		bool TestCheckbox { get; }
		DateTime TestDate { get; }
		DateTime TestDateTime { get; }
		string TestFile { get; }
		string TestImage { get; }
		int TestInteger { get; }
		string TestMultiLineText { get; }
		float TestNumber { get; }
		string TestPassword { get; }
		string TestRichText { get; }
		string TestSingleLineText { get; }
		// List
		IEnumerable<Guid> TestChecklist { get; }
		string TestDroplist { get; }
		Guid TestGroupedDroplink { get; }
		string TestGroupedDroplist { get; }
		IEnumerable<Guid> TestMultilist { get; }
		IEnumerable<Guid> TestMultilistWithSearch { get; }
		NameValueCollection TestNameLookupValueList { get; }
		NameValueCollection TestNameValueList { get; }
		IEnumerable<Guid> TestTreelist { get; }
		IEnumerable<Guid> TestTreelistEx { get; }
	}
}
