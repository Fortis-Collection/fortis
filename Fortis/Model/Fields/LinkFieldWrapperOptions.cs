using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fortis.Model.Fields
{
	public class LinkFieldWrapperOptions
	{
		public static readonly string OptionsParameterName = "linkField";
		public static readonly string InnerHtmlParameterName = "innerHTML";
		public static readonly string CssParameterName = "css";
		public static readonly string EditorCssParameterName = "includeEditorCss";
		public static readonly string HrefDefaultParameterName = "hrefDefault";

		public virtual string InnerHtml { get; set; }
		public virtual string Css { get; set; }
		public virtual string IncludeContentEditorCss { get; set; }
		public virtual string DisplayHrefByDefault { get; set; }
	}
}
