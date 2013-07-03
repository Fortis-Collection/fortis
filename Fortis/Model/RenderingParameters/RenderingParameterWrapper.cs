using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Fortis.Model.RenderingParameters.Fields
{
	public class RenderingParameterWrapper : IRenderingParameterWrapper
	{
		protected NameValueCollection paramters = null;
		protected Dictionary<string, FieldWrapper> _fields = new Dictionary<string, FieldWrapper>();

		public RenderingParameterWrapper(string queryString)
		{
			paramters = Sitecore.Web.WebUtil.ParseUrlParameters(queryString);
		}

		protected FieldWrapper GetField(string key, string type)
		{
			key = key.ToLower();
			if (!_fields.Keys.Contains(key))
			{
				try
				{
					switch (type)
					{
						case "checkbox":
							_fields[key] = new BooleanFieldWrapper(paramters[key]);
							break;
						case "checklist":
						case "treelist":
						case "treelistex":
						case "multilist":
							_fields[key] = new ListFieldWrapper(paramters[key]);
							break;
						case "droplink":
						case "droptree":
						case "general link":
							_fields[key] = new LinkFieldWrapper(paramters[key]);
							break;
						case "single-line text":
						case "multi-line text":
						case "rich text":
						case "droplist":
						case "number":
							_fields[key] = new TextFieldWrapper(paramters[key]);
							break;
						default:
							_fields[key] = new FieldWrapper(paramters[key]);
							break;
					}
				}
				catch
				{
					// Todo: Log error
				}
			}

			return (FieldWrapper)_fields[key];
		}
	}
}
