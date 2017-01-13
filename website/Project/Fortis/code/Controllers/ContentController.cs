using System.Web.Mvc;
using Fortis.Model;
using LM.Model.Templates.Ignite;

namespace Fortis.Website.Controllers
{
    public class ContentController : Controller
    {
        protected readonly IItemFactory ItemFactory;

        public ContentController(IItemFactory itemFactory)
        {
            ItemFactory = itemFactory;
        }

        // GET: Content
        public ActionResult MetaData()
        {
            var contextItems = this.ItemFactory.GetRenderingContextItems<IMetaData, IMetaData>();

            return this.View("MetaData", contextItems.RenderingItem);
        }

        public ActionResult PageList()
        {
            var contextItems = this.ItemFactory.GetRenderingContextItems<IItemWrapper, IContentList>();

            return this.View("PageList", contextItems.RenderingItem);
        }
    }
}