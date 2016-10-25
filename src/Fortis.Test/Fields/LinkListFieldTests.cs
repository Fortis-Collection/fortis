using Fortis.Fields.LinkListField;
using System;
using System.Collections.Generic;
using Xunit;

namespace Fortis.Test.Fields
{
	public class LinkListFieldTests : FieldTestAutoFixture<LinkListField>
	{
		[Fact]
		public void Value_PipeSeparatedGuids_ReturnsCorrectlyOrderedEnumerableGuids()
		{
			var rawValue = "{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}|{094AED03-02E7-4868-80CB-19926661FB77}";

			FakeField.Value = rawValue;

			var expected = new List<Guid>
			{
				new Guid("{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}"),
				new Guid("{094AED03-02E7-4868-80CB-19926661FB77}")
			};
			var actual = ModelledField.Value;

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void Value_EmptyValue_ReturnsEmptyEnumerableGuids()
		{
			var rawValue = "";

			FakeField.Value = rawValue;

			var expected = new List<Guid>();
			var actual = ModelledField.Value;

			Assert.Equal(expected, actual);
		}

		protected override LinkListField Create()
		{
			return new LinkListField();
		}
	}
}
