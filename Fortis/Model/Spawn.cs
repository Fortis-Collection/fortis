using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Collections;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Fortis.Model.Fields;
using System.Reflection;
using System.Collections.Specialized;
using System.Web.Configuration;

namespace Fortis.Model
{
	internal static class Spawn
	{
		private static readonly string _configurationKey = "fortis";
		private static readonly string _assemblyConfigurationKey = "assembly";
		private static Assembly _modelAssembly;
		private static readonly NameValueCollection _configuration = (NameValueCollection)WebConfigurationManager.GetSection(_configurationKey);
		private static string ModelAssemblyName { get { return _configuration[_assemblyConfigurationKey]; } }
		private static Assembly ModelAssembly
		{
			get
			{
				if (_modelAssembly == null)
				{
					foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
					{
						if (assembly.FullName.Equals(ModelAssemblyName))
						{
							_modelAssembly = assembly;
							break;
						}
					}

					if (_modelAssembly == null)
					{
						throw new Exception("Forits | Unable to find model assembly: " + ModelAssemblyName);
					}
				}

				return _modelAssembly;
			}
		}

		private static Dictionary<string, Type> _templateMap = null;
		internal static Dictionary<string, Type> TemplateMap
		{
			get
			{
				if (_templateMap == null)
				{
					_templateMap = new Dictionary<string, Type>();
					
					foreach (var t in ModelAssembly.GetTypes())
					{
						foreach (TemplateMappingAttribute templateAttribute in t.GetCustomAttributes(typeof(TemplateMappingAttribute), false))
						{
							if (string.IsNullOrEmpty(templateAttribute.Type))
							{
								if (!_templateMap.Keys.Contains(templateAttribute.Id))
								{
									_templateMap.Add(templateAttribute.Id, t);
								}
							}
						}
					}
				}

				return _templateMap;
			}
		}

		private static Dictionary<Type, Guid> _interfaceTemplateMap;
		internal static Dictionary<Type, Guid> InterfaceTemplateMap
		{
			get
			{
				if (_interfaceTemplateMap == null)
				{
					_interfaceTemplateMap = new Dictionary<Type, Guid>();

					foreach (var t in ModelAssembly.GetTypes())
					{
						foreach (TemplateMappingAttribute templateAttribute in t.GetCustomAttributes(typeof(TemplateMappingAttribute), false))
						{
							if (string.Equals(templateAttribute.Type, "InterfaceMap"))
							{
								if (!_interfaceTemplateMap.Keys.Contains(t))
								{
									_interfaceTemplateMap.Add(t, templateAttribute.Id);
								}
							}
						}
					}
				}

				return _interfaceTemplateMap;
			}
		}

		private static Dictionary<string, Type> _renderingParametersTemplateMap = null;
		internal static Dictionary<string, Type> RenderingParametersTemplateMap
		{
			get
			{
				if (_renderingParametersTemplateMap == null)
				{
					_renderingParametersTemplateMap = new Dictionary<string, Type>();

					foreach (var t in ModelAssembly.GetTypes())
					{
						foreach (TemplateMappingAttribute templateAttribute in t.GetCustomAttributes(typeof(TemplateMappingAttribute), false))
						{
							if (templateAttribute.Type == "RenderingParameter")
							{
								if (!_renderingParametersTemplateMap.Keys.Contains(templateAttribute.Id))
								{
									_renderingParametersTemplateMap.Add(templateAttribute.Id, t);
								}
							}
						}
					}
				}

				return _renderingParametersTemplateMap;
			}
		}

		internal static IItemWrapper FromItem(Item item)
		{
			return FromItem<IItemWrapper>(item);
		}

		internal static IItemWrapper FromItem<T>(Item item)
			where T : IItemWrapper
		{
			if (item != null)
			{
				// Attempt to exact match the item against a template in the model
				var id = item.TemplateID.Guid;
				if (TemplateMap.Keys.Contains(id))
				{
					// Get type information
					var type = TemplateMap[id];

					return (IItemWrapper)Activator.CreateInstance(type, new object[] { item });
				}

				var wrapperType = typeof(T);

				// Attempt to match the template of the type passed through to an inherited template.
				if (wrapperType != typeof(IItemWrapper))
				{
					if (!InterfaceTemplateMap.ContainsKey(wrapperType))
					{
						throw new Exception("Fortis | Unable to find template for " + wrapperType.FullName);
					}

					var typeTemplateId = InterfaceTemplateMap[wrapperType];

						var itemTemplate = TemplateManager.GetTemplate(item);

						if (itemTemplate.DescendsFrom(new ID(typeTemplateId)))
						{
							// Get type information
							var type = TemplateMap[typeTemplateId];

							return (IItemWrapper)Activator.CreateInstance(type, new object[] { item });
						}
					}
				}

				return new ItemWrapper(item);
			}

			return null;
		}

		internal static IEnumerable<IItemWrapper> FromItems(IEnumerable<Item> items)
		{
			return FromItems<IItemWrapper>(items);
		}

		internal static IEnumerable<T> FromItems<T>(IEnumerable<Item> items)
			where T : IItemWrapper
		{
			foreach (var item in items)
			{
				var wrappedItem = (T)FromItem<T>(item);

				if (wrappedItem != null)
				{
					yield return wrappedItem;
				}
			}
		}

		internal static IRenderingParameterWrapper FromRenderingParameters<T>(Item renderingItem, Dictionary<string, string> parameters)
			where T : IRenderingParameterWrapper
		{
			if (renderingItem != null)
			{
				var id = renderingItem["Parameters Template"];

				if (!RenderingParametersTemplateMap.ContainsKey(id))
				{
					throw new Exception("Fortis | Unable to find rendering parameters template " + id + " for " + renderingItem.Name);
				}

				var type = RenderingParametersTemplateMap[id];

				return (IRenderingParameterWrapper)Activator.CreateInstance(type, new object[] { parameters });
			}

			return null;
		}

		internal static IEnumerable<IFieldWrapper> FromFields(FieldCollection fields)
		{
			foreach (Field field in fields)
			{
				yield return FromField(field);
			}
		}

		internal static IEnumerable<IFieldWrapper> FromFields(FieldChangeList fields)
		{
			foreach (Field field in fields)
			{
				yield return FromField(field);
			}
		}

		internal static IFieldWrapper FromField(Field field)
		{
			switch (field.Type.ToLower())
			{
				case "checkbox":
					return new BooleanFieldWrapper(field);
				case "image":
					return new ImageFieldWrapper(field);
				case "date":
				case "datetime":
					return new DateTimeFieldWrapper(field);
				case "checklist":
				case "treelist":
				case "treelistex":
				case "multilist":
					return new ListFieldWrapper(field);
				case "file":
					return new FileFieldWrapper(field);
				case "droplink":
				case "droptree":
					return new LinkFieldWrapper(field);
				case "general link":
					return new GeneralLinkFieldWrapper(field);
				case "text":
				case "single-line text":
				case "multi-line text":
				case "number":
				case "droplist":
					return new TextFieldWrapper(field);
				case "rich text":
					return new RichTextFieldWrapper(field);
				default:
					return null;
			}
		}
	}
}
