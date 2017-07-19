using System.Linq;
using Fortis.Items;
using Sitecore.FakeDb;
using Xunit;
using Sitecore.Data;

namespace Fortis.Test.Items
{
	public class BaseItemTests : ItemTestAutoFixture
	{
		[Fact]
		public void ItemId_Item_ItemIDGuid()
		{
			var item = Create(Item);

			var expected = Item.ID.Guid;
			var actual = item.ItemId;

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ItemLongId_Item_ItemPathsLongID()
		{
			var item = Create(Item);

			var expected = Item.Paths.LongID;
			var actual = item.ItemLongId;

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ItemShortId_Item_ItemIDToShortID()
		{
			var item = Create(Item);

			var expected = Item.ID.ToShortID().ToString();
			var actual = item.ItemShortId;

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ItemPath_Item_ItemPathsPath()
		{
			var item = Create(Item);

			var expected = Item.Paths.Path;
			var actual = item.ItemPath;

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ItemName_Item_ItemName()
		{
			var item = Create(Item);

			var expected = Item.Name;
			var actual = item.ItemName;

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ItemDisplayName_Item_ItemDisplayName()
		{
			var item = Create(Item);

			var expected = Item.DisplayName;
			var actual = item.ItemDisplayName;

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ItemLanguage_Item_ItemLanguageName()
		{
			var item = Create(Item);

			var expected = Item.Language.Name;
			var actual = item.ItemLanguage;

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ItemDatabase_Item_ItemDatabaseName()
		{
			var item = Create(Item);

			var expected = Item.Database.Name;
			var actual = item.ItemDatabase;

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ItemTemplateId_Item_ItemTemplateIDGuid()
		{
			var item = Create(Item);

			var expected = Item.TemplateID.Guid;
			var actual = item.ItemTemplateId;

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ItemTemplateName_Item_ItemTemplateName()
		{
			var item = Create(Item);

			var expected = Item.TemplateName;
			var actual = item.ItemTemplateName;

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ItemTemplateIds_Item_ItemTemplateIDGuid()
		{
			var item = Create(Item);

			var expected = Item.Template.BaseTemplates.Select(bt => bt.ID.Guid);
			var actual = item.ItemTemplateIds;

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ItemIslatestVersion_Item_ItemTemplateIDGuid()
		{
			var item = Create(Item);

			var expected = Item.Versions.IsLatestVersion();
			var actual = item.ItemIsLatestVersion;

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ItemIsStandardValues_Item_StandardValuesManagerIsStandardValuesHolder()
		{
			var item = Create(Item);

			var expected = StandardValuesManager.IsStandardValuesHolder(Item);
			var actual = item.ItemIsStandardValues;

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ItemChildrenCount_Item_ItemChildrenCount()
		{
			var item = Create(Item);

			var expected = Item.Children.Count;
			var actual = item.ItemChildrenCount;

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ItemHasChildren_Item_ItemHasChildren()
		{
			var item = Create(Item);

			var expected = Item.HasChildren;
			var actual = item.ItemHasChildren;

			Assert.Equal(expected, actual);
		}

		public override void SetItem(ref DbItem item)
		{
			item.Name = "Test Item";
			item.TemplateID = new ID("{42f7627e-a0db-4f1e-bd5c-b6ad0763309a}");
		}

		public BaseItem Create(Sitecore.Data.Items.Item item)
		{
			return new BaseItem(null, null)
			{
				Item = item
			};
		}
	}
}
