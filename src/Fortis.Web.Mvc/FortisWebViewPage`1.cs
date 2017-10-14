using Fortis.Web.Fields;
using System.Web;
using System.Web.Mvc;
using Fortis.Fields;
using System.IO;

namespace Fortis.Web.Mvc
{
    public abstract class FortisWebViewPage<TModel> : WebViewPage<TModel>
    {
        private readonly IFieldRenderer fieldRenderer;

        public FortisWebViewPage()
        {
            fieldRenderer = new FieldRenderer();
        }

        public IFieldRenderResult BeginRenderField(IField field, TextWriter textWriter, object parameters = null)
        {
            return fieldRenderer.BeginRender(field, Output, parameters);
        }

        public IFieldRenderResult BeginRenderEditableField(IField field, object parameters = null)
        {
            return fieldRenderer.BeginRenderEditable(field, Output, parameters);
        }

        public IHtmlString RenderField(IField field, object parameters = null)
        {
            return fieldRenderer.Render(field, parameters);
        }

        public IHtmlString RenderEditableField(IField field, object parameters = null)
        {
            return fieldRenderer.RenderEditable(field, parameters);
        }
    }
}
