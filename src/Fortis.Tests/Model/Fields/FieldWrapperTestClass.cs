using System;
using Fortis.Model.Fields;
using Fortis.Providers;
using NSubstitute;
using Sitecore.Data;
using Sitecore.FakeDb;

namespace Fortis.Tests.Model.Fields
{
	public abstract class FieldWrapperTestClass<TFieldWrapper> where TFieldWrapper : FieldWrapper
	{
		protected ISpawnProvider SpawnProvider;

		protected const string FieldName = "Testing Field";
		protected Db Db;
		protected DbField Field;

		// test fixture setup
		protected FieldWrapperTestClass()
		{
			this.SpawnProvider = Substitute.For<ISpawnProvider>();

			// Construct test item.
			this.Db = new Db();
			var item = new DbItem("Test", ID.NewID);
			this.Field = new DbField(FieldName, ID.NewID);
			item.Fields.Add(this.Field);
			this.Db.Add(item);
		}

		protected TFieldWrapper CreateFieldWrapper()
		{
			var item = this.Db.GetItem("/sitecore/content/Test");
			var field = item.Fields[FieldName];
			return (TFieldWrapper)Activator.CreateInstance(typeof(TFieldWrapper), field, this.SpawnProvider);
		}

		// tst fixture tear down
		public void Dispose()
		{
			this.Db.Dispose();
		}
	}
}
