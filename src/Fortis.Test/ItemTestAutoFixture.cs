using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.FakeDb;

namespace Fortis.Test
{
	public class ItemTestAutoFixture
	{
		protected const string FieldName = "Test Field";
		protected const string ItemName = "Test Item";
		protected Db FakeDatabase;
		protected DbTemplate FakeTemplateA;
		protected DbTemplate FakeTemplateB;
		protected DbTemplate FakeTemplateC;
		protected DbItem FakeItem;
		protected DbField FakeField;

		protected ItemTestAutoFixture()
		{
			FakeTemplateA = new DbTemplate("Template A", new ID("{d4d8da75-efff-4e1f-98d7-1b05df85160e}"));

			FakeTemplateA.Add(new DbField("Test")
			{
				Type = "Single-Line Text"
			});
			FakeTemplateA.Add(new DbField("Test Date Time")
			{
				Type = "DateTime"
			});
			FakeTemplateA.Add(new DbField("Test Boolean")
			{
				Type = "Checkbox"
			});

			FakeTemplateB = new DbTemplate("Template B", new ID("{3453112b-6d83-4f60-93be-7c09e1416d00}"));
			FakeTemplateC = new DbTemplate("Template C", new ID("{42f7627e-a0db-4f1e-bd5c-b6ad0763309a}")) { BaseIDs = new ID[] { FakeTemplateA.ID } };
			FakeItem = new DbItem(ItemName) { TemplateID = FakeTemplateC.ID };

			FakeItem.Fields.Add("Test", "Test Text Field Value");
			FakeItem.Fields.Add("Test Date Time", "20160428T220000Z");
			FakeItem.Fields.Add("Test Boolean", "1");

			FakeField = new DbField(FieldName);
			FakeDatabase = new Db();

			SetField(ref FakeField);

			FakeTemplateA.Add(FakeField);

			SetItem(ref FakeItem);

			FakeDatabase.Add(FakeTemplateA);
			FakeDatabase.Add(FakeTemplateB);
			FakeDatabase.Add(FakeTemplateC);
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
