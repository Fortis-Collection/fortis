using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.Collections;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Fortis.Model.Fields;

namespace Fortis.Model
{
	internal static class Spawn
	{
		private static Dictionary<string, Type> _templateMap;
		internal static Dictionary<string, Type> TemplateMap
		{
			get
			{
				if (_templateMap == null)
				{
					_templateMap = new Dictionary<string, Type>();
					var assembly = System.Reflection.Assembly.GetCallingAssembly();

					foreach (var t in assembly.GetTypes())
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

		private static Dictionary<Type, string> _interfaceTemplateMap;
		internal static Dictionary<Type, string> InterfaceTemplateMap
		{
			get
			{
				if (_interfaceTemplateMap == null)
				{
					_interfaceTemplateMap = new Dictionary<Type, string>();
					var assembly = System.Reflection.Assembly.GetCallingAssembly();

					foreach (Type t in assembly.GetTypes())
					{
						foreach (TemplateMappingAttribute templateAttribute in t.GetCustomAttributes(typeof(TemplateMappingAttribute), false))
						{
							if (templateAttribute.Type == "InterfaceMap")
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

		internal static IItemWrapper FromItem(Item item)
		{
			return FromItem<IItemWrapper>(item);
		}

		internal static IItemWrapper FromItem<T>(Item item)
		{
			if (item != null)
			{
				// Attempt to exact match the item against a template in the model
				var id = item.TemplateID.ToString();
				if (TemplateMap.Keys.Contains(id))
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
					var typeTemplateId = InterfaceTemplateMap[typeof(T)];

					if (typeTemplateId != null)
					{
						var itemTemplate = TemplateManager.GetTemplate(item);

						if (Sitecore.Data.ID.IsID(typeTemplateId) && itemTemplate.DescendsFrom(Sitecore.Data.ID.Parse(typeTemplateId)))
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
			foreach (var item in items)
			{
				var wrappedItem = (T)FromItem<T>(item);

				if (wrappedItem != null)
				{
					yield return wrappedItem;
				}
			}
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
