using Fortis.Model.RenderingParameters.Fields;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Fortis.Model
{
	public class RenderingParameterWrapper : IRenderingParameterWrapper
	{
		protected Dictionary<string, string> _parameters = null;
		protected Dictionary<string, FieldWrapper> _fields = new Dictionary<string, FieldWrapper>();

		public RenderingParameterWrapper(Dictionary<string, string> parameters)
		{
			_parameters = parameters;
		}

		protected IFieldWrapper GetField(string key, string type)
		{
			if (!_fields.ContainsKey(key))
			{
				try
				{
					switch (type)
					{
						case "checkbox":
							_fields[key] = new BooleanFieldWrapper(_parameters[key]);
							break;
						case "checklist":
						case "treelist":
						case "treelistex":
						case "multilist":
						case "tags":
							_fields[key] = new ListFieldWrapper(_parameters[key]);
							break;
						case "droplink":
						case "droptree":
						case "general link":
							_fields[key] = new LinkFieldWrapper(_parameters[key]);
							break;
						case "single-line text":
						case "multi-line text":
						case "rich text":
						case "droplist":
						case "number":
							_fields[key] = new TextFieldWrapper(_parameters[key]);
							break;
						default:
							_fields[key] = new FieldWrapper(_parameters[key]);
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

		public object Original
		{
			get { return _parameters; }
		}
	}
}
