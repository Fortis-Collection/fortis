using System;
using Fortis.Model.Fields;
using Fortis.Providers;
using NSubstitute;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.FakeDb;
using Sitecore.FakeDb.Sites;

namespace Fortis.Tests.Model.Fields
{
    /// <summary>
    /// Base class for testing field wrappers
    /// </summary>
    /// <typeparam name="TFieldWrapper">The type of the field wrapper.</typeparam>
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

			// init sitecore context
			Sitecore.Context.Site = new FakeSiteContext("website");
			Sitecore.Context.Item = this.Item;
		}

	    protected Item Item
	    {
		    get { return this.Db.GetItem("/sitecore/content/Test"); }
	    }

	    protected TFieldWrapper FieldWrapper
		{
		    get
		    {
		        return (TFieldWrapper) Activator.CreateInstance(
					typeof (TFieldWrapper), 
					this.Item.Fields[FieldName], 
					this.SpawnProvider);
		    }
		}

		// test fixture tear down
		public void Dispose()
		{
			this.Db.Dispose();
		}
	}
}
