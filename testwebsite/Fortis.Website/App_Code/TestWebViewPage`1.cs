using Fortis.Items.Context;
using System.Web.Mvc;

namespace Fortis.Website.App_Code
{
    public abstract class TestWebViewPage<TModel> : WebViewPage<TModel>
    {
        public TestWebViewPage()
        {
            ContextItem = DependencyResolver.Current.GetService<IContextItem>();
        }

        public IContextItem ContextItem { get; private set; }
    }
}