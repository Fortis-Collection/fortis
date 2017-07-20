using Fortis.Items;
using Fortis.Items.Context;
using Fortis.Website.Models;
using System;
using System.Web.Mvc;

namespace Fortis.Website.Controllers
{
    public class TestController : Controller
    {
		protected readonly IContextItem ContextItem;
		protected readonly IContextItemGetter ItemGetter;

		public TestController(
			IContextItem contextItem,
			IContextItemGetter itemGetter)
		{
			ContextItem = contextItem;
			ItemGetter = itemGetter;
		}

        public ViewResult Test()
        {
			var contextItem = ContextItem.GetItem<IItem>();
			var testItem = ItemGetter.GetItem<ITestTemplate>(new Guid("{62ED9278-F6DE-445C-BA36-68DB1565F3B3}"));
			var model = new TestModel
			{
				ContextItemName = contextItem.ItemName,
				TestTemplate = testItem
			};

            return View(model);
        }
    }
}