using Fortis.Model.RenderingParameters.Fields;
using Fortis.Providers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Fortis.Model
{
	public class RenderingParameterWrapper : IRenderingParameterWrapper
	{
		protected ISpawnProvider SpawnProvider;
		protected Dictionary<string, string> _parameters = null;
		protected Dictionary<string, FieldWrapper> _fields = new Dictionary<string, FieldWrapper>();

		public RenderingParameterWrapper(Dictionary<string, string> parameters, ISpawnProvider spawnProvider)
		{
			_parameters = parameters;

			SpawnProvider = spawnProvider;
		}

		protected IFieldWrapper GetField(string key, string type)
		{
			if (!_fields.ContainsKey(key))
			{
				var parameterValue = string.Empty;

				_parameters.TryGetValue(key, out parameterValue);

				try
				{
					switch (type)
					{
						case "checkbox":
							_fields[key] = new BooleanFieldWrapper(parameterValue, SpawnProvider);
							break;
						case "checklist":
						case "treelist":
						case "treelist with search":
						case "treelistex":
						case "multilist":
						case "multilist with search":
						case "tags":
							_fields[key] = new ListFieldWrapper(parameterValue, SpawnProvider);
							break;
						case "droplink":
						case "droptree":
						case "general link":
							_fields[key] = new LinkFieldWrapper(parameterValue, SpawnProvider);
							break;
						case "single-line text":
						case "multi-line text":
						case "rich text":
						case "droplist":
						case "number":
							_fields[key] = new TextFieldWrapper(parameterValue, SpawnProvider);
							break;
						case "integer":
							_fields[key] = new IntegerFieldWrapper(parameterValue, SpawnProvider);
							break;
						default:
							_fields[key] = new FieldWrapper(parameterValue, SpawnProvider);
							break;
					}
				}
				catch (Exception ex)
				{
					var paramKeys = new StringBuilder();

					foreach (var param in _parameters)
					{
						paramKeys.AppendLine(param.Key);
					}

					throw new Exception("Fortis: An error occurred while mapping the field with key " + key
										+ Environment.NewLine
										+ Environment.NewLine
										+ "Available parameters: "
										+ Environment.NewLine
										+ paramKeys.ToString(), ex);
				}
			}

			try
			{
				return (FieldWrapper)_fields[key];
			}
			catch (Exception ex)
			{
				throw new Exception("Fortis: An error occurred while mppaing the field with key " + key, ex);
			}
		}

		public object Original
		{
			get { return _parameters; }
		}
	}
}
