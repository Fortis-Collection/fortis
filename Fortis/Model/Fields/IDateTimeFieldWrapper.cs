using System;
using System.Web;

namespace Fortis.Model.Fields
{
	public interface IDateTimeFieldWrapper : IFieldWrapper<DateTime>
	{
		IHtmlString Render(bool includeTime);
	}
}
