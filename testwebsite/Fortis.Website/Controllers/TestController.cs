using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fortis.Website.Controllers
{
    public class TestController : Controller
    {
		public TestController(
			)
		{

		}

        public ViewResult Test()
        {
            return View();
        }
    }
}