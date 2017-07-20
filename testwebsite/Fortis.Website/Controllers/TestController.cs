using Fortis.Items;
using Fortis.Items.Context;
using Fortis.Website.Models;
using System.Web.Mvc;

namespace Fortis.Website.Controllers
{
    public class TestController : Controller
    {
		protected readonly IContextItem ContextItem;

		public TestController(
			IContextItem contextItem)
		{
			ContextItem = contextItem;
		}

        public ViewResult Test()
        {
			var item = ContextItem.GetItem<IItem>();
			var model = new TestModel
			{
				ItemName = item.ItemName
			};

            return View(model);
        }
    }
}