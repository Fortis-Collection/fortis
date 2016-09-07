using System;
using Fortis.Model.Fields;
using Fortis.Providers;
using NSubstitute;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.FakeDb;
using Xunit;

namespace Fortis.Tests.Model.Fields
{
	/// <summary>
	/// Test Methods Syntax: [Testing method/property name]_[Input parameters if applicable]_[Expected behavior]
	/// </summary>
	/// <seealso cref="System.IDisposable" />
	public class BooleanFieldWrapperTests : IDisposable
	{
		private readonly ISpawnProvider SpawnProvider;

		private const string FieldName = "Boolean Field";
		private readonly Db Db;
		private readonly DbField Field;

		// test fixture setup
		public BooleanFieldWrapperTests()
		{
			this.SpawnProvider = Substitute.For<ISpawnProvider>();

			// Construct test item.
			this.Db = new Db();
			var item = new DbItem("Test", ID.NewID);
			this.Field = new DbField(FieldName, ID.NewID);
			item.Fields.Add(this.Field);
			this.Db.Add(item);
		}

		protected Field GetFieldObject()
		{
			var item = this.Db.GetItem("/sitecore/content/Test");
			return item.Fields[FieldName];
		}

		[Theory]
		[InlineData("", false)]
		[InlineData("0", false)]
		[InlineData("1", true)]
		public void ValueProperty_SpecificRawValue_ReturnsExpectedBoolean(string fieldValue, bool expectedValue)
		{
			this.Field.Value = fieldValue;

			var field = this.GetFieldObject();
			var fieldWrapper = new BooleanFieldWrapper(field, this.SpawnProvider);

			var actual = fieldWrapper.Value;

			Assert.Equal(expectedValue, actual);
		}

		[Theory]
		[InlineData("", false)]
		[InlineData("0", true)]
		[InlineData("1", true)]
		public void HasValueProperty_SpecificRawValue_ReturnsExpectedBoolean(string fieldValue, bool expectedValue)
		{
			this.Field.Value = fieldValue;

			var field = this.GetFieldObject();
			var fieldWrapper = new BooleanFieldWrapper(field, this.SpawnProvider);

			var actual = fieldWrapper.HasValue;

			Assert.Equal(expectedValue, actual);
		}

		// tst fixture tear down
		public void Dispose()
		{
			this.Db.Dispose();
		}
	}
}
