using System.IO;
using System.Web;
using Fortis.Fields;

namespace Fortis.Web.Fields
{
    public interface IFieldRenderer
    {
        IFieldRenderResult BeginRender(IField field, TextWriter textWriter, object parameters = null);
        IFieldRenderResult BeginRenderEditable(IField field, TextWriter textWriter, object parameters = null);
        IHtmlString Render(IField field, object parameters = null);
        IHtmlString RenderEditable(IField field, object parameters = null);
    }
}