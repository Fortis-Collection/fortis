using System.Collections.Generic;
using System.Collections.Specialized;

namespace Fortis.Model.RenderingParameters.Fields
{
	public class RenderingParameterWrapper : IRenderingParameterWrapper
	{
		protected NameValueCollection _paramters = null;
		protected Dictionary<string, FieldWrapper> _fields = new Dictionary<string, FieldWrapper>();

		public RenderingParameterWrapper(string queryString)
		{
			_paramters = Sitecore.Web.WebUtil.ParseUrlParameters(queryString);
		}

		protected FieldWrapper GetField(string key, string type)
		{
			key = key.ToLower();
			if (!_fields.ContainsKey(key))
			{
				try
				{
					switch (type)
					{
						case "checkbox":
							_fields[key] = new BooleanFieldWrapper(_paramters[key]);
							break;
						case "checklist":
						case "treelist":
						case "treelistex":
						case "multilist":
							_fields[key] = new ListFieldWrapper(_paramters[key]);
							break;
						case "droplink":
						case "droptree":
						case "general link":
							_fields[key] = new LinkFieldWrapper(_paramters[key]);
							break;
						case "single-line text":
						case "multi-line text":
						case "rich text":
						case "droplist":
						case "number":
							_fields[key] = new TextFieldWrapper(_paramters[key]);
							break;
						default:
							_fields[key] = new FieldWrapper(_paramters[key]);
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
