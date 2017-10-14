using System.IO;

namespace Fortis.Web.Fields
{
    public class FieldRenderResult : IFieldRenderResult
    {
        private TextWriter textWriter;
        private string lastPart;

        public FieldRenderResult(
            TextWriter textWriter,
            string firstPart,
            string lastPart)
        {
            this.textWriter = textWriter;
            this.lastPart = lastPart;

            this.textWriter.Write(firstPart);
        }

        public void EndRender()
        {
            textWriter.Write(lastPart);
        }

        public void Dispose()
        {
            EndRender();
        }
    }
}
