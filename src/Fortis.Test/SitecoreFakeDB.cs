using Sitecore.FakeDb;
using Xunit;

namespace Fortis.Test
{
	public class SitecoreFakeDB
	{
		[Fact]
		public void ConfigurationCheck()
		{
			using (var db = new Db { new DbItem("Home") { { "Title", "Welcome!" } } })
			{
				Sitecore.Data.Items.Item home = db.GetItem("/sitecore/content/home");

				Assert.Equal("Welcome!", home["Title"]);
			}
		}
	}
}
