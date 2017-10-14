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
		protected readonly IContextItemGetter ContextItemGetter;

		public TestController(
			IContextItem contextItem,
			IContextItemGetter contextItemGetter)
		{
			ContextItem = contextItem;
			ContextItemGetter = contextItemGetter;
		}

        public ViewResult Test()
        {
			var contextItem = ContextItem.GetItem<IItem>();
			var testTemplate = ContextItemGetter.GetItem<ITestTemplate>(new Guid("{62ED9278-F6DE-445C-BA36-68DB1565F3B3}"));
			var testTemplateItem = ContextItemGetter.GetItem<ITestTemplateItem>(new Guid("{62ED9278-F6DE-445C-BA36-68DB1565F3B3}"));
			var model = new TestModel
			{
				ContextItemName = contextItem.ItemName,
				TestTemplate = testTemplate,
				TestTemplateItem = testTemplateItem
			};

            return View(model);
        }
    }
}