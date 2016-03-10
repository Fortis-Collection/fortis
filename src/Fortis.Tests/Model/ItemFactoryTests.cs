namespace Fortis.Tests.Model
{
	using Fortis.Model;
	using Fortis.Mvc.Providers;
	using Fortis.Providers;

	using NUnit.Framework;

	[TestFixture]
    public class ItemFactoryTests
    {


		[Test]
		public void Select_HomeItem_IItemWrapper()
		{
			using (var db = new Sitecore.FakeDb.Db
			{
				new Sitecore.FakeDb.DbItem("Home")
				{
					{ "Title", "Welcome!" }
				}
			})
			{
				var contextProvider = new ContextProvider();
				var spawnProvider = new SpawnProvider(new TemplateMapProvider(new ModelAssemblyProvider()));
				var itemFactory = new ItemFactory(contextProvider, spawnProvider);

				//var homeItemWrapper = 

				//var home = db.GetItem("/sitecore/content/home");
				//Assert.AreEqual("Welcome!", home["Title"]);
			}
		}
    }
}
