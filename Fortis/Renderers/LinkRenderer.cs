using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.Data.Items;
using Sitecore.Collections;
using Sitecore;
using Sitecore.Xml.Xsl;
using System.Web;
using Sitecore.Diagnostics;

namespace Fortis.Renderers
{
	public class LinkRenderer : Sitecore.Xml.Xsl.LinkRenderer
	{
		private readonly char[] _delimiter;
		private readonly Item _item;

		private string _innerHTML;
		public string InnerHTML
		{
			get { return _innerHTML; }
			set { _innerHTML = value; }
		}

		private string _css = string.Empty;
		public string CSS
		{
			get { return _css; }
			set { _css = value; }
		}

		private bool _includeContentEditorCss = true;
		public bool IncludeContentEditorCss
		{
			get { return _includeContentEditorCss; }
			set { _includeContentEditorCss = value; }
		}

		private bool _displayHrefByDefault = true;
		public bool DisplayHrefByDefault
		{
			get { return _displayHrefByDefault; }
			set { _displayHrefByDefault = value; }
		}

		public LinkRenderer(Item item)
			: base(item)
		{
			_delimiter = new char[] { '=', '&' };
			Assert.ArgumentNotNull(item, "item");
			_item = item;
		}

		public override Sitecore.Xml.Xsl.RenderFieldResult Render()
		{
			string str8;
			SafeDictionary<string> dictionary = new SafeDictionary<string>();
			dictionary.AddRange(this.Parameters);
			if (MainUtil.GetBool(dictionary["endlink"], false))
			{
				return RenderFieldResult.EndLink;
			}
			Set<string> set = Set<string>.Create(new string[] { "field", "select", "text", "haschildren", "before", "after", "enclosingtag", "fieldname" });
			Sitecore.Data.Fields.LinkField linkField = this.LinkField;
			if (linkField != null)
			{
				dictionary["title"] = StringUtil.GetString(new string[] { dictionary["title"], linkField.Title });
				dictionary["target"] = StringUtil.GetString(new string[] { dictionary["target"], linkField.Target });

				if (_includeContentEditorCss)
				{
					_css += (string.IsNullOrWhiteSpace(_css) ? string.Empty : " ") + linkField.Class;
				}
			}
			string str = string.Empty;
			string rawParameters = this.RawParameters;
			if (!string.IsNullOrEmpty(rawParameters) && (rawParameters.IndexOfAny(this._delimiter) < 0))
			{
				str = rawParameters;
			}
			if (string.IsNullOrEmpty(str))
			{
				Item targetItem = this.TargetItem;
				string str3 = (targetItem != null) ? targetItem.DisplayName : string.Empty;
				string str4 = (linkField != null) ? linkField.Text : string.Empty;
				str = StringUtil.GetString(new string[] { str, dictionary["text"], str4, str3 });
			}
			string url = this.GetUrl(linkField);
			if (((str8 = this.LinkType) != null) && (str8 == "javascript"))
			{
				dictionary["href"] = "#";
				dictionary["onclick"] = StringUtil.GetString(new string[] { dictionary["onclick"], url });
			}
			else
			{
				dictionary["href"] = HttpUtility.HtmlEncode(StringUtil.GetString(new string[] { dictionary["href"], url }));
			}
			StringBuilder tag = new StringBuilder("<a", 0x2f);
			foreach (KeyValuePair<string, string> pair in dictionary)
			{
				string key = pair.Key;
				string str7 = pair.Value;
				if (!set.Contains(key.ToLowerInvariant()))
				{
					FieldRendererBase.AddAttribute(tag, key, str7);
				}
			}
			if (!string.IsNullOrWhiteSpace(_css))
			{
				FieldRendererBase.AddAttribute(tag, "class", _css);
			}
			tag.Append('>');
			if (!MainUtil.GetBool(dictionary["haschildren"], false))
			{
				if (!string.IsNullOrEmpty(_innerHTML))
				{
					tag.Append(_innerHTML);
				}
				else if (!string.IsNullOrEmpty(str))
				{
					tag.Append(str);
				}
				else if (_displayHrefByDefault)
				{
					tag.Append(dictionary["href"]);
				}
				else
				{
					return RenderFieldResult.Empty;
				}
			}
			return new RenderFieldResult { FirstPart = tag.ToString(), LastPart = "</a>" };
		}
	}
}