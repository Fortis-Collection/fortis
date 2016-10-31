using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.FakeDb;

namespace Fortis.Test.Fields
{
	public class FieldTestAutoFixture
	{
		protected const string FieldName = "Test Field";
		protected const string ItemName = "Test Item";
		protected Db FakeDatabase;
		protected DbItem FakeItem;
		protected DbField FakeField;

		protected FieldTestAutoFixture()
		{
			FakeItem = new DbItem(ItemName);
			FakeField = new DbField(FieldName);
			FakeDatabase = new Db();

			SetField(ref FakeField);

			FakeItem.Fields.Add(FakeField);

			SetItem(ref FakeItem);

			FakeDatabase.Add(FakeItem);
		}

		protected Field Field => Item.Fields[FieldName];

		protected Item Item => FakeDatabase.GetItem($"/sitecore/content/{ItemName}");

		public void Dispose()
		{
			FakeDatabase.Dispose();
		}

		public virtual void SetField(ref DbField field) { }

		public virtual void SetItem(ref DbItem item) { }
	}
}
