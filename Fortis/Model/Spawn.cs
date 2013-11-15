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
		private const string CONFIGURATION_KEY = "fortis";
		private const string ASSEMBLY_CONFIGURATION_KEY = "assembly";
		private static readonly NameValueCollection _configuration = (NameValueCollection)WebConfigurationManager.GetSection(CONFIGURATION_KEY);
		private static string ModelAssembly { get { return _configuration[ASSEMBLY_CONFIGURATION_KEY]; } }

		private static Dictionary<Guid, Type> _templateMap;
		internal static Dictionary<Guid, Type> TemplateMap
		{
			get
			{
				if (_templateMap == null)
				{
					_templateMap = new Dictionary<Guid, Type>();

					var modelAssembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(assembly => assembly.FullName.Equals(ModelAssembly));

					if (modelAssembly == null)
					{
						throw new Exception("Forits | Unable to find model assembly: " + ModelAssembly);
					}
					
					foreach (var t in modelAssembly.GetTypes())
					{
						foreach (TemplateMappingAttribute templateAttribute in t.GetCustomAttributes(typeof(TemplateMappingAttribute), false))
						{
							if (string.IsNullOrEmpty(templateAttribute.Type))
							{
								if (!_templateMap.ContainsKey(templateAttribute.Id))
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
					var modelAssembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(assembly => assembly.FullName.Equals(ModelAssembly));

					if (modelAssembly == null)
					{
						throw new Exception("Forits | Unable to find model assembly: " + ModelAssembly);
					}

					foreach (var t in modelAssembly.GetTypes())
					{
						foreach (TemplateMappingAttribute templateAttribute in t.GetCustomAttributes(typeof(TemplateMappingAttribute), false))
						{
							if (string.IsNullOrEmpty(templateAttribute.Type))
							{
								if (!_templateMap.ContainsKey(templateAttribute.Id))
								{
									_templateMap.Add(templateAttribute.Id, t);
								}
							}
						}
					}
				}

				return _interfaceTemplateMap;
			}
		}

		internal static IItemWrapper FromItem(Item item)
		{
			return FromItem<IItemWrapper>(item);
		}

		internal static IItemWrapper FromItem<T>(Item item)
		{
			if (item != null)
			{
				// Attempt to exact match the item against a template in the model
				var id = item.TemplateID.Guid;
				if (TemplateMap.ContainsKey(id))
				{
					// Get type information
					var type = TemplateMap[id];
					// Get public constructors
					var ctors = type.GetConstructors();
					// Invoke the first public constructor with no parameters.
					return (IItemWrapper)ctors[0].Invoke(new object[] { item });
				}

				// Attempt to match the template of the type passed through to an inherited template.
				if (typeof(T) != typeof(IItemWrapper))
				{
					if (InterfaceTemplateMap.ContainsKey(typeof(T)))
					{
						var typeTemplateId = InterfaceTemplateMap[typeof(T)];

						var itemTemplate = TemplateManager.GetTemplate(item);

						if (itemTemplate.DescendsFrom(new ID(typeTemplateId)))
						{
							// Get type information
							var type = TemplateMap[typeTemplateId];
							// Get public constructors
							var ctors = type.GetConstructors();
							// Invoke the first public constructor with no parameters.
							return (IItemWrapper)ctors[0].Invoke(new object[] { item });
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
		{
			return items.Select(item => (T)FromItem<T>(item)).Where(wrappedItem => wrappedItem != null);
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
			return from Field field in fields select FromField(field);
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
