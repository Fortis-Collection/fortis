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

		private static Dictionary<Guid, Type> _templateMap = null;
		internal static Dictionary<Guid, Type> TemplateMap
		{
			get
			{
				if (_templateMap == null)
				{
					_templateMap = new Dictionary<Guid, Type>();
					
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

		private static Dictionary<Guid, Type> _renderingParametersTemplateMap = null;
		internal static Dictionary<Guid, Type> RenderingParametersTemplateMap
		{
			get
			{
				if (_renderingParametersTemplateMap == null)
				{
					_renderingParametersTemplateMap = new Dictionary<Guid, Type>();

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

		internal static Type GetImplementation<T>() where T : IItemWrapper
		{
			var typeOfT = typeof(T);

			if (!typeOfT.IsInterface)
			{
				throw new Exception("Fortis: An interface implementing IITemWrapper must be passed as the generic argument to get the corresponding implementation. " + typeOfT.Name + " is not an interface.");
			}

			if (!InterfaceTemplateMap.ContainsKey(typeOfT))
			{
				throw new Exception("Fortis: Type " + typeOfT.Name + " does not exist in interface template map");
			}

			var templateId = InterfaceTemplateMap[typeOfT];

			if (!TemplateMap.ContainsKey(templateId))
			{
				throw new Exception("Fortis: Template ID " + templateId + " does not exist in template map");
			}

			return TemplateMap[templateId];
		}

		internal static IItemWrapper FromItem<T>(Guid itemId, Guid templateId) where T : IItemWrapper
		{
			return FromItem(itemId, templateId, typeof(T));
		}

		internal static IItemWrapper FromItem(Guid itemId, Guid templateId, Type template = null, Dictionary<string, object> lazyFields = null)
		{
			// Exact match
			if (TemplateMap.ContainsKey(templateId))
			{
				var concreteTemplate = TemplateMap[templateId];

				// Check to see if Type being requested is assignable to the concrete type
				if (!template.IsAssignableFrom(concreteTemplate))
				{
					throw new Exception("Fortis: The type " + concreteTemplate.Name + " is not assignable from the type " + template.Name);
				}

				return (IItemWrapper)Activator.CreateInstance(concreteTemplate, new object[] { itemId, lazyFields });
			}

			// Inherited template mapping needs implementing

			//var typeOfBaseWrapper = typeof(IItemWrapper);

			//if (template != null &&
			//	template.IsInterface &&
			//	template != typeOfBaseWrapper &&
			//	template.IsAssignableFrom(typeOfBaseWrapper))
			//{
			//	if (!InterfaceTemplateMap.ContainsKey(template))
			//	{
			//		throw new Exception("Fortis | Unable to find template for " + template.FullName);
			//	}

				
			//}

			return new ItemWrapper(itemId);
		}

		internal static IItemWrapper FromItem(Item item)
		{
			return FromItem<IItemWrapper>(item);
		}

		internal static IItemWrapper FromItem<T>(Item item) where T : IItemWrapper
		{
			return FromItem(item, typeof(T));
		}

		internal static IItemWrapper FromItem(Item item, Type template = null)
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

				if (template != null)
				{
					var wrapperType = template;

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
				ID templateId = null;

				if (ID.TryParse(id, out templateId))
				{
					if (!RenderingParametersTemplateMap.ContainsKey(templateId.Guid))
					{
						throw new Exception("Fortis | Unable to find rendering parameters template " + id + " for " + renderingItem.Name);
					}

					var type = RenderingParametersTemplateMap[templateId.Guid];

					return (IRenderingParameterWrapper)Activator.CreateInstance(type, new object[] { parameters });
				}
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

		internal static bool IsCompatibleTemplate<T>(Guid templateId) where T : IItemWrapper
		{
			return IsCompatibleTemplate(templateId, typeof(T));
		}

		internal static bool IsCompatibleTemplate(Guid templateId, Type template)
		{
			// template Type must at least implement IItemWrapper
			if (template != typeof(IItemWrapper))
			{
				// TODO: Implement
			}

			return true;
		}

		internal static bool IsCompatibleFieldType<T>(string fieldType) where T : FieldWrapper
		{
			return IsCompatibleFieldType(fieldType, typeof(T));
		}

		internal static bool IsCompatibleFieldType(string scFieldType, Type fieldType)
		{
			switch (scFieldType.ToLower())
			{
				case "checkbox":
					return fieldType == typeof(BooleanFieldWrapper);
				case "image":
					return fieldType == typeof(ImageFieldWrapper);
				case "date":
				case "datetime":
					return fieldType == typeof(DateTimeFieldWrapper);
				case "checklist":
				case "treelist":
				case "treelistex":
				case "multilist":
					return fieldType == typeof(ListFieldWrapper);
				case "file":
					return fieldType == typeof(FileFieldWrapper);
				case "droplink":
				case "droptree":
					return fieldType == typeof(LinkFieldWrapper);
				case "general link":
					return fieldType == typeof(GeneralLinkFieldWrapper);
				case "text":
				case "single-line text":
				case "multi-line text":
				case "droplist":
					return fieldType == typeof(TextFieldWrapper);
				case "rich text":
					return fieldType == typeof(RichTextFieldWrapper);
				case "number":
					return fieldType == typeof(NumberFieldWrapper);
				case "integer":
					return fieldType == typeof(IntegerFieldWrapper);
				default:
					return false;
			}
		}

		internal static IFieldWrapper FromField(Field field)
		{
			if (field == null)
			{
				return null;
			}

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
				case "droplist":
					return new TextFieldWrapper(field);
				case "rich text":
					return new RichTextFieldWrapper(field);
				case "number":
					return new NumberFieldWrapper(field);
				case "integer":
					return new IntegerFieldWrapper(field);
				default:
					return null;
			}
		}
	}
}
