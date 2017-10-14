using System;

namespace Fortis.Web.Fields
{
    public interface IFieldRenderResult : IDisposable
    {
        void EndRender();
    }
}
