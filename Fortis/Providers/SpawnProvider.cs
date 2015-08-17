using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Collections;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Fortis.Model.Fields;
using Fortis.Model;
using Sitecore.ContentSearch.Linq.Extensions;
using Sitecore.WordOCX.Extensions;

namespace Fortis.Providers
{
	public class SpawnProvider : ISpawnProvider
	{
		private readonly ITemplateMapProvider _templateMapProvider;

		public ITemplateMapProvider TemplateMapProvider { get { return _templateMapProvider; } }

		public SpawnProvider(ITemplateMapProvider templateMappingProvider)
		{
			_templateMapProvider = templateMappingProvider;
		}

		public IItemWrapper FromItem<T>(Guid itemId, Guid templateId) where T : IItemWrapper
		{
			return FromItem(itemId, templateId, typeof(T));
		}

		public IItemWrapper FromItem(Guid itemId, Guid templateId, Type template = null, Dictionary<string, object> lazyFields = null)
		{
			// Exact match
			if (TemplateMapProvider.TemplateMap.ContainsKey(templateId))
			{
				var concreteTemplates = TemplateMapProvider.TemplateMap[templateId];

				// Check to see if Type being requested is assignable to the concrete type
				if (template != null && !concreteTemplates.Any(template.IsAssignableFrom))
				{
					throw new Exception("Fortis: The types " + concreteTemplates.Select(ct => ct.Name).StringConcatenate(s => s + ", ") + "are not assignable from the type " + template.Name);
				}

				return (IItemWrapper)Activator.CreateInstance(concreteTemplates[0], new object[] { itemId, lazyFields, this });
			}

			return new ItemWrapper(itemId, this);
		}

		public IItemWrapper FromItem(Item item)
		{
			return FromItem<IItemWrapper>(item);
		}

		public IItemWrapper FromItem<T>(Item item) where T : IItemWrapper
		{
			return FromItem(item, typeof(T));
		}

		public IItemWrapper FromItem(Item item, Type template = null)
		{
			if (item != null)
			{
				// Attempt to exact match the item against a template in the model
				var id = item.TemplateID.Guid;
				if (TemplateMapProvider.TemplateMap.Keys.Contains(id))
				{
				    Type type = null;
					// Get type information
				    if (template != null && template.IsInterface)
				        type = TemplateMapProvider.TemplateMap[id].FirstOrDefault(t => t.ImplementsInterface(template));

				    if (type == null && template == null)
				    {
				        type = TemplateMapProvider.TemplateMap[id][0];

				    }

                    if(type != null)
                        return (IItemWrapper)Activator.CreateInstance(type, item, this);
                }

				if (template != null)
				{
					var wrapperType = template;

					// Attempt to match the template of the type passed through to an inherited template.
					if (wrapperType != typeof(IItemWrapper))
					{
						if (!TemplateMapProvider.InterfaceTemplateMap.ContainsKey(wrapperType))
						{
							throw new Exception("Fortis | Unable to find template for " + wrapperType.FullName);
						}

						var typeTemplateId = TemplateMapProvider.InterfaceTemplateMap[wrapperType];
						var itemTemplate = TemplateManager.GetTemplate(item);

						if (itemTemplate != null)
						{
							if (itemTemplate.DescendsFrom(new ID(typeTemplateId)))
							{
                                // Get type information
                                Type type = null;

                                // Get type information
                                if (template.IsInterface)
                                    type = TemplateMapProvider.TemplateMap[typeTemplateId].FirstOrDefault(t => t.ImplementsInterface(template));

                                if (type != null)
                                    return (IItemWrapper)Activator.CreateInstance(type, item, this);
							}
						}
					}
				}

				return new ItemWrapper(item, this);
			}

			return null;
		}

		public IEnumerable<IItemWrapper> FromItems(IEnumerable<Item> items)
		{
			return FromItems<IItemWrapper>(items);
		}

		public IEnumerable<T> FromItems<T>(IEnumerable<Item> items)
			where T : IItemWrapper
		{
		    return items.Select(FromItem<T>).OfType<T>();
		}

	    public IRenderingParameterWrapper FromRenderingParameters<T>(Item renderingItem, Dictionary<string, string> parameters)
			where T : IRenderingParameterWrapper
		{
			if (renderingItem != null)
			{
				var id = renderingItem["Parameters Template"];
				ID templateId = null;

				if (ID.TryParse(id, out templateId))
				{
					if (!TemplateMapProvider.RenderingParametersTemplateMap.ContainsKey(templateId.Guid))
					{
						throw new Exception("Fortis | Unable to find rendering parameters template " + id + " for " + renderingItem.Name);
					}

					var type = TemplateMapProvider.RenderingParametersTemplateMap[templateId.Guid][0];

					return (IRenderingParameterWrapper)Activator.CreateInstance(type, parameters, this);
				}
			}

			return null;
		}

		public IEnumerable<IFieldWrapper> FromFields(FieldCollection fields)
		{
			foreach (Field field in fields)
			{
				yield return FromField(field);
			}
		}

		public IEnumerable<IFieldWrapper> FromFields(FieldChangeList fields)
		{
			foreach (Field field in fields)
			{
				yield return FromField(field);
			}
		}

		public IFieldWrapper FromField(Field field)
		{
			if (field == null)
			{
				return null;
			}

			switch (field.Type.ToLower())
			{
				case "checkbox":
					return new BooleanFieldWrapper(field, this);
				case "image":
					return new ImageFieldWrapper(field, this);
				case "date":
				case "datetime":
					return new DateTimeFieldWrapper(field, this);
				case "checklist":
				case "treelist":
				case "treelistex":
				case "multilist":
					return new ListFieldWrapper(field, this);
				case "file":
					return new FileFieldWrapper(field, this);
				case "droplink":
				case "droptree":
					return new LinkFieldWrapper(field, this);
				case "general link":
					return new GeneralLinkFieldWrapper(field, this);
				case "text":
				case "single-line text":
				case "multi-line text":
				case "droplist":
					return new TextFieldWrapper(field, this);
				case "rich text":
					return new RichTextFieldWrapper(field, this);
				case "number":
					return new NumberFieldWrapper(field, this);
				case "integer":
					return new IntegerFieldWrapper(field, this);
				default:
					return null;
			}
		}

		public IEnumerable<T> FilterWrapperTypes<T>(IEnumerable<IItemWrapper> wrappers)
		{
			foreach (IItemWrapper wrapper in wrappers)
			{
				if (wrapper is T)
				{
					yield return (T)wrapper;
				}
			}
		}
	}
}
