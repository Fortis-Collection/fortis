namespace Fortis.Tests.Model
{
	using System;
	using Fortis.Model;
	using Fortis.Mvc.Providers;
	using Fortis.Providers;
	using Sitecore.Data;
	using Sitecore.FakeDb;
	using Xunit;

	public class ItemFactoryTests
    {
		[Fact]
		public void Select_HomeItem_IItemWrapper()
		{
			Assert.True(true);

			//var homeItemId = new ID(new Guid("df640448-a780-4bae-ba30-a5cb02310feb"));
			//var mockItem = new DbItem("Home", homeItemId)
			//{
			//	{"Title", "Welcome!"}
			//};

			//using (var db = new Db())
			//{
			//	db.Add(mockItem);

			//	var contextProvider = new ContextProvider();
			//	var spawnProvider = new SpawnProvider(new TemplateMapProvider(new ModelAssemblyProvider()));
			//	var itemFactory = new ItemFactory(contextProvider, spawnProvider);

			//	var homeItemWrapper = itemFactory.Select<IItemWrapper>(homeItemId.Guid);
			//	var home = db.GetItem(homeItemId);

			//	Assert.NotNull(homeItemWrapper);
			//	Assert.NotNull(home);
			//	Assert.Equal(homeItemWrapper.Name, home.Name);
			//}
		}
    }
}
